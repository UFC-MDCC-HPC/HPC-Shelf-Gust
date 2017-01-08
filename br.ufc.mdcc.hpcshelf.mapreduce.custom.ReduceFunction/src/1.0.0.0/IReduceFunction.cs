using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Iterator;

namespace br.ufc.mdcc.hpcshelf.mapreduce.custom.ReduceFunction
{
	public interface IReduceFunction<PType, TKey, TValue, OKey, OValue, G> : BaseIReduceFunction<PType, TKey, TValue, OKey, OValue, G>
		where PType:IData
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where G:IData
	{
		void Compute();
		void Optimize();
		void InputGraph();
	}
}