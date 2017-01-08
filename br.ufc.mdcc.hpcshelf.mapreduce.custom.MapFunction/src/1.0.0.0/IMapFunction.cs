using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.mapreduce.custom.MapFunction { 

	public interface IMapFunction<IKey, IValue, TKey, TValue> : BaseIMapFunction<IKey, IValue, TKey, TValue>
		where IKey:IData
		where IValue:IData
		where TKey:IData
		where TValue:IData
{


} // end main interface 

} // end namespace 
