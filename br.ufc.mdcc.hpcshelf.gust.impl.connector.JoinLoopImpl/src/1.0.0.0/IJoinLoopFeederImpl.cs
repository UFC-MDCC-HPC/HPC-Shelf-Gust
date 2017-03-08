using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.connector.JoinLoop;
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
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;

namespace br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinLoopImpl {
	public class IJoinLoopFeederImpl<M1,IKey,IValue,GIF>: BaseIJoinLoopFeederImpl<M1,IKey,IValue,GIF>, IJoinLoopFeeder<M1,IKey,IValue,GIF>
		where M1:IMaintainer
		where IKey:IData
		where IValue:IData
        where GIF:IInputFormat {
		static private int TAG_SPLIT_NEW_CHUNK = 246;
		static private int TAG_SPLIT_END_CHUNK = 247;

		private IIteratorInstance<IKVPair<IKey,IIterator<IValue>>> output_instance = null;
		private IIteratorInstance<IKVPair<IInteger,IIterator<GIF>>> output_instance_gifs = null;

		public override void main () {
			this.readSource ();
			this.iterate ();
		}
		public void readSource () {
			output_instance_gifs = (IIteratorInstance<IKVPair<IInteger,IIterator<GIF>>>)Output_gifs.Instance;
			Feed_graph.Server = output_instance_gifs;

			// RECEIVE PAIR FROM THE SOURCE (1st iteration)
			Tuple<int,int> unit_ref_source = new Tuple<int,int> (this.FacetIndexes [FACET_SOURCE] [0], 0);

			Task_binding_data.TraceFlag = true;
			Task_binding_data.invoke (ITaskPortData.READ_SOURCE);
			// Do nothing ... 

			IList<IKVPairInstance<IInteger,GIF>> buffer;
			object buffer_obj;

			CompletedStatus status;

			do {
				IActionFuture sync_perform;

				Task_binding_split_first.invoke (ITaskPortAdvance.READ_CHUNK);  //****
				Task_binding_split_first.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

				IDictionary<object,IIteratorInstance<GIF>> kv_cache = new Dictionary<object,IIteratorInstance<GIF>> ();

				Split_channel.Receive (unit_ref_source, MPI.Communicator.anyTag, out buffer, out status);

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

				output_instance_gifs.finish ();
				foreach (IIteratorInstance<IValue> iterator in kv_cache.Values) 
					iterator.finish ();

				sync_perform.wait ();

				// CHUNK_READY
				Task_binding_split_first.invoke (ITaskPortAdvance.CHUNK_READY);

			} while (status.Tag != TAG_SPLIT_END_CHUNK);

			output_instance_gifs.finish ();
		}

		public void iterate () {
			IList<IKVPairInstance<IKey,IValue>> buffer;
			object buffer_obj;
			CompletedStatus status;

			output_instance = (IIteratorInstance<IKVPair<IKey,IIterator<IValue>>>)Output.Instance;
			Feed_pairs.Server = output_instance;

			IDictionary<int,Tuple<int,int>> unit_ref = new Dictionary<int, Tuple<int,int>> ();
			// RECEIVE PAIRS FROM THE REDUCERS (next iterations)
			int senders_size = 0;
			foreach (int i in this.FacetIndexes[FACET_REDUCE]) {   
				int nr0 = senders_size;
				senders_size += this.UnitSizeInFacet [i] ["join_loop"];
				for (int k = 0, j = nr0; j < senders_size; k++, j++)
					unit_ref [j] = new Tuple<int,int> (i, k);
			}

			bool end_computation = false;
			while (!end_computation) {
				end_computation = true;
				int count_finished_streams = 0;

				while (count_finished_streams < senders_size) {
					IActionFuture sync_perform;

					Task_binding_split_next.invoke (ITaskPortAdvance.READ_CHUNK);
					Task_binding_split_next.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

					IDictionary<object,IIteratorInstance<IValue>> kv_cache = new Dictionary<object,IIteratorInstance<IValue>> ();

					for (int i = 0; i < senders_size; i++) {					
						Split_channel.Receive (unit_ref [i], MPI.Communicator.anyTag, out buffer_obj, out status);

						if (status.Tag == TAG_SPLIT_END_CHUNK)
							count_finished_streams++;
						else
							end_computation = false;
						
						try {
							buffer = (IList<IKVPairInstance<IKey,IValue>>)buffer_obj;

							foreach (IKVPairInstance<IKey,IValue> kv in buffer){
								IIteratorInstance<IValue> iterator = null;
								if (!kv_cache.ContainsKey (kv.Key)) 
								{
									iterator = Value_factory.newIteratorInstance ();
									kv_cache.Add (kv.Key, iterator);
									IKVPairInstance<IKey,IIterator<IValue>> item = (IKVPairInstance<IKey,IIterator<IValue>>)Output.createItem ();
									item.Key = kv.Key;
									item.Value = iterator;
									output_instance.put (item);
								} 
								else 
									kv_cache.TryGetValue (kv.Key, out iterator);

								iterator.put (kv.Value);
							}

						} catch (InvalidCastException e) {
							count_finished_streams++;
						}
					}

					output_instance.finish ();
					foreach (IIteratorInstance<IValue> iterator in kv_cache.Values) 
						iterator.finish ();

					sync_perform.wait ();

					// CHUNK_READY
					Task_binding_split_next.invoke (ITaskPortAdvance.CHUNK_READY);
				}
			}
			Task_binding_data.invoke (ITaskPortData.TERMINATE);
			Task_binding_data.invoke (ITaskPortData.WRITE_SINK);
		}
	}
}
