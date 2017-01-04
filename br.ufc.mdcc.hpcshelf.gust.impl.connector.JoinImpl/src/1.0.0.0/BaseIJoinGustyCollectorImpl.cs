/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpcshelf.gust.connector.Join;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl 
{
	public abstract class BaseIJoinGustyCollectorImpl<M0,IKey,IValue,OKey,OValue,BF,TF>: Synchronizer, BaseIJoinGustyCollector<M0,IKey,IValue,OKey, OValue, BF,TF>
		where M0:IMaintainer
		where IKey:IData
		where IValue:IData
		where OKey:IData
		where OValue:IData
		where BF:IPartitionFunction<IKey>
		where TF:ITerminateFunction<IKey,IValue,OKey,OValue>
	{
		static protected int FACET_REDUCE = 0;
		static protected int FACET_MAP = 1;
		static protected int FACET_SOURCE = 2;
		static protected int FACET_SINK = 3;

		private ITaskPort<ITaskPortTypeAdvance> task_port_split_next = null;


		private ITaskPort<ITaskPortTypeAdvance> task_binding_split_next = null;
		public ITaskPort<ITaskPortTypeAdvance> Task_binding_split_next
		{
			get
			{
				if (this.task_binding_split_next == null)
					this.task_binding_split_next = (ITaskPort<ITaskPortTypeAdvance>) Services.getPort("task_binding_split_next");
				return this.task_binding_split_next;
			}
		}
			
		private IClientBase<IPortTypeIterator> collect_pairs = null;

		public IClientBase<IPortTypeIterator> Collect_pairs
		{
			get
			{
				if (this.collect_pairs == null)
					this.collect_pairs = (IClientBase<IPortTypeIterator>) Services.getPort("collect_pairs");
				return this.collect_pairs;
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

		private BF bin_function_iterate = default(BF);
		protected BF Bin_function_iterate
		{
			get
			{
				if (this.bin_function_iterate == null)
					this.bin_function_iterate = (BF) Services.getPort("bin_function_iterate");
				return this.bin_function_iterate;
			}
		}

		private OKey input_key = default(OKey);
		protected OKey Input_key
		{
			get
			{
				if (this.input_key == null)
					this.input_key = (OKey) Services.getPort("input_key");
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

		private OKey input_key_iterate = default(OKey);
		protected OKey Input_key_iterate
		{
			get
			{
				if (this.input_key_iterate == null)
					this.input_key_iterate = (OKey) Services.getPort("input_key_iterate");
				return this.input_key_iterate;
			}
		}

		private IInteger output_key_iterate = null;
		protected IInteger Output_key_iterate
		{
			get
			{
				if (this.output_key_iterate == null)
					this.output_key_iterate = (IInteger) Services.getPort("output_key_iterate");
				return this.output_key_iterate;
			}
		}

		private TF terminate_function = default(TF);
		protected TF Terminate_function
		{
			get
			{
				if (this.terminate_function == null)
					this.terminate_function = (TF) Services.getPort("terminate_function");
				return this.terminate_function;
			}
		}
		private IIterator<IKVPair<OKey, OValue>> output_pairs = null;

		protected IIterator<IKVPair<OKey, OValue>> Output_pairs
		{
			get
			{
				if (this.output_pairs == null)
					this.output_pairs = (IIterator<IKVPair<OKey, OValue>>) Services.getPort("output_pairs");
				return this.output_pairs;
			}
		}


		private IIterator<IKVPair<IKey, IValue>> input_pairs = null;

		protected IIterator<IKVPair<IKey, IValue>> Input_pairs
		{
			get
			{
				if (this.input_pairs == null)
					this.input_pairs = (IIterator<IKVPair<IKey, IValue>>) Services.getPort("input_pairs");
				return this.input_pairs;
			}
		}

		private IInputFormat collect_format = null;
		protected IInputFormat Collect_format
		{
			get
			{
				if (this.collect_format == null)
					this.collect_format = (IInputFormat) Services.getPort("collect_format");
				return this.collect_format;
			}
		}
	}
}