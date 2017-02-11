/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSinkInterface;
using br.ufc.mdcc.hpcshelf.mapreduce.binding.environment.EnvironmentBindingWriteData;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeData;

namespace br.ufc.mdcc.hpcshelf.mapreduce.connector.JoinLoop
{
	public interface BaseIJoinLoopWriteSink<M3,OKey,OValue> : ISynchronizerKind 
		where M3:IMaintainer
		where OKey:IData
		where OValue:IData
	{
		IWriteData<IPortTypeDataSinkInterface> Sink { get; }
		ITaskPort<ITaskPortTypeData> Task_binding_data { get; }
	}
}