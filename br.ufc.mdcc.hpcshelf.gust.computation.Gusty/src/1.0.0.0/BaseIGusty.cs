/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Integer;
//using br.ufc.mdcc.hpcshelf.gust.binding.environment.BindingDirectMxNIterator;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;
//using br.ufc.mdcc.hpcshelf.platform.Platform;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction;

namespace br.ufc.mdcc.hpcshelf.gust.computation.Gusty
{
	public interface BaseIGusty<M, GF, TKey, TValue, OKey, OValue, PType, G> : IComputationKind 
		where M:IMaintainer
		where GF:IGustyFunction<TKey, TValue, OKey, OValue, PType, G>
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where PType:IData
		where G:IData
	{
		IIterator<IKVPair<OKey, OValue>> Output {get;}
		IKVPair<IInteger, PType> Partition_graph {get;}
		G Graph {get;}
		IServerBase<IPortTypeIterator> Feed_pairs {get;}
		IClientBase<IPortTypeIterator> Collect_pairs {get;}
		ITaskPort<ITaskPortTypeAdvance> Task_gusty {get;}
		OValue Continue_value {get;}
		IKVPair<TKey, IIterator<TValue>> Input_values {get;}
		//IProcessingNode<M> Platform_gusty {get;}
	}
}