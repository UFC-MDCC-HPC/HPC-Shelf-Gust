using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskPortType;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using System.Collections.Generic;
using System.Threading;

namespace br.ufc.mdcc.hpc.storm.binding.task.impl.TaskBindingBaseImpl
{
	public class TaskPort<T> : BaseTaskPort<T>, ITaskPort<T>
      where T:ITaskPortType
	{
		public override void main()
		{
		}
		public static void writeFile(string PATH, string saida, bool manter){ 
			using (System.IO.StreamWriter file = new System.IO.StreamWriter (@PATH, manter)) {
				file.WriteLine (saida);
			}
		}
		
		public override void after_initialize ()
		{
			int remote_leader = this.Id_unit.Equals("peer_right") ? this.UnitRanks ["peer_left"] [0] : this.UnitRanks ["peer_right"] [0];
			channel = new MPI.Intercommunicator(this.PeerComm, 0, this.Communicator, remote_leader, 0);
			writeFile ("./LOG-TaskPort.TXT","INICIO TaskPort"+
				" remote_leader:"+remote_leader+
				" this.Rank:"+this.Rank+
				" this.PeerRank:"+this.PeerRank+
				" this.PeerSize:"+this.PeerSize, false);
		}

		private MPI.Intercommunicator channel;

		#region ITaskPort implementation

		public void invoke (object action_id)
		{
			int partner_size = channel.RemoteSize;
			int value = ActionDef.action_ids[action_id];

			MPI.RequestList request_list = new MPI.RequestList ();

			for (int i=0; i<partner_size; i++) 
			{
				MPI.Request req = channel.ImmediateSend<object>(value, i, value);
				request_list.Add (req);
			}

			for (int i=0; i<partner_size; i++) 
			{
				MPI.ReceiveRequest req = channel.ImmediateReceive<object>(i, value);
				request_list.Add (req);
			}

			Console.WriteLine (channel.Rank + ": BEFORE WAIT ALL");
			request_list.WaitAll ();
			Console.WriteLine (channel.Rank + ": AFTER WAIT ALL");
		}

		public void invoke (object action_id, out IActionFuture future)
		{
			int value = ActionDef.action_ids[action_id];
			int partner_size = channel.RemoteSize;

			MPI.RequestList request_list = new MPI.RequestList ();

			for (int i=0; i<partner_size; i++) 
			{
				MPI.Request req = channel.ImmediateSend<object>(value, i, value);
				request_list.Add (req);
			}

			for (int i=0; i<partner_size; i++) 
			{
				MPI.ReceiveRequest req = channel.ImmediateReceive<object>(i, value);
				request_list.Add (req);
			}

			ManualResetEvent sync = new ManualResetEvent (false);

			ActionFuture future_ = new ActionFuture(request_list);
			future = future_;

			Thread t = new Thread(new ThreadStart(() => handle_request(future_, sync)));

			t.Start();
		}

		void handle_request (ActionFuture future, ManualResetEvent sync)
		{
			future.RequestList.WaitAll ();
			sync.Set ();
			future.setCompleted ();
		}

		void handle_request (ActionFuture future, ManualResetEvent sync, Action reaction)
		{
			handle_request (future, sync);
			reaction ();
		}

		public Thread invoke (object action_id, Action reaction, out IActionFuture future)
		{
			int partner_size = channel.RemoteSize;
			int value = ActionDef.action_ids[action_id];

			MPI.RequestList request_list = new MPI.RequestList ();

			for (int i=0; i<partner_size; i++) 
			{
				MPI.Request req = channel.ImmediateSend<object>(value, i, value);
				request_list.Add (req);
			}

			for (int i=0; i<partner_size; i++) 
			{
				MPI.ReceiveRequest req = channel.ImmediateReceive<object>(i, value);
				request_list.Add (req);
			}

			ManualResetEvent sync = new ManualResetEvent (false);

			ActionFuture future_ = new ActionFuture(request_list, sync);
			future = future_;

			Thread t = new Thread(new ThreadStart(() => handle_request(future_, sync, reaction)));

			t.Start();

			writeFile ("./LOG-TaskPort.TXT","End TaskPort"+
				" this.Rank:"+this.Rank+
				" this.PeerRank:"+this.PeerRank
				+" this.PeerSize:"+this.PeerSize, true);

			return t;
		}

		#endregion
	}
	
    internal class ActionFuture : IActionFuture
	{
		private MPI.RequestList request_list = null;
	    private ManualResetEvent sync = null;
		private bool completed = false;

		public ActionFuture (MPI.RequestList request_list)
		{
			this.request_list = request_list;
		}

		public ActionFuture (MPI.RequestList request_list, ManualResetEvent sync)
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

		public void setCompleted()
		{
			completed = true;
		}

		public MPI.RequestList RequestList { get { return request_list; } } 
	}

	internal class ActionFutureSet : IActionFutureSet
	{
		IList<IActionFuture> pending_list = new List<IActionFuture>();
		IList<IActionFuture> completed_list = new List<IActionFuture>();

		#region ActionFutureSet implementation
		public void addAction (IActionFuture new_future)
		{			
			pending_list.Add (new_future);
		}
		public void waitAll ()
		{
			foreach (IActionFuture action_future in pending_list)
				action_future.wait ();

			foreach (IActionFuture action_future in pending_list) 
				completed_list.Add (action_future);
			pending_list.Clear ();
		}


		public IActionFuture waitAny ()
		{
			while (true) 
			{
				IActionFuture f = this.testAny ();
				if (f != null)
					return f;
			}

		}
		 
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
				{
					pending_list.Remove (f);
					completed_list.Add (f);
				}
			
				return completed;
			}
		}

		public IActionFuture testAny ()
		{
			lock (sync_oper) 
			{
				foreach (IActionFuture action_future in pending_list) 
				{
					if (action_future.test ()) 
					{
						pending_list.Remove (action_future);
						completed_list.Add (action_future);
						return action_future;
					}
				}
			}

			return null;
		}
		public IActionFuture[] Completed 
		{
			get 
			{
				IActionFuture[] f = new IActionFuture[completed_list.Count];
				completed_list.CopyTo (f, 0);
				return f;
			}
		}

		private object sync_oper = new object (); 

		public IActionFuture[] Pending 
		{
			get 
			{
				IActionFuture[] f = new IActionFuture[pending_list.Count];
				pending_list.CopyTo (f, 0);
				return f;
			}
		}
		#endregion
	}
}
