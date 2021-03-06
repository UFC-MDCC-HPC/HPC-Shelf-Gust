/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.gust.connector.Join
{
	public interface BaseIJoinGustyFeeder<M1,IKey, IValue, GIF> : ISynchronizerKind 
		where M1: IMaintainer
		where IKey:IData
		where IValue:IData
		where GIF:IInputFormat
	{
		ITaskPort<ITaskPortTypeAdvance> Task_binding_split_first {get;}
		ITaskPort<ITaskPortTypeAdvance> Task_binding_split_next {get;}
		IServerBase<IPortTypeIterator> Feed_pairs { get; }
		IServerBase<IPortTypeIterator> Feed_graph { get; }
	}
}