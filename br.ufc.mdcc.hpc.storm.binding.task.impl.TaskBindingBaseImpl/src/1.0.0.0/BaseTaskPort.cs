/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskPortType;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;

namespace br.ufc.mdcc.hpc.storm.binding.task.impl.TaskBindingBaseImpl 
{
	public abstract class BaseTaskPort<T>: Synchronizer, BaseITaskPort<T>
		where T:ITaskPortType
	{
		private T task_port_type = default(T);

		protected T Task_port_type
		{
			get
			{
				if (this.task_port_type == null)
					this.task_port_type = (T) Services.getPort("task_port_type");
				return this.task_port_type;
			}
		}

		private IChannel channel = null;

		protected IChannel Channel
		{
			get
			{
				if (this.channel == null)
					this.channel = (IChannel) Services.getPort("channel");
				return this.channel;
			}
		}
	}
}