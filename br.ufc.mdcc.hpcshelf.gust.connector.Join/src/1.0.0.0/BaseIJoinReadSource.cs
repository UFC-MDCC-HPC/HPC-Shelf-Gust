/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.gust.binding.environment.EnvironmentBindingReadData;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSourceInterface;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.gust.connector.Join
{
	//public interface BaseIJoinReadSource<M2, BF, IKey, IValue, GIF> : ISynchronizerKind 
	public interface BaseIJoinReadSource<M2, GIF> : ISynchronizerKind 
		where M2:IMaintainer
		//where BF:IPartitionFunction<GIF>
		//where IKey:IData
		//where IValue:IData
		where GIF:IInputFormat
	{
		ITaskPort<ITaskPortTypeAdvance> Task_binding_split_first {get;}
		ITaskPort<ITaskPortTypeData> Task_binding_data	{ get; } 
		IReadData<IPortTypeDataSourceInterface> Source { get; }
	}
}