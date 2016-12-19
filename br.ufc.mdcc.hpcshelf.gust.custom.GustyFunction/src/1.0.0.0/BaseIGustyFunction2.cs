/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;

namespace br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction
{
	public interface BaseIGustyFunction<TKey, TValue, OKey, OValue> : IComputationKind 
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
	{
		IKVPair<TKey, IIterator<TValue>> Input_values {get;}
		IKVPair<OKey, OValue> Output_value {get;}
	}
}