/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeJoin;

namespace br.ufc.mdcc.hpcshelf.gust.binding.task.TaskBindingJoin
{
	public interface BaseITaskBindingJoin : ISynchronizerKind 
	{
		ITaskPort<ITaskPortTypeJoin> Task_port {get;}
	}
}