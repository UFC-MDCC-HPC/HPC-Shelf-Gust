/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.connector.JoinLoop;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.gust.binding.environment.EnvironmentBindingWriteData;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSinkInterface;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinLoopImpl 
{
	public abstract class BaseIJoinLoopWriteSinkImpl<M3,OKey,OValue>: Synchronizer, BaseIJoinLoopWriteSink<M3,OKey,OValue>
		where M3:IMaintainer
		where OKey:IData
		where OValue:IData
	{
		static protected int FACET_REDUCE = 0;
		//static protected int FACET_MAP = 1;
		static protected int FACET_SOURCE = 2;
		static protected int FACET_SINK = 3;

		private IChannel split_channel = null;

		protected IChannel Split_channel
		{
			get
			{
				if (this.split_channel == null)
					this.split_channel = (IChannel) Services.getPort("split_channel");
				return this.split_channel;
			}
		}
		private IWriteData<IPortTypeDataSinkInterface> sink = null;

		public IWriteData<IPortTypeDataSinkInterface> Sink
		{
			get
			{
				if (this.sink == null)
					this.sink = (IWriteData<IPortTypeDataSinkInterface>) Services.getPort("sink");
				return this.sink;
			}
		}

	
		private ITaskPort<ITaskPortTypeData> task_binding_data = null;
		public ITaskPort<ITaskPortTypeData> Task_binding_data
		{
			get
			{
				if (this.task_binding_data == null)
					this.task_binding_data = (ITaskPort<ITaskPortTypeData>) Services.getPort("task_binding_data");
				return this.task_binding_data;
			}
		}


	}
}