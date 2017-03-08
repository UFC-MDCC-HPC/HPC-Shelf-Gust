using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.computation.Gusty
{
	public interface IGusty<M, RF, GIF, TKey, TValue, OKey, OValue, G> : BaseIGusty<M, RF, GIF, TKey, TValue, OKey, OValue, G>
		where M:IMaintainer
		where RF:IGustyFunction<GIF, TKey, TValue, OKey, OValue, G>
		where GIF:IInputFormat
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where G:IData
	{
	}
}