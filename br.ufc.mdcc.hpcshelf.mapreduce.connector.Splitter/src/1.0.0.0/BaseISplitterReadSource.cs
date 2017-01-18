/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.mapreduce.binding.environment.EnvironmentBindingReadData;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSourceInterface;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.connector.Splitter
{
	public interface BaseISplitterReadSource<M2, BF, IKey, IValue, GIF> : ISynchronizerKind 
		where M2:IMaintainer
		where BF:IPartitionFunction<IKey>
		where IKey:IData
		where IValue:IData
		where GIF:IInputFormat
	{
		ITaskPort<ITaskPortTypeAdvance> Task_binding_split_first {get;}
		ITaskPort<ITaskPortTypeData> Task_binding_data	{ get; } 
		IReadData<IPortTypeDataSourceInterface> Source { get; }
	}
}