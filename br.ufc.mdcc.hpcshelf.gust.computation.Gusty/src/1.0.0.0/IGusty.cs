using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.computation.Gusty
{
	public interface IGusty<M, GF, TKey, TValue, OKey, OValue, PType, G> : BaseIGusty<M, GF, TKey, TValue, OKey, OValue, PType, G>
		where M:IMaintainer
		where GF:IGustyFunction<TKey, TValue, OKey, OValue, PType, G>
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where PType:IData
		where G:IData
	{
	}
}