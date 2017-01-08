/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.ReduceFunction;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.computation.Reducer
{
	public interface BaseIReducer<M, RF, PType, TKey, TValue, OKey, OValue, G> : IComputationKind 
		where M:IMaintainer
		where RF:IReduceFunction<PType, TKey, TValue, OKey, OValue, G>
		where OKey:IData
		where OValue:IData
		where TKey:IData
		where TValue:IData
		where PType:IData
		where G:IData
	{
	}
}