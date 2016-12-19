/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Iterator;


namespace br.ufc.mdcc.hpcshelf.gust.custom.MapFunction { 

public interface BaseIMapFunction<IKey, IValue, TKey, TValue> : IComputationKind 
		where IKey:IData
		where IValue:IData
		where TKey:IData
		where TValue:IData
{

	IIterator<IKVPair<TKey,TValue>> Output_data {get;}
	IKey Input_key {get;}
	IValue Input_value {get;}


} // end main interface 

} // end namespace 
