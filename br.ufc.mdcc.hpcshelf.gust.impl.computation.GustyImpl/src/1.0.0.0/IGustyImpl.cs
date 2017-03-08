using System;
using System.Reflection;
using System.Linq;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.computation.Gusty;
using System.Diagnostics;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using System.Threading;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using System.Collections.Generic;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;

namespace br.ufc.mdcc.hpcshelf.gust.impl.computation.GustyImpl
{
	public class IGustyImpl<M, RF, GIF, TKey, TValue, OKey, OValue, G> : BaseIGustyImpl<M, RF, GIF, TKey, TValue, OKey, OValue, G>, IGusty<M, RF, GIF, TKey, TValue, OKey, OValue, G>
		where M:IMaintainer
		where RF:IGustyFunction<GIF, TKey, TValue, OKey, OValue, G>
		where GIF:IInputFormat
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where G:IData
	{
		public override void main() {
			/* 1. Ler pares chave (TKey) e valores (TValue) de Input.
             * 2. Para cada par, atribuir a Key e Values e chamar Gusty_function.go();
             * 3. Pegar o resultado de Reduction_function.go() de Output_gusty (OValue) 
             *    e colocar no iterator Output.
             */
			graph_creator ();
			readPair_OMK_OMVs();
		}
		private void graph_creator () {
			IIteratorInstance<IKVPair<IInteger, IIterator<GIF>>> input_instance_gif = (IIteratorInstance<IKVPair<IInteger, IIterator<GIF>>>)Collect_graph.Client;
			IIteratorInstance<IKVPair<IInteger,GIF>> output_instance_gif = (IIteratorInstance<IKVPair<IInteger,GIF>>)Output_gif.Instance;
			Feed_graph.Server = output_instance_gif;

			IActionFuture sync_perform;

			bool end_iteration = false;
			while (!end_iteration) {    // take next chunk ...
				Task_gusty.invoke (ITaskPortAdvance.READ_CHUNK);
				Task_gusty.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

				IKVPairInstance<IInteger, IIterator<GIF>> kvpair = null;
				object kvpair_object;
				object o;

				if (!input_instance_gif.has_next ())
					end_iteration = true;

				while (input_instance_gif.fetch_next (out kvpair_object)) {
					kvpair = (IKVPairInstance<IInteger, IIterator<GIF>>)kvpair_object;
					Graph_values.Instance = kvpair;
					Gusty_function.graph_creator ();
				}
				sync_perform.wait ();
			}
			//IActionFuture gusty_chunk_ready;
			//Task_gusty.invoke (ITaskPortAdvance.CHUNK_READY, out gusty_chunk_ready);  //***
			output_instance_gif.finish ();
			//gusty_chunk_ready.wait ();
		}

		private void readPair_OMK_OMVs()
		{
			Console.WriteLine (this.Rank + ": REDUCE 1");

			//IIteratorInstance<IKVPair<TKey, IIterator<TValue>>> input_instance = (IIteratorInstance<IKVPair<TKey, IIterator<TValue>>>)Collect_pairs.Client;
			IIteratorInstance<IKVPair<OKey,OValue>> output_instance = (IIteratorInstance<IKVPair<OKey,OValue>>)Output.Instance;
			Feed_pairs.Server = output_instance;

			Gusty_function.startup_push();
			IActionFuture gusty_chunk_ready_startup;
			Task_gusty.invoke (ITaskPortAdvance.CHUNK_READY, out gusty_chunk_ready_startup);
			output_instance.finish ();
			gusty_chunk_ready_startup.wait ();

			IIteratorInstance<IKVPair<TKey, IIterator<TValue>>> input_instance = (IIteratorInstance<IKVPair<TKey, IIterator<TValue>>>)Collect_pairs.Client;

			IList<MethodInfo> phases = getGustMethods (Gusty_function); 
			IEnumerator<MethodInfo> current_phase = phases.GetEnumerator ();

			IActionFuture sync_perform;

			// TODO: Dividir em chunks a saa de pares (OKey,OValue)

			Console.WriteLine (this.Rank + ": REDUCER 2");

//			object gusty_lock = new object ();

			bool end_computation = false;
			while (!end_computation)    // new iteration
			{
				if (!current_phase.MoveNext ()) {
					current_phase = phases.GetEnumerator ();
					current_phase.MoveNext ();
				}
				MethodInfo phase = current_phase.Current;
				//IDictionary<object,object> cont_dict = new Dictionary<object, object> ();

				Console.WriteLine (this.Rank + ": REDUCER LOOP");

				end_computation = true;

				bool end_iteration = false;
				while (!end_iteration)    // take next chunk ...
				{          
					Console.WriteLine (this.Rank + ": REDUCER ITERATE 1");

					Task_gusty.invoke (ITaskPortAdvance.READ_CHUNK);
					Task_gusty.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

					Console.WriteLine (this.Rank + ": REDUCER ITERATE 2");

					IKVPairInstance<TKey, IIterator<TValue>> kvpair = null;
					object kvpair_object;

					if (!input_instance.has_next ())
						end_iteration = true;
					else
						end_computation = false;

					int count=0;
					while (input_instance.fetch_next (out kvpair_object)) 
					{
						Console.WriteLine (this.Rank + ": REDUCER ITERATE INNER LOOP 3 count=" + count);

						kvpair = (IKVPairInstance<TKey, IIterator<TValue>>)kvpair_object;

//						object acc_value;
//						if (!cont_dict.TryGetValue(kvpair.Key, out acc_value))
//							cont_dict[kvpair.Key] = new object();
//						else
//							((IDataInstance)Continue_value.Instance).ObjValue = acc_value;
						
						Input_values.Instance = kvpair; 
						Gusty_function.pull ();
						//Gusty_function.go ();

						//cont_dict [kvpair.Key] = ((IDataInstance)((IKVPairInstance<OKey,OValue>)Output_value.Instance).Value).ObjValue;

						count++;
					}

					Console.WriteLine (this.Rank + ": REDUCER ITERATE 4 count=" + count);

					sync_perform.wait ();

					Console.WriteLine (this.Rank + ": REDUCER ITERATE 5");
				}

				Console.WriteLine (this.Rank + ": REDUCER ITERATE END ITERATION");

				phase.Invoke(Gusty_function,null);

				IActionFuture gusty_chunk_ready;
				Task_gusty.invoke (ITaskPortAdvance.CHUNK_READY, out gusty_chunk_ready);  //***

//				foreach (KeyValuePair<object,object> output_pair in cont_dict) 
//				{					
//					IKVPairInstance<OKey,OValue> new_pair = (IKVPairInstance<OKey,OValue>) Output_value.newInstance ();
//					new_pair.Key = output_pair.Key;
//					new_pair.Value = output_pair.Value;
//					output_instance.put (new_pair);	 
//				}

				output_instance.finish ();

				gusty_chunk_ready.wait ();

				Console.WriteLine (this.Rank + ": REDUCER ITERATE FINISH");
			}

			Console.WriteLine (this.Rank + ": REDUCER FINISH ... ");
		}
		private static IList<MethodInfo> getGustMethods(object o){
			IList<MethodInfo> gusts_methods = new List<MethodInfo> ();
			IList<MethodInfo> all_methods = new List<MethodInfo> (o.GetType ().GetMethods ());
			for (int i = 0; i < all_methods.Count; i++) {
				MethodInfo m = all_methods.ElementAt (i);
				if (m.Name.Length >= 4) {
					if (m.Name.Substring (0, 4).ToLower().Equals ("gust") && (m.GetParameters ().Length == 0) ) {
						gusts_methods.Add (m);
					}
				}
			}
			return gusts_methods;
		}
	}
}
