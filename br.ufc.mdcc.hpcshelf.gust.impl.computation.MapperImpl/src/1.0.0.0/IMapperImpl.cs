	using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.MapFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.computation.Mapper;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using System.Diagnostics;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;

namespace br.ufc.mdcc.hpcshelf.gust.impl.computation.MapperImpl
{
	public class IMapperImpl<M,IKey, IValue, TKey, TValue, MF> : BaseIMapperImpl<M,IKey, IValue, TKey, TValue, MF>, IMapper<M,IKey, IValue, TKey, TValue, MF>
		where M:IMaintainer
		where MF:IMapFunction<IKey, IValue, TKey, TValue>
		where IKey:IData
		where IValue:IData
		where TKey:IData
		where TValue:IData
	{
		public override void main()
		{
			Console.WriteLine (this.Rank + ": STARTING MAPPER ...1");
			IIteratorInstance<IKVPair<IKey,IValue>> input_instance = (IIteratorInstance<IKVPair<IKey,IValue>>)Collect_pairs.Client;
			Console.WriteLine (this.Rank + ": STARTING MAPPER ...2 " + (input_instance == null));
			IIteratorInstance<IKVPair<TKey,TValue>> output_instance = (IIteratorInstance<IKVPair<TKey,TValue>>)Output.Instance;
			Console.WriteLine (this.Rank + ": STARTING MAPPER ...3");
			Feed_pairs.Server = output_instance;

			IActionFuture sync_perform;
		

			bool end_computation = false;
			while (!end_computation) // next iteration
			{  
				bool finished_stream = false;

				Console.WriteLine (this.Rank + ": STARTING MAPPER ...4");

				end_computation = true;

				while (!finished_stream)  // take next chunk ...
				{        
					Console.WriteLine (this.Rank + ": LOOP CHUNK MAPPER ... 1");

					Task_map.invoke (ITaskPortAdvance.READ_CHUNK);
					Task_map.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

					Console.WriteLine (this.Rank + ": LOOP CHUNK MAPPER ... 2");

					object bin_object = null;

					if (!input_instance.has_next ())
						finished_stream = true;
					else
						end_computation = false;

					Console.WriteLine (this.Rank + ": LOOP CHUNK MAPPER ... 3");

					IKVPairInstance<IKey,IValue> bin;
					// READ_CHUNK / PERFORM
					while (input_instance.fetch_next (out bin_object)) 
					{
						bin = (IKVPairInstance<IKey,IValue>)bin_object;
						Map_key.Instance = bin.Key;
						Map_value.Instance = bin.Value;
						Map_function.go ();
						Console.WriteLine (this.Rank + ": LOOP CHUNK MAPPER ... 4");
					}

					sync_perform.wait ();

					Console.WriteLine (this.Rank + ": LOOP CHUNK MAPPER ... 5");

					output_instance.finish ();

					Console.WriteLine (this.Rank + ": LOOP CHUNK MAPPER ... 6");

					Task_map.invoke (ITaskPortAdvance.CHUNK_READY);  //****
					/* levando em conta que há sincronização pelos iteradores, talvez não haja necessidade de sincronizar o CHUNK_READY para o
				     * step começar a ler os pares */
					Console.WriteLine (this.Rank + ": LOOP CHUNK MAPPER ... 7");
				}

				Console.WriteLine (this.Rank + ": LOOP CHUNK MAPPER ... FINISH ITERATION 1");
			}

			Console.WriteLine (this.Rank + ": MAPPER FINISH !");
		}
	}
}
