/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.ReduceFunction;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.computation.Reducer
{
	public interface BaseIReducer<M, RF, GIF, TKey, TValue, OKey, OValue, G> : IComputationKind 
		where M:IMaintainer
		where RF:IReduceFunction<GIF, TKey, TValue, OKey, OValue, G>
		where GIF:IInputFormat
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where G:IData
	{
	}
}