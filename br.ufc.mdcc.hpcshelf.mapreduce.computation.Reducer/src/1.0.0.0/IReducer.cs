using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.ReduceFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.computation.Reducer
{
	public interface IReducer<M, RF, PType, TKey, TValue, OKey, OValue, G> : BaseIReducer<M, RF, PType, TKey, TValue, OKey, OValue, G>
		where M:IMaintainer
		where RF:IReduceFunction<PType, TKey, TValue, OKey, OValue, G>
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where PType:IData
		where G:IData
	{
	}
}