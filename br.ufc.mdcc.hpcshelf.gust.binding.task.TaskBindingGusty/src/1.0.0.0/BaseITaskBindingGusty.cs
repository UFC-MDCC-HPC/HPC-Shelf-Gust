/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeGusty;

namespace br.ufc.mdcc.hpcshelf.gust.binding.task.TaskBindingGusty
{
	public interface BaseITaskBindingGusty : ISynchronizerKind 
	{
		ITaskPort<ITaskPortTypeGusty> Task_port {get;}
	}
}