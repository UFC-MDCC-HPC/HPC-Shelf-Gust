/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;

namespace br.ufc.mdcc.hpcshelf.gust.connector.JoinLoop
{
	public interface BaseIJoinLoop<M1, TF, IKey, IValue, OKey, OValue, BF, GIF> : ISynchronizerKind 
		where M1:IMaintainer
		where TF:ITerminateFunction<IKey, IValue, OKey, OValue>
		where IKey:IData
		where IValue:IData
		where OKey:IData
		where OValue:IData
		where BF:IPartitionFunction<IKey>
		where GIF:IInputFormat
	{
//		ITaskPort<ITaskPortTypeAdvance> Task_binding_split_next { get; }
//		IClientBase<IPortTypeIterator> Collect_pairs { get; }
//		IClientBase<IPortTypeIterator> Collect_graph { get; }
//
//		ITaskPort<ITaskPortTypeAdvance> Task_binding_split_first {get;}
//		IServerBase<IPortTypeIterator> Feed_pairs { get; }
//		IServerBase<IPortTypeIterator> Feed_graph { get; }
	}
}