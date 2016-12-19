using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskPortType;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;

namespace br.ufc.mdcc.hpc.storm.binding.task.impl.TaskBindingBaseImpl
{
	public class TaskPort<T> : BaseTaskPort<T>, ITaskPort<T>
      where T:ITaskPortType
	{

		public override void main()
		{
		}

		public override void after_initialize ()
		{
			foreach (KeyValuePair<int,IDictionary<string,int>> rrr in this.UnitSizeInFacet)
				foreach (KeyValuePair<string,int> sss in rrr.Value)
					Trace.WriteLineIf(this.TraceFlag==true,  this.ThisFacetInstance + "/" + this.Rank  + ": : TASK PORT --- facet_instance=" + rrr.Key + " / unit_id=" + sss.Key + " / size=" + sss.Value + " --- " + this.CID.getInstanceName());
		}

		public new bool TraceFlag 
		{
			get {
				return base.TraceFlag;
			}
			set {
				base.TraceFlag = value;
				Channel.TraceFlag = base.TraceFlag;
			}
		}

		//private MPI.Intercommunicator channel;

		private RequestList synchronize_action(object action_id)
		{
			Console.WriteLine ("ActionDef.action_ids.Count = " + ActionDef.action_ids.Count);

			int value = ActionDef.action_ids[action_id];
			RequestList request_list = new RequestList ();

			Trace.WriteLineIf(this.TraceFlag==true, this.ThisFacetInstance + "/" + this.Rank  + ": synchronize_action " + action_id + " -1");

			Trace.WriteLineIf(this.TraceFlag==true, this.ThisFacetInstance + "/" + this.Rank  + ": synchronize_action " + action_id + " 0");

			foreach (KeyValuePair<int,IDictionary<string,int>> facet in Channel.UnitSizeInFacet)
				if (facet.Key != this.ThisFacetInstance)
					foreach (KeyValuePair<string,int> unit_team in facet.Value) 
						for (int i=0; i < unit_team.Value; i++)
						{
							Trace.WriteLineIf(this.TraceFlag==true, "synchronize_action " + action_id + " LOOP SEND " + facet.Key + "/" + i);
							Request req = Channel.ImmediateSend<object> (value, new Tuple<int, int> (facet.Key, i), value);
							request_list.Add (req);
						}
			
			Trace.WriteLineIf(this.TraceFlag==true, this.ThisFacetInstance + "/" + this.Rank  + ": synchronize_action " + action_id + " 1");

			foreach (KeyValuePair<int,IDictionary<string,int>> facet in Channel.UnitSizeInFacet)
				if (facet.Key != this.ThisFacetInstance)
					foreach (KeyValuePair<string,int> unit_team in facet.Value) 
						for (int i=0; i < unit_team.Value; i++)
						{
							Trace.WriteLineIf(this.TraceFlag==true, "synchronize_action " + action_id + " LOOP RECV " + facet.Key + "/" + i);
							ReceiveRequest req = Channel.ImmediateReceive<object> (new Tuple<int, int> (facet.Key, i), value);
							request_list.Add (req);
						}

			Trace.WriteLineIf(this.TraceFlag==true, this.ThisFacetInstance + "/" + this.Rank  + ": synchronize_action " + action_id + " 2");
			return request_list;

		}

		#region ITaskPort implementation

		public void invoke (object action_id)
		{
			object invoke_lock;
			if (!action_lock.TryGetValue (action_id, out invoke_lock)) 
			{
				invoke_lock = new object ();
				action_lock [action_id] = invoke_lock;
			}

			lock (invoke_lock)
			{
				Trace.WriteLineIf (this.TraceFlag == true, this.ThisFacetInstance + "/" + this.Rank + ": INVOKE SYNC " + action_id + " BEFORE LOCK");
				RequestList request_list = synchronize_action (action_id);

				Trace.WriteLineIf (this.TraceFlag == true, this.ThisFacetInstance + "/" + this.Rank + ": INVOKE SYNC " + action_id + " BEFORE WAIT ALL");
				request_list.WaitAll ();
				Trace.WriteLineIf (this.TraceFlag == true, this.ThisFacetInstance + "/" + this.Rank + ": INVOKE SYNC " + action_id + " AFTER WAIT ALL");
			}
		}

		private IDictionary<object, object> action_lock = new Dictionary<object,object>();


		public void invoke (object action_id, out IActionFuture future)
		{
			Trace.WriteLineIf(this.TraceFlag==true,  this.ThisFacetInstance + "/" + this.Rank  + ": INVOKE FUTURE " + action_id + " 0");

			RequestList request_list = synchronize_action (action_id);

			Trace.WriteLineIf(this.TraceFlag==true,  this.ThisFacetInstance + "/" + this.Rank  + ": INVOKE FUTURE " + action_id + " 1");

			ManualResetEvent sync = new ManualResetEvent (false);

			ActionFuture future_ = new ActionFuture(request_list, sync);
			future = future_;

			Trace.WriteLineIf(this.TraceFlag==true,  this.ThisFacetInstance + "/" + this.Rank  + ": INVOKE FUTURE " + action_id + " 2");

			Thread t = new Thread(new ThreadStart(() => handle_request(future_, sync)));

			Trace.WriteLineIf(this.TraceFlag==true,  this.ThisFacetInstance + "/" + this.Rank  + ": INVOKE FUTURE " + action_id + " 3");

			t.Start();

			Trace.WriteLineIf(this.TraceFlag==true,  this.ThisFacetInstance + "/" + this.Rank  + ": INVOKE FUTURE " + action_id + " 4");
		}

		void handle_request (ActionFuture future, ManualResetEvent sync)
		{
			Trace.WriteLineIf (this.TraceFlag == true, this.ThisFacetInstance + "/" + this.Rank  + ": HANDLE REQUEST 1");
			future.RequestList.WaitAll ();
			Trace.WriteLineIf (this.TraceFlag == true, this.ThisFacetInstance + "/" + this.Rank  + ": HANDLE REQUEST 2");
			sync.Set ();
			Trace.WriteLineIf (this.TraceFlag == true, this.ThisFacetInstance + "/" + this.Rank  + ": HANDLE REQUEST 3");
			future.setCompleted ();
			Trace.WriteLineIf (this.TraceFlag == true, this.ThisFacetInstance + "/" + this.Rank  + ": HANDLE REQUEST 4");
		}

		void handle_request (ActionFuture future, ManualResetEvent sync, Action reaction)
		{
			handle_request (future, sync);
			reaction ();
		}

		public Thread invoke (object action_id, Action reaction, out IActionFuture future)
		{
			Trace.WriteLineIf(this.TraceFlag==true,  this.ThisFacetInstance + "/" + this.Rank  + ": INVOKE ACTION " + action_id + " 0");

			RequestList request_list = synchronize_action (action_id);

			Trace.WriteLineIf(this.TraceFlag==true,  this.ThisFacetInstance + "/" + this.Rank  + ": INVOKE ACTION " + action_id + " 1");

			ManualResetEvent sync = new ManualResetEvent (false);

			ActionFuture future_ = new ActionFuture(request_list, sync);
			future = future_;

			Trace.WriteLineIf(this.TraceFlag==true,  this.ThisFacetInstance + "/" + this.Rank  + ": INVOKE ACTION " + action_id + " 2");

			Thread t = new Thread(new ThreadStart(() => handle_request(future_, sync, reaction)));

			t.Start();

			Trace.WriteLineIf(this.TraceFlag==true,  this.ThisFacetInstance + "/" + this.Rank  + ": INVOKE ACTION " + action_id + " 3");

			return t;
		}

		#endregion
	}
	
    internal class ActionFuture : IActionFuture
	{
		private RequestList request_list = null;
	    private ManualResetEvent sync = null;
		private bool completed = false;

		public ActionFuture (RequestList request_list)
		{
			this.request_list = request_list;
		}

		public ActionFuture (RequestList request_list, ManualResetEvent sync)
		{
			this.request_list = request_list;
			this.sync = sync;
		}

		#region ActionFuture implementation
		public void wait ()
		{
			if (!completed)
				sync.WaitOne ();
		}
		public bool test ()
		{
			return completed;
		}

		public IActionFutureSet createSet()
		{
			IActionFutureSet afs = new ActionFutureSet ();
			afs.addAction (this);
			return afs;
		}

		#endregion

		public object waiting_lock = new object ();

		public void setCompleted()
		{
			lock (waiting_lock) 
			{
				completed = true;
				foreach (AutoResetEvent waiting_set in waiting_sets)
					waiting_set.Set ();
			}
		}

		private IList<AutoResetEvent> waiting_sets = new List<AutoResetEvent> ();

		public RequestList RequestList { get { return request_list; } } 

		public void registerWaitingSet(AutoResetEvent waiting_set)
		{
			lock (waiting_lock) 
			{
				if (completed)
					waiting_set.Set ();
				waiting_sets.Add (waiting_set);
			}
		}

		public void unregisterWaitingSet(AutoResetEvent waiting_set)
		{
			waiting_sets.Remove (waiting_set);
		}
	}

	internal class ActionFutureSet : IActionFutureSet
	{
		IList<IActionFuture> pending_list = new List<IActionFuture>();

		#region ActionFutureSet implementation
		public void addAction (IActionFuture new_future)
		{								
			lock (sync_oper)
				pending_list.Add (new_future);
				
			if (sync_future != null)
				new_future.registerWaitingSet (sync_future);
		}

		public void waitAll ()
		{
			foreach (IActionFuture action_future in pending_list) 
				lock(sync_oper)	action_future.wait ();			

			pending_list.Clear ();
		}

		AutoResetEvent sync_future = null;

		public IActionFuture waitAny ()
		{
			sync_future = new AutoResetEvent(false);

			lock (sync_oper)
				foreach (IActionFuture action_future in pending_list) 
						action_future.registerWaitingSet (sync_future);	
				
			sync_future.WaitOne ();
			sync_future = null;

			IActionFuture f = this.testAny ();

			lock (sync_oper)
				foreach (IActionFuture action_future in pending_list) 
					action_future.unregisterWaitingSet (sync_future);	


		/*	while (true)
			{
				Thread.Sleep (200);
				f = testAny();
				if (f != null)
					return f;
			}*/


			return f;
		} 
		 
		private object sync_oper = new object (); 

		public bool testAll ()
		{
			lock (sync_oper) 
			{
				bool completed = true;
				IList<IActionFuture> tobeRemoved = new List<IActionFuture> ();

				foreach (IActionFuture action_future in pending_list) 
				{
					bool one_completed = action_future.test ();
					if (one_completed)
						tobeRemoved.Add (action_future);
					completed = completed && one_completed;
				}

				foreach (IActionFuture f in tobeRemoved) 
					pending_list.Remove (f);
			
				return completed;
			}
		}

		public IActionFuture testAny ()
		{
			IActionFuture completed_action_future = null;

			lock (sync_oper) 
			{				
				foreach (IActionFuture action_future in pending_list) 
				{
					if (action_future.test ()) 
					{
						completed_action_future = action_future;
						break;
					}
				}
				pending_list.Remove (completed_action_future);
			}

			return completed_action_future;
		}

		public IActionFuture[] Pending 
		{
			get 
			{
				lock (sync_oper) 
				{				
					IActionFuture[] f = new IActionFuture[pending_list.Count];
					pending_list.CopyTo (f, 0);
					return f;
				}
			}
		}
		#endregion
	}
}
