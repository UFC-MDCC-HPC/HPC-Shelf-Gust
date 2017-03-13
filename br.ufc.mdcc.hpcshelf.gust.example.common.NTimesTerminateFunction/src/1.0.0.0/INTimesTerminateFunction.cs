using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;

namespace br.ufc.mdcc.hpcshelf.gust.example.common.NTimesTerminateFunction
{
	public interface INTimesTerminateFunction<IKey, IValue, OKey, OValue> : BaseINTimesTerminateFunction<IKey, IValue, OKey, OValue>, ITerminateFunction<IKey, IValue, OKey, OValue>
		where IKey:IData
		where IValue:IData
		where OKey:IData
		where OValue:IData
	{
	}
}