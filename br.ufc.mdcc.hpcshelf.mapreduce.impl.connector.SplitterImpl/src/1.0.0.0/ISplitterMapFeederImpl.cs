using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.connector.Splitter;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using System.Collections.Generic;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using System.Diagnostics;
using System.Threading;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.connector.SplitterImpl {
	public class ISplitterMapFeederImpl<M1,IKey,IValue,GIF>: BaseISplitterMapFeederImpl<M1,IKey,IValue,GIF>, ISplitterMapFeeder<M1,IKey,IValue,GIF>
		where M1:IMaintainer
		where IKey:IData
		where IValue:IData
        where GIF:IInputFormat {
		static private int TAG_SPLIT_NEW_CHUNK = 246;
		static private int TAG_SPLIT_END_CHUNK = 247;

		private IIteratorInstance<IKVPair<IKey,IValue>> output_instance = null;
		private IIteratorInstance<IKVPair<IInteger,GIF>> output_instance_gif = null;

		public override void main () {
			this.readSource ();
			this.iterate ();
		}

		public void readSource () {
			Console.WriteLine (this.GlobalRank + ": STARTING SPLITTER READSOURCE...1");
			output_instance_gif = (IIteratorInstance<IKVPair<IInteger,GIF>>)Output_gif.Instance;
			Feed_graph.Server = output_instance_gif;
			Console.WriteLine (this.GlobalRank + ": STARTING SPLITTER READSOURCE...2");

			// RECEIVE PAIR FROM THE SOURCE (1st iteration)
			Tuple<int,int> unit_ref_source = new Tuple<int,int> (this.FacetIndexes [FACET_SOURCE] [0], 0);

			//	Thread[] threads_receive = new Thread[senders_size];

			//	for (int i = 0; i < senders_size; i++) 
			//	{
			//		threads_receive [i] = new Thread ((ParameterizedThreadStart)delegate(object unit_ref_obj) { 
			//			Tuple<int,int> unit_ref_i = (Tuple<int,int>)unit_ref_obj;
			//			receive_pairs_iteration (unit_ref_i);
			//		});
			//	}

			// TODO: READ_SOURCE é necessário ? Não no map feeder. Tirar fatia de Task_binding data ...

			Task_binding_data.TraceFlag = true;
			Task_binding_data.invoke (ITaskPortData.READ_SOURCE);
			// Do nothing ... 


			IList<IKVPairInstance<IInteger,GIF>> buffer;
			object buffer_obj;

			CompletedStatus status;


			do {
				IActionFuture sync_perform;

				Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER - BEFORE READ_CHUNK");

				Task_binding_split_first.invoke (ITaskPortAdvance.READ_CHUNK);  //****
				Task_binding_split_first.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

				Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER !!! PERFORM OK !");

				Split_channel.Receive (unit_ref_source, MPI.Communicator.anyTag, out buffer, out status);

				Console.WriteLine (this.Rank + ": CHUNK PAIRS RECEIVED !!! from source buffer.Count=" + buffer.Count);

				foreach (IKVPairInstance<IInteger,GIF> kv in buffer)
					output_instance_gif.put (kv);

				output_instance_gif.finish ();

				sync_perform.wait ();

				// CHUNK_READY
				Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER 1");

				Task_binding_split_first.invoke (ITaskPortAdvance.CHUNK_READY);

				Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER 2");
			} while (status.Tag != TAG_SPLIT_END_CHUNK);

			Console.WriteLine (this.Rank + ": FINISH READING CHUNKS OF SOURCE");
		}

		public void iterate () {
			IList<IKVPairInstance<IKey,IValue>> buffer;
			object buffer_obj;
			CompletedStatus status;

			Console.WriteLine (this.GlobalRank + ": STARTING SPLITTER ITERATE...1");
			output_instance = (IIteratorInstance<IKVPair<IKey,IValue>>)Output.Instance;
			Feed_pairs.Server = output_instance;
			Console.WriteLine (this.GlobalRank + ": STARTING SPLITTER ITERATE...2");

			IDictionary<int,Tuple<int,int>> unit_ref = new Dictionary<int, Tuple<int,int>> ();
			Console.WriteLine (this.GlobalRank + ": STARTING SPLITTER ...3");
			// RECEIVE PAIRS FROM THE REDUCERS (next iterations)
			int senders_size = 0;
			foreach (int i in this.FacetIndexes[FACET_REDUCE]) {   
				Console.WriteLine (this.GlobalRank + ": STARTING SPLITTER ...4 -- i=" + i);
				int nr0 = senders_size;
				senders_size += this.UnitSizeInFacet [i] ["reduce_collector"];
				for (int k = 0, j = nr0; j < senders_size; k++, j++)
					unit_ref [j] = new Tuple<int,int> (i, k);
			}

			bool end_computation = false;
			while (!end_computation) {
				end_computation = true;
				int count_finished_streams = 0;

				while (count_finished_streams < senders_size) {
					IActionFuture sync_perform;

					Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER NEXT 1");

					Task_binding_split_next.invoke (ITaskPortAdvance.READ_CHUNK);
					Task_binding_split_next.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

					Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER NEXT 2");

					for (int i = 0; i < senders_size; i++) {					
						Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER NEXT LOOP - before receive");

						Split_channel.Receive (unit_ref [i], MPI.Communicator.anyTag, out buffer_obj, out status);

						Console.WriteLine (this.Rank + ": CHUNK PAIRS RECEIVED !!! from reduce_collector of index " + i + " / count_finished_streams=" + count_finished_streams);

						if (status.Tag == TAG_SPLIT_END_CHUNK)
							count_finished_streams++;
						else
							end_computation = false;
						
						try {
							buffer = (IList<IKVPairInstance<IKey,IValue>>)buffer_obj;

							foreach (IKVPairInstance<IKey,IValue> kv in buffer)
								output_instance.put (kv);
						} catch (InvalidCastException e) {
							Console.WriteLine ("SPLITTER MAPPER: incompatible input !");
							count_finished_streams++;
						}
					}

					output_instance.finish ();

					Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER NEXT 3");

					sync_perform.wait ();

					Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER NEXT 4");

					// CHUNK_READY
					Task_binding_split_next.invoke (ITaskPortAdvance.CHUNK_READY);

					Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER NEXT 5");
				}
			}

			Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER FINISH 1 !");

			Task_binding_data.invoke (ITaskPortData.TERMINATE);
			Task_binding_data.invoke (ITaskPortData.WRITE_SINK);

			Console.WriteLine (this.Rank + ": SPLITTER MAP FEEDER FINISH 2 !");
		}


		//private Object thisLock = new Object();

		//private void receive_pairs_iteration(Tuple<int,int> unit_ref)
		//{
		//	IList<IKVPairInstance<IKey,IValue>> buffer;
		//	CompletedStatus status;
		//
		//	while (true)
		//	{
		//		lock (thisLock)
		//		{
		//			Task_port_split_first.invoke (ITaskPortAdvance.PERFORM);
		//			Split_channel.Receive (unit_ref, MPI.Communicator.anyTag, out buffer, out status);
		//
		//			Console.WriteLine ("CHUNK PAIRS RECEIVED !!! ");
		//
		//			foreach (IKVPairInstance<IKey,IValue> kv in buffer)
		//				output_instance.put (kv);
		//
		//			if (status.Tag == TAG_SPLIT_PAIR_FINISH)
		//				count_finished_streams++;
		//
		//			output_instance.finish ();
		//
		//			// CHUNK_READY
		//			Task_port_split_first.invoke (ITaskPortAdvance.CHUNK_READY);
		//		}
		//	}
		//}
	}
}
