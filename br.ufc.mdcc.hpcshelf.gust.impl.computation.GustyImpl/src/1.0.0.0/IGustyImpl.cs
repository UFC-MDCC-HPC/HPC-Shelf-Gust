using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.computation.Gusty;

using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
//using br.ufc.mdcc.common.Integer;
//using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
//using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;

namespace br.ufc.mdcc.hpcshelf.gust.impl.computation.GustyImpl
{
	public class IGustyImpl<M, GF, TKey, TValue, OKey, OValue, PType, G> : BaseIGustyImpl<M, GF, TKey, TValue, OKey, OValue, PType, G>, IGusty<M, GF, TKey, TValue, OKey, OValue, PType, G>
where M:IMaintainer
where GF:IGustyFunction<TKey, TValue, OKey, OValue, PType, G>
where TKey:IData
where TValue:IData
where OKey:IData
where OValue:IData
where PType:IData
where G:IData
	{
		public override void main() {
			/* 1. Ler pares chave (TKey) e valores (TValue) de Input.
             * 2. Para cada par, atribuir a Key e Values e chamar Gusty_function.go();
             * 3. Pegar o resultado de Reduction_function.go() de Output_gusty (OValue) 
             *    e colocar no iterator Output.
             */
			readPair_OMK_OMVs(); //startThreads();
		}

		private void readPair_OMK_OMVs() {
			//Console.WriteLine (this.Rank + ": REDUCE 1");

			IIteratorInstance<IKVPair<TKey, IIterator<TValue>>> collect_pairs_client_iterator_kv_input_instance = (IIteratorInstance<IKVPair<TKey, IIterator<TValue>>>)Collect_pairs.Client;
			IIteratorInstance<IKVPair<OKey,OValue>> feed_pairs_server_iterator_kv_output_instance = (IIteratorInstance<IKVPair<OKey,OValue>>)Output.Instance;
			Feed_pairs.Server = feed_pairs_server_iterator_kv_output_instance;

			IActionFuture sync_perform;

			// TODO: Dividir em chunks a saa de pares (OKey,OValue)

			//Console.WriteLine (this.Rank + ": REDUCER 2");

//			object gusty_lock = new object ();

			bool end_computation = false;
			while (!end_computation) { // new iteration
				IDictionary<object,object> cont_dict = new Dictionary<object, object> ();

				//Console.WriteLine (this.Rank + ": REDUCER LOOP");

				end_computation = true;

				bool end_iteration = false;
				while (!end_iteration) { // take next chunk ...          
					//Console.WriteLine (this.Rank + ": REDUCER ITERATE 1");

					Task_gusty.invoke (ITaskPortAdvance.READ_CHUNK);
					Task_gusty.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

					//Console.WriteLine (this.Rank + ": REDUCER ITERATE 2");

					IKVPairInstance<TKey, IIterator<TValue>> kvpair = null;
					object kvpair_object;

					if (!collect_pairs_client_iterator_kv_input_instance.has_next ())
						end_iteration = true;
					else
						end_computation = false;

					int count=0;
					while (collect_pairs_client_iterator_kv_input_instance.fetch_next (out kvpair_object)) {
						//Console.WriteLine (this.Rank + ": REDUCER ITERATE INNER LOOP 3 count=" + count);

						kvpair = (IKVPairInstance<TKey, IIterator<TValue>>)kvpair_object;

						object acc_value;
						if (!cont_dict.TryGetValue(kvpair.Key, out acc_value))
							cont_dict[kvpair.Key] = new object();
						else
							((IDataInstance)Continue_value.Instance).ObjValue = acc_value;
						
						Input_values.Instance = kvpair; 
						Gusty_function.go ();				
						cont_dict [kvpair.Key] = ((IDataInstance)((IKVPairInstance<OKey,OValue>)Output_value.Instance).Value).ObjValue;

						count++;
					}
					//Console.WriteLine (this.Rank + ": REDUCER ITERATE 4 count=" + count);
					sync_perform.wait ();
					//Console.WriteLine (this.Rank + ": REDUCER ITERATE 5");
				}
				//Console.WriteLine (this.Rank + ": REDUCER ITERATE END ITERATION");

				int chunk_counter = 1;

				IActionFuture gusty_chunk_ready;
				Task_gusty.invoke (ITaskPortAdvance.CHUNK_READY, out gusty_chunk_ready);  //***

				foreach (KeyValuePair<object,object> output_pair in cont_dict) {					
					IKVPairInstance<OKey,OValue> new_pair = (IKVPairInstance<OKey,OValue>) Output_value.newInstance ();
					new_pair.Key = output_pair.Key;
					new_pair.Value = output_pair.Value;
					feed_pairs_server_iterator_kv_output_instance.put (new_pair);	 
				}
				feed_pairs_server_iterator_kv_output_instance.finish ();
				gusty_chunk_ready.wait ();
				//Console.WriteLine (this.Rank + ": REDUCER ITERATE FINISH");
			}
			//Console.WriteLine (this.Rank + ": REDUCER FINISH ... ");
		}


//		private void startThreads() {
//			/*Instancias*/
//			Thread treadPairOMKOMV = new Thread(new ThreadStart(readPair_OMK_OMVs));
//
//			/*Starting*/
//			treadPairOMKOMV.Start(); 	
//			/* Joins*/
//			treadPairOMKOMV.Join();
//		}
	}
}
