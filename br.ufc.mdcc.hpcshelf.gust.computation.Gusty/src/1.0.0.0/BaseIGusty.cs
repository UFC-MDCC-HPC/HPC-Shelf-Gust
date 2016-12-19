/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.computation.Gusty
{
	public interface BaseIGusty<M,OKey, OValue, TKey, TValue, RF> : IComputationKind 
		where M:IMaintainer
		where RF:IGustyFunction<OKey, OValue, TKey, TValue>
		where OKey:IData
		where OValue:IData
		where TKey:IData
		where TValue:IData
	{
	}
}