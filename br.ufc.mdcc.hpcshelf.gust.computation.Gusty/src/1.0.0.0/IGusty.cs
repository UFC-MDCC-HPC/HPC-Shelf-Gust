using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.computation.Gusty
{
	public interface IGusty<M,TKey, TValue, OKey, OValue, RF> : BaseIGusty<M,TKey, TValue, OKey, OValue, RF>
		where M:IMaintainer
		where RF:IGustyFunction<TKey, TValue, OKey, OValue>
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
	{
	}
}