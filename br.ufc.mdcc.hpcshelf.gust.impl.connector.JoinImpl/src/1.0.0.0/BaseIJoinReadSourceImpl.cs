/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.connector.Join;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.gust.binding.environment.EnvironmentBindingReadData;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSourceInterface;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl 
{
	public abstract class BaseIJoinReadSourceImpl<M2,IKey, IValue, BF>: Synchronizer, BaseIJoinReadSource<M2,IKey, IValue, BF>
		where M2:IMaintainer
		where IKey:IData
		where IValue:IData
		where BF:IPartitionFunction<IKey>
	{
		static protected int FACET_REDUCE = 0;
		static protected int FACET_MAP = 1;
		static protected int FACET_SOURCE = 2;
		static protected int FACET_SINK = 3;

		private BF bin_function = default(BF);

		protected BF Bin_function
		{
			get
			{
				if (this.bin_function == null)
					this.bin_function = (BF) Services.getPort("bin_function");
				return this.bin_function;
			}
		}
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
		private IReadData<IPortTypeDataSourceInterface> source = null;

		public IReadData<IPortTypeDataSourceInterface> Source
		{
			get
			{
				if (this.source == null)
					this.source = (IReadData<IPortTypeDataSourceInterface>) Services.getPort("source");
				return this.source;
			}
		}
		

		private ITaskPort<ITaskPortTypeAdvance> task_binding_split_first = null;
		public ITaskPort<ITaskPortTypeAdvance> Task_binding_split_first
		{
			get
			{
				if (this.task_binding_split_first == null)
					this.task_binding_split_first = (ITaskPort<ITaskPortTypeAdvance>) Services.getPort("task_binding_split_first");
				return this.task_binding_split_first;
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

		private IKey input_key = default(IKey);
		protected IKey Input_key
		{
			get
			{
				if (this.input_key == null)
					this.input_key = (IKey) Services.getPort("input_key");
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

		private IInputFormat read_format = null;
		protected IInputFormat Read_format
		{
			get
			{
				if (this.read_format == null)
					this.read_format = (IInputFormat) Services.getPort("read_format");
				return this.read_format;
			}
		}

	}
}