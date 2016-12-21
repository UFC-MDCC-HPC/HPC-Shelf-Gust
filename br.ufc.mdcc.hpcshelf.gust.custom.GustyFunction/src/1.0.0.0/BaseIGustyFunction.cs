/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
//using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction
{
	public interface BaseIGustyFunction<TKey, TValue, OKey, OValue, PType, G> : IComputationKind 
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where PType:IData
		where G:IData
	{
		IKVPair<TKey, IIterator<TValue>> Input_values {get;}
		//G Graph {get;}
		//OValue Continuation_value {get;}
		//IKVPair<IInteger, PType> Partition_graph {get;}
		IIterator<IKVPair<OKey, OValue>> Output {get;}
	}
}