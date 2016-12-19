using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction
{
	public interface ITerminateFunction<IKey, IValue, OKey, OValue> : BaseITerminateFunction<IKey, IValue, OKey, OValue>
		where IKey:IData
		where IValue:IData
		where OKey:IData
		where OValue:IData
	{	
		IPortTypeIterator Iterate_pairs { set; }
	}
}