/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction
{
	public interface BaseITerminateFunction<IKey, IValue, OKey, OValue> : IComputationKind 
		where IKey:IData
		where IValue:IData
		where OKey:IData
		where OValue:IData
	{	
		IIterator<IKVPair<OKey,OValue>> Output_pairs {get;}
		IIterator<IKVPair<IKey,IValue>> Input_pairs {get;}
	}
}