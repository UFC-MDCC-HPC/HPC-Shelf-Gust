using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.connector.Join;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using System.Collections.Generic;
using br.ufc.mdcc.common.Integer;
using System.Threading;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;

namespace br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl
{
	public class IJoinGustyCollectorImpl<M0,IKey, IValue, OKey, OValue, BF, TF> : BaseIJoinGustyCollectorImpl<M0,IKey, IValue, OKey, OValue, BF, TF>, IJoinGustyCollector<M0,IKey, IValue, OKey, OValue, BF, TF>
		where M0:IMaintainer
		where IKey:IData
		where IValue:IData
		where OKey:IData
		where OValue:IData
		where BF:IPartitionFunction<IKey>
		where TF:ITerminateFunction<IKey,IValue,OKey,OValue>
	{
		static private int TAG_SPLIT_NEW_CHUNK = 246;
		static private int TAG_SPLIT_END_CHUNK = 247;
		static private int TAG_SPLIT_END_COMPUTATION = 247;

		private void terminate_go()
		{
			Terminate_function.go ();
		}

		public override void main()
		{
			Console.WriteLine (this.Rank + ": SPLITTER REDUCE COLLECTOR START");

			IIteratorInstance<IKVPair<OKey,OValue>> input_instance = (IIteratorInstance<IKVPair<OKey,OValue>>) Collect_pairs.Client;
			Terminate_function.Iterate_pairs = input_instance;

			Thread thread_terminate_function = new Thread (new ThreadStart(terminate_go));
			thread_terminate_function.Start ();

			Thread thread_send_to_mappers = new Thread (new ThreadStart (send_to_mappers));
			thread_send_to_mappers.Start ();

			thread_terminate_function.Join ();
			thread_send_to_mappers.Join ();

			Console.WriteLine (this.Rank + ": SPLITTER REDUCE COLLECTOR FINISH");

		//	Thread thread_send_to_sink = new Thread (new ThreadStart (send_to_sink));
		//	thread_send_to_sink.Start ();

		}
			
		private static int CHUNK_SIZE = 50;

		private void send_to_mappers ()
		{
			Console.WriteLine (this.Rank + ": IJoinGustyCollector 1");

			IIteratorInstance<IKVPair<OKey,OValue>> output_instance = (IIteratorInstance<IKVPair<OKey,OValue>>) Output_pairs.Instance;
			IIteratorInstance<IKVPair<IKey,IValue>> input_instance = (IIteratorInstance<IKVPair<IKey,IValue>>) Input_pairs.Instance;

			object bin_object = null;

			// DETERMINE COMMUNICATION TARGETs
			Tuple<int,int> sink_ref = new Tuple<int,int> (this.FacetIndexes [FACET_SINK] [0], 0);

			IDictionary<int,Tuple<int,int>> unit_ref = new Dictionary<int, Tuple<int,int>> ();
			int m_size = 0;
			foreach (int i in this.FacetIndexes[FACET_MAP]) 
			{   
				int nr0 = m_size;
				m_size += this.UnitSizeInFacet [i] ["map_feeder"];
				for (int k=0, j=nr0; j < m_size; k++, j++) 
					unit_ref [j] = new Tuple<int,int> (i/*,0 index of MAP_FEEDER*/,k);
			}

			Console.WriteLine (this.Rank + ": IJoinGustyCollector 2");

			IActionFuture sync_perform;

			bool end_computation = false;
			while (!end_computation) // new iteration
			{    
				Console.WriteLine (this.Rank + ": IJoinGustyCollector LOOP");
				end_computation = true;

				//Console.WriteLine (this.Rank + ": IJoinGustyCollector ITERATE 1");
				//Task_binding_split_next.invoke (ITaskPortAdvance.READ_CHUNK);  //****
				//Task_binding_split_next.invoke (ITaskPortAdvance.PERFORM, out sync_perform);
				//Console.WriteLine (this.Rank + ": IJoinGustyCollector ITERATE 2");

				// SEND TO SINK

				IList<IKVPairInstance<OKey,OValue>> buffer_sink = new List<IKVPairInstance<OKey,OValue>>();

				end_computation = !input_instance.has_next();

				Thread thread_send_output = new Thread(new ThreadStart(delegate {
					int count1 = 0;
					while (output_instance.fetch_next (out bin_object)) 
					{
						IKVPairInstance<OKey,OValue> item = (IKVPairInstance<OKey,OValue>)bin_object;
						buffer_sink.Add (item);
						if (count1 % CHUNK_SIZE == 0) 
						{
							// PERFORM
							Console.WriteLine (this.Rank + ": IJoinGustyCollector SINK SEND CHUNK 3-1 count=" + count1);
							Split_channel.Send (buffer_sink, sink_ref, TAG_SPLIT_NEW_CHUNK);
							Console.WriteLine (this.Rank + ": IJoinGustyCollector SINK SEND CHUNK 3-2 count=" + count1);
							buffer_sink.Clear ();
						}
						count1++;
					}
					if (buffer_sink.Count >0)
					{
						Console.WriteLine (this.Rank + ": IJoinGustyCollector SINK SEND CHUNK 3-3 count=" + count1);
						Split_channel.Send (buffer_sink, sink_ref, TAG_SPLIT_NEW_CHUNK);
						Console.WriteLine (this.Rank + ": IJoinGustyCollector SINK SEND CHUNK 3-4 count=" + count1);
					}
				}));

				thread_send_output.Start();

				// SEND BACK TO MAPPER (new iteration)

				Bin_function_iterate.NumberOfPartitions = m_size;

				IList<IKVPairInstance<OKey,OValue>>[] buffer = new IList<IKVPairInstance<OKey,OValue>>[m_size];
				for (int i = 0; i < m_size; i++)
					buffer [i] = new List<IKVPairInstance<OKey,OValue>> ();

				Console.WriteLine (this.Rank + ": IJoinGustyCollector ITERATE 3 end_computation=" + end_computation);

				int count = 0;
				while (input_instance.fetch_next (out bin_object)) 
				{
					Console.WriteLine (this.Rank + ": IJoinGustyCollector ITERATE LOOP 4-1 count=" + count);

					IKVPairInstance<OKey,OValue> item = (IKVPairInstance<OKey,OValue>)bin_object;

					this.Input_key_iterate.Instance = item.Key;
					Bin_function_iterate.go ();
					int index = ((IIntegerInstance)this.Output_key_iterate.Instance).Value;

					buffer [index].Add (item);

					if (count % CHUNK_SIZE == 0) 
					{
						Task_binding_split_next.invoke (ITaskPortAdvance.READ_CHUNK);
						Task_binding_split_next.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

						for (int i = 0; i < m_size; i++) 
						{
							Console.WriteLine ("SPLITTER REDUCE COLLECTOR - Sending chunk of " + buffer[i].Count + " elements");
							Split_channel.Send (buffer [i], unit_ref [i], TAG_SPLIT_NEW_CHUNK);
							buffer [i].Clear();
						}
						sync_perform.wait ();
						Task_binding_split_next.invoke (ITaskPortAdvance.CHUNK_READY);
						Console.WriteLine (this.Rank + ": IJoinGustyCollector ITERATE 3-5");
					}

					Console.WriteLine (this.Rank + ": IJoinGustyCollector ITERATE LOOP 4-2 count=" + count);

					count++;
				}

				Console.WriteLine (this.Rank + ": IJoinGustyCollector ITERATE 5");

				Task_binding_split_next.invoke (ITaskPortAdvance.READ_CHUNK);
				Task_binding_split_next.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

				// SEND REMAINING PAIRS AND CLOSES THE CHUNK LIST
				Console.WriteLine (this.Rank + ": IJoinGustyCollector ITERATE 6-1");
				for (int i = 0; i < m_size; i++) 
					Split_channel.Send (buffer [i], unit_ref [i], TAG_SPLIT_END_CHUNK);

				sync_perform.wait ();

				Console.WriteLine (this.Rank + ": IJoinGustyCollector ITERATE 7-1");
				Task_binding_split_next.invoke (ITaskPortAdvance.CHUNK_READY);

				Console.WriteLine (this.Rank + ": IJoinGustyCollector ITERATE 7-2");
				thread_send_output.Join();

				//sync_perform.wait ();

				Console.WriteLine (this.Rank + ": IJoinGustyCollector ITERATE 7-3");
			}

			Console.WriteLine (this.Rank + ": IJoinGustyCollector END COMPUTATION ");

			input_instance.finish ();
			output_instance.finish ();
			Split_channel.Send (new List<IKVPairInstance<OKey,OValue>>(), sink_ref, TAG_SPLIT_END_CHUNK);
		}
	}
}
