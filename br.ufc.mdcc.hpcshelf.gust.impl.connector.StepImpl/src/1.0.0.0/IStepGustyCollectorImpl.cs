using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.gust.connector.Step;
using System.Diagnostics;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using System.Collections.Generic;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;

namespace br.ufc.mdcc.hpcshelf.gust.impl.connector.StepImpl	
{
	public class IStepGustyCollectorImpl<M1,TKey,TValue,PF,GIF> : BaseIStepGustyCollectorImpl<M1,TKey,TValue,PF,GIF>, IStepGustyCollector<M1,TKey,TValue,PF,GIF>
		where M1:IMaintainer
		where PF:IPartitionFunction<TKey>
		where TKey:IData
		where TValue:IData
		where GIF:IInputFormat
	{
		static private int TAG_SHUFFLE_OMV_NEW_CHUNK = 345;
		static private int TAG_SHUFFLE_OMV_END_CHUNK = 347;

		public void read_send_graph (int gusty_size, IDictionary<int,Tuple<int,int>> unit_ref) {
			IIteratorInstance<IKVPair<IInteger,GIF>> input_instance_graph = (IIteratorInstance<IKVPair<IInteger,GIF>>)Collect_graph.Client;

			object bin_object = null;
			IActionFuture sync_perform;

			Partition_function_gif.NumberOfPartitions = gusty_size;

			bool end_iteration_gif = false; bool set_table = true;
			while (!end_iteration_gif) {
				Task_binding_shuffle.invoke (ITaskPortAdvance.READ_CHUNK);
				Task_binding_shuffle.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

				IList<IKVPairInstance<IInteger,GIF>>[] buffer_gif = new IList<IKVPairInstance<IInteger,GIF>>[gusty_size];
				for (int i = 0; i < gusty_size; i++)
					buffer_gif [i] = new List<IKVPairInstance<IInteger,GIF>> ();

				IKVPairInstance<IInteger,GIF> item = null;

				if (!input_instance_graph.has_next ())
					end_iteration_gif = true;

				int count = 0;
				while (input_instance_graph.fetch_next (out bin_object)) {
					item = (IKVPairInstance<IInteger,GIF>)bin_object;
					this.Input_key_gif.Instance = item.Value;//item.Key;
					Partition_function_gif.go ();
					int index = ((IIntegerInstance)this.Output_key_gif.Instance).Value;
					buffer_gif [index].Add (item);
					Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP Collector Graph count=" + (count++));
				}
				// PERFORM
				for (int i = 0; i < gusty_size; i++)
					Step_channel.Send (buffer_gif [i], unit_ref [i], end_iteration_gif ? TAG_SHUFFLE_OMV_END_CHUNK : TAG_SHUFFLE_OMV_NEW_CHUNK);

				sync_perform.wait ();

				Task_binding_shuffle.invoke (ITaskPortAdvance.CHUNK_READY);
			}
			Partition_function.PartitionTABLE = Partition_function_gif.PartitionTABLE;
		}

		public override void main()
		{
			Console.WriteLine (this.GlobalRank + ": STARTING SHUFFLER MAP ...1");
//			IIteratorInstance<IKVPair<TKey,TValue>> input_instance = (IIteratorInstance<IKVPair<TKey,TValue>>) Collect_pairs.Client;
			object bin_object = null;

			IActionFuture sync_perform;

			// DETERMINE COMMMUNICATION TARGETs
			IDictionary<int,Tuple<int,int>> unit_ref = new Dictionary<int, Tuple<int,int>> ();
			int nf = this.FacetMultiplicity [FACET_REDUCE];
			int gusty_size = 0;
			Console.WriteLine (this.GlobalRank + ": STARTING SHUFFLER MAP ...2");
			foreach (int i in this.FacetIndexes[FACET_REDUCE]) 
			{   
				int nr0 = gusty_size;
				Console.WriteLine (this.GlobalRank + ": STARTING SHUFFLER MAP ...3 - BEGIN 1 - i=" + i);
				foreach (KeyValuePair<int,IDictionary<string,int>> ttt in this.UnitSizeInFacet) 
				{
					Console.WriteLine (this.GlobalRank + ": STARTING SHUFFLER MAP ...3 - " + (ttt.Value == null));
					foreach (KeyValuePair<string,int> tttt in ttt.Value)
						Console.WriteLine (this.GlobalRank + ": STARTING SHUFFLER MAP ...3 --- " + ttt.Key + " / " + tttt.Key + " / " + tttt.Value);
				}														 
				gusty_size += this.UnitSizeInFacet [i] ["gusty_feeder"];
				Console.WriteLine (this.GlobalRank + ": STARTING SHUFFLER MAP ...3 - BEGIN 2 - " + i);
				for (int k=0, j=nr0; j < gusty_size; k++, j++) 
					unit_ref [j] = new Tuple<int,int> (i/*,0 INDEX OF gusty_feeder*/,k);
				Console.WriteLine (this.GlobalRank + ": STARTING SHUFFLER MAP ...3 - END - " + i);
			}
			read_send_graph (gusty_size, unit_ref);

			IIteratorInstance<IKVPair<TKey,TValue>> input_instance = (IIteratorInstance<IKVPair<TKey,TValue>>) Collect_pairs.Client;

			Partition_function.NumberOfPartitions = gusty_size;

			bool end_computation = false;
			while (!end_computation) // next iteration
			{   
				end_computation = true;

				bool end_iteration = false;
				while (!end_iteration) // take next chunk ...
				{  
					Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP ...4");

					Task_binding_shuffle.invoke (ITaskPortAdvance.READ_CHUNK);
					Task_binding_shuffle.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

					Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP ...5");

					IList<IKVPairInstance<TKey,TValue>>[] buffer = new IList<IKVPairInstance<TKey,TValue>>[gusty_size];
					for (int i = 0; i < gusty_size; i++)
						buffer [i] = new List<IKVPairInstance<TKey,TValue>> ();
				
					Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP ...6");

					IKVPairInstance<TKey,TValue> item = null;

					if (!input_instance.has_next ())
						end_iteration = true;
					else
						end_computation = false;

					int count = 0;
					while (input_instance.fetch_next (out bin_object)) 
					{
						Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP ...6 count=" + count);
						item = (IKVPairInstance<TKey,TValue>)bin_object;
						this.Input_key.Instance = item.Key;
						Partition_function.go ();
						int index = ((IIntegerInstance)this.Output_key.Instance).Value;
						buffer [index].Add (item);
						Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP ...7 count=" + count);
						count++;
					}

					Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP ...9");
					for (int i=0; i<buffer.Length;i++)
						Console.WriteLine ("SHUFFLER MAP ...9 - buffer[" + i + "]=" + buffer[i].Count);

					// PERFORM
					for (int i = 0; i < gusty_size; i++)
						Step_channel.Send (buffer [i], unit_ref [i], end_iteration ? TAG_SHUFFLE_OMV_END_CHUNK : TAG_SHUFFLE_OMV_NEW_CHUNK);

					Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP ...10");

					sync_perform.wait ();

					Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP ...11");

					Task_binding_shuffle.invoke (ITaskPortAdvance.CHUNK_READY);  //****
					Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP ...12");
				}			

				Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP ...13");
			}		
			Console.WriteLine (this.GlobalRank + ": SHUFFLER MAP ...14");
		}
	}
}
