using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.connector.Splitter;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using System.Collections.Generic;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Iterator;
using System.Diagnostics;
using br.ufc.mdcc.common.Data;
using System.Threading;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeData;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.connector.SplitterImpl
{
	public class ISplitterWriteSinkImpl<M3,OKey,OValue> : BaseISplitterWriteSinkImpl<M3,OKey,OValue>, ISplitterWriteSink<M3,OKey,OValue>
		where M3:IMaintainer
		where OKey:IData
		where OValue:IData
	{
		static private int TAG_SPLIT_NEW_CHUNK = 246;
		static private int TAG_SPLIT_END_CHUNK = 247;

		public override void main()
		{
			Split_channel.TraceFlag = true;
			Task_binding_data.TraceFlag = true;
			Console.WriteLine ("SPLITER WRITE SINK 0");

			Sink.clientConnection (); 
			IPortTypeIterator output_instance = (IPortTypeIterator) Sink.Client;

			Console.WriteLine ("SPLITER WRITE SINK 1");

			Task_binding_data.TraceFlag = true;
			Task_binding_data.invoke (ITaskPortData.READ_SOURCE);
			// Do nothing ... 


			Console.WriteLine ("SPLITER WRITE SINK 3");

			// RECEIVE PAIRS FROM THE REDUCERS (next iterations)
			IDictionary<int,Tuple<int,int>> unit_ref = new Dictionary<int, Tuple<int,int>> ();
			unit_ref = new Dictionary<int, Tuple<int,int>> ();
			int senders_size = 0;
			foreach (int i in this.FacetIndexes[FACET_REDUCE]) 
			{   
				int nr0 = senders_size;
				senders_size += this.UnitSizeInFacet [i] ["reduce_collector"];
				for (int k=0, j=nr0; j < senders_size; k++, j++) 
					unit_ref [j] = new Tuple<int,int> (i,k);
			}				

			Console.WriteLine ("SPLITER WRITE SINK 4");

			Thread t_output = new Thread (new ThreadStart (delegate 
									{
										Task_binding_data.invoke (ITaskPortData.TERMINATE);
										Task_binding_data.invoke (ITaskPortData.WRITE_SINK);
										Sink.startWriteSink ();
									}));

			t_output.Start ();

			Thread[] receive_pairs_iteration_thread = new Thread[senders_size];

			for (int i = 0; i < senders_size; i++)
			{
				receive_pairs_iteration_thread [i] = new Thread (new ParameterizedThreadStart (delegate(object i_obj) {
					int ii = (int) i_obj;
					receive_pairs_iteration (output_instance, senders_size, unit_ref[ii]);
				}));

				receive_pairs_iteration_thread [i].Start (i);
			}

			for (int i = 0; i < senders_size; i++)
				receive_pairs_iteration_thread [i].Join ();

			output_instance.finish ();

			t_output.Join ();
			Console.WriteLine ("SPLITER WRITE SINK - WRITTER FINISH");

			Split_channel.TraceFlag = false;
			Task_binding_data.TraceFlag = false;

		}

		private object cs_recv = new object();

		private void receive_pairs_iteration(IPortTypeIterator output_instance, int senders_size, Tuple<int,int> unit_ref)
		{
			CompletedStatus status;

			do 
			{
				Console.WriteLine ("SPLITER WRITE SINK 7-1 facet=" + unit_ref.Item1 + "/rank=" + unit_ref.Item2);
				IList<IKVPairInstance<OKey,OValue>> buffer;
				lock (cs_recv) Split_channel.Receive (unit_ref, MPI.Communicator.anyTag, out buffer, out status);
				foreach (IKVPairInstance<OKey,OValue> kv in buffer)
					output_instance.put (kv);
				Console.WriteLine ("SPLITER WRITE SINK 7-2 facet=" + unit_ref.Item1 + "/rank=" + unit_ref.Item2);
			} while (status.Tag == TAG_SPLIT_NEW_CHUNK);


			Console.WriteLine ("SPLITER WRITE SINK - OUTPUT RECEIVED");
		}
	}
}
