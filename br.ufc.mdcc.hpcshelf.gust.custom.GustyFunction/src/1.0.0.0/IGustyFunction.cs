using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction
{
	public interface IGustyFunction<TKey, TValue, OKey, OValue, PType, G> : BaseIGustyFunction<TKey, TValue, OKey, OValue, PType, G>
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where PType:IData
		where G:IData
	{
		void Compute();
		void Optimize();
		void InputGraph();
	}
}