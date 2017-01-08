/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.custom.ReduceFunction
{
	public interface BaseIReduceFunction<GIF, TKey, TValue, OKey, OValue, G> : IComputationKind 
		where GIF:IData
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where G:IData
	{
		IKVPair<TKey, IIterator<TValue>> Input_values {get;}
		IIterator<IKVPair<OKey, OValue>> Output {get;}	
		IIterator<IKVPair<IInteger,GIF>> Graph_format {get;}
	}
}