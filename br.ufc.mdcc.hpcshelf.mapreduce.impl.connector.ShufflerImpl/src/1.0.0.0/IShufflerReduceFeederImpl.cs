using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.connector.Shuffler;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using System.Collections.Generic;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using System.Diagnostics;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.connector.ShufflerImpl
{
	public class IShufflerReduceFeederImpl<M0,TKey, TValue,GIF> : BaseIShufflerReduceFeederImpl<M0,TKey, TValue,GIF>, IShufflerReduceFeeder<M0,TKey, TValue,GIF>
		where M0:IMaintainer
		where TKey:IData
		where TValue:IData
		where GIF:IInputFormat
	{
		static private int TAG_SHUFFLE_OMV_NEW_CHUNK = 345;
		static private int TAG_SHUFFLE_OMV_END_CHUNK = 347;

		public void receive_write_gif (int senders_size, IDictionary<int,Tuple<int,int>> unit_ref) {
			IIteratorInstance<IKVPair<IInteger,IIterator<GIF>>> output_instance_gifs = (IIteratorInstance<IKVPair<IInteger,IIterator<GIF>>>)Output_gifs.Instance;
			Feed_graph.Server = output_instance_gifs;

			bool[] finished_stream = new bool[senders_size];
			for (int i = 0; i < senders_size; i++)
				finished_stream [i] = false;

			int count_finished_streams = 0;

			while (count_finished_streams < senders_size) {     // take next chunk ... 
				IActionFuture sync_perform;

				Task_binding_shuffle.invoke (ITaskPortAdvance.READ_CHUNK);
				Task_binding_shuffle.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

				IDictionary<object,IIteratorInstance<GIF>> kv_cache = new Dictionary<object,IIteratorInstance<GIF>> ();

				// PERFORM
				for (int i = 0; i < senders_size; i++) {
					if (!finished_stream [i]) {
						IList<IKVPairInstance<IInteger,GIF>> buffer;
						CompletedStatus status;
						Shuffler_channel.Receive (unit_ref [i], MPI.Communicator.anyTag, out buffer, out status);

						foreach (IKVPairInstance<IInteger,GIF> kv in buffer) {	
							IIteratorInstance<GIF> iterator = null;
							if (!kv_cache.ContainsKey (kv.Key)) {
								iterator = Value_factory_gif.newIteratorInstance ();
								kv_cache.Add (kv.Key, iterator);
								IKVPairInstance<IInteger,IIterator<GIF>> item = (IKVPairInstance<IInteger,IIterator<GIF>>)Output_gifs.createItem ();
								item.Key = kv.Key;
								item.Value = iterator;
								output_instance_gifs.put (item);
							} else
								kv_cache.TryGetValue (kv.Key, out iterator);

							iterator.put (kv.Value);
						}

						if (status.Tag == TAG_SHUFFLE_OMV_END_CHUNK) {
							count_finished_streams++;
							finished_stream [i] = true;
						} 
					}	
				}

				output_instance_gifs.finish ();
				foreach (IIteratorInstance<GIF> iterator in kv_cache.Values)
					iterator.finish ();

				sync_perform.wait ();

				// CHUNK_READY
				Task_binding_shuffle.invoke (ITaskPortAdvance.CHUNK_READY);   //****
			}
			output_instance_gifs.finish ();
			output_instance_gifs.finish ();
		}

		public override void main()
		{
			Console.WriteLine (this.Rank + ": SHUFFLER REDUCE COLLECTOR START");

//			IIteratorInstance<IKVPair<TKey,IIterator<TValue>>> output_instance = (IIteratorInstance<IKVPair<TKey,IIterator<TValue>>>) Output.Instance;
//			Feed_pairs.Server = output_instance;

			// DETERMINE COMMUNICATION SOURCEs
			IDictionary<int,Tuple<int,int>> unit_ref = new Dictionary<int, Tuple<int,int>> ();
			int nf = this.FacetMultiplicity [FACET_MAP];
			int senders_size = 0;
			foreach (int i in this.FacetIndexes[FACET_MAP]) 
			{   
				int nr0 = senders_size;
				senders_size += this.UnitSizeInFacet [i] ["map_collector"];
				for (int k=0, j=nr0; j < senders_size; k++, j++) 
					unit_ref [j] = new Tuple<int,int> (i/*,0 INDEX OF map_collector*/,k);
			}

			receive_write_gif (senders_size, unit_ref);

			IIteratorInstance<IKVPair<TKey,IIterator<TValue>>> output_instance = (IIteratorInstance<IKVPair<TKey,IIterator<TValue>>>) Output.Instance;
			Feed_pairs.Server = output_instance;

			bool end_computation = false;
			while (!end_computation)   // next iteration
			{
				bool[] finished_stream = new bool[senders_size];
				for (int i = 0; i < senders_size; i++)
					finished_stream [i] = false;

				int count_finished_streams = 0;

				end_computation = true;

				while (count_finished_streams < senders_size)     // take next chunk ... 
				{  
					Console.WriteLine (this.GlobalRank + ": SHUFFLER REDUCER...1");

					IActionFuture sync_perform;

					Task_binding_shuffle.invoke (ITaskPortAdvance.READ_CHUNK);
					Task_binding_shuffle.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

					Console.WriteLine (this.GlobalRank + ": SHUFFLER REDUCER...2");

					IDictionary<object,IIteratorInstance<TValue>> kv_cache = new Dictionary<object,IIteratorInstance<TValue>> ();

					// PERFORM
					for (int i = 0; i < senders_size; i++) 
					{
						if (!finished_stream[i])
						{
							Console.WriteLine (this.GlobalRank + ": SHUFFLER REDUCER...3 i=" + i);

							IList<IKVPairInstance<TKey,TValue>> buffer;
							CompletedStatus status;
							Shuffler_channel.Receive (unit_ref[i], MPI.Communicator.anyTag, out buffer, out status);

							Console.WriteLine (this.GlobalRank + ": SHUFFLER REDUCER...4");

							foreach (IKVPairInstance<TKey,TValue> kv in buffer)
							{	
								IIteratorInstance<TValue> iterator = null;
								if (!kv_cache.ContainsKey (kv.Key)) 
								{
									iterator = Value_factory.newIteratorInstance ();
									kv_cache.Add (kv.Key, iterator);
									IKVPairInstance<TKey,IIterator<TValue>> item = (IKVPairInstance<TKey,IIterator<TValue>>)Output.createItem ();
									item.Key = kv.Key;
									item.Value = iterator;
									output_instance.put (item);
								} 
								else 
									kv_cache.TryGetValue (kv.Key, out iterator);

								iterator.put (kv.Value);
							}

							Console.WriteLine (this.GlobalRank + ": SHUFFLER REDUCER...5");

							if (status.Tag == TAG_SHUFFLE_OMV_END_CHUNK) 
							{
								count_finished_streams++;
								finished_stream [i] = true;
							} 
							else
								end_computation = false;

							Console.WriteLine (this.GlobalRank + ": SHUFFLER REDUCER...6");
						}	
					}

					output_instance.finish ();
					foreach (IIteratorInstance<TValue> iterator in kv_cache.Values) 
						iterator.finish ();

					Console.WriteLine (this.GlobalRank + ": SHUFFLER REDUCER...7-1");

					sync_perform.wait ();

					Console.WriteLine (this.GlobalRank + ": SHUFFLER REDUCER...7-2");

					// CHUNK_READY
					Task_binding_shuffle.invoke (ITaskPortAdvance.CHUNK_READY);   //****

					Console.WriteLine (this.GlobalRank + ": SHUFFLER REDUCER...8");
				}

				output_instance.finish ();

				Console.WriteLine (this.GlobalRank + ": SHUFFLER REDUCER...9 - FINISH");

			}
		}
	}
}
