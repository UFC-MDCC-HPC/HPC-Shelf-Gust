/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeDataColector;

namespace br.ufc.mdcc.hpcshelf.gust.binding.task.TaskBindingDataColector
{
	public interface BaseITaskBindingDataColector : ISynchronizerKind 
	{
		ITaskPort<ITaskPortTypeDataColector> Task_port {get;}
	}
}