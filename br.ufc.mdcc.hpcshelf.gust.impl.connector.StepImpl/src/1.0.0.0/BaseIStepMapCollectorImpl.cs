/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.gust.connector.Step;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.impl.connector.StepImpl 
{
	public abstract class BaseIStepMapCollectorImpl<M0,TKey,TValue,PF>: Synchronizer, BaseIStepMapCollector<M0,TKey,TValue,PF>
		where M0:IMaintainer
		where PF:IPartitionFunction<TKey>
		where TKey:IData
		where TValue:IData
	{
		static protected int FACET_REDUCE = 0;
		static protected int FACET_MAP = 1;

		private IClientBase<IPortTypeIterator> collect_pairs = null;

		protected IClientBase<IPortTypeIterator> Collect_pairs
		{
			get
			{
				if (this.collect_pairs == null)
					this.collect_pairs = (IClientBase<IPortTypeIterator>) Services.getPort("collect_pairs");
				return this.collect_pairs;
			}
		}

		private ITaskPort<ITaskPortTypeAdvance> task_binding_shuffle = null;

		public ITaskPort<ITaskPortTypeAdvance> Task_binding_shuffle
		{
			get
			{
				if (this.task_binding_shuffle == null)
					this.task_binding_shuffle = (ITaskPort<ITaskPortTypeAdvance>) Services.getPort("task_binding_shuffle");
				return this.task_binding_shuffle;
			}
		}

		private TKey input_key = default(TKey);
		protected TKey Input_key
		{
			get
			{
				if (this.input_key == null)
					this.input_key = (TKey) Services.getPort("input_key");
				return this.input_key;
			}
		}

		private IInteger output_key = null;
		protected IInteger Output_key
		{
			get
			{
				if (this.output_key == null)
					this.output_key = (IInteger) Services.getPort("output_key");
				return this.output_key;
			}
		}

		private IChannel step_channel = null;

		public IChannel Step_channel
		{
			get
			{
				if (this.step_channel == null)
					this.step_channel = (IChannel) Services.getPort("step_channel");
				return this.step_channel;
			}
		}
		private PF partition_function = default(PF);

		protected PF Partition_function
		{
			get
			{
				if (this.partition_function == null)
					this.partition_function = (PF) Services.getPort("partition_function");
				return this.partition_function;
			}
		}
	}
}