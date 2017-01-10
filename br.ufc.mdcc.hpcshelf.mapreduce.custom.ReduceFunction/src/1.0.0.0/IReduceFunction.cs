using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.custom.ReduceFunction
{
	public interface IReduceFunction<GIF, TKey, TValue, OKey, OValue, G> : BaseIReduceFunction<GIF, TKey, TValue, OKey, OValue, G>
		where GIF:IInputFormat//Data
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