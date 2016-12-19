using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.MapFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.computation.Mapper
{
	public interface IMapper<M,IKey, IValue, TKey, TValue, MF> : BaseIMapper<M,IKey, IValue, TKey, TValue, MF>
	    where M:IMaintainer
		where MF:IMapFunction<IKey, IValue, TKey, TValue>
		where IKey:IData
		where IValue:IData
		where TKey:IData
		where TValue:IData
	{
	}
}