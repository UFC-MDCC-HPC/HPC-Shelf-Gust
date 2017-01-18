/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.mapreduce.connector.Shuffler;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.connector.ShufflerImpl 
{
	public abstract class BaseIShufflerMapCollectorImpl<M0,TKey,TValue,PF,GIF>: Synchronizer, BaseIShufflerMapCollector<M0,TKey,TValue,PF,GIF>
		where M0:IMaintainer
		where PF:IPartitionFunction<TKey>
		where TKey:IData
		where TValue:IData
		where GIF:IInputFormat
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

		private IClientBase<IPortTypeIterator> collect_graph = null;
		protected IClientBase<IPortTypeIterator> Collect_graph
		{
			get
			{
				if (this.collect_graph == null)
					this.collect_graph = (IClientBase<IPortTypeIterator>) Services.getPort("collect_graph");
				return this.collect_graph;
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

		private IChannel shuffler_channel = null;

		public IChannel Shuffler_channel
		{
			get
			{
				if (this.shuffler_channel == null)
					this.shuffler_channel = (IChannel) Services.getPort("shuffler_channel");
				return this.shuffler_channel;
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
/////////////
		private IPartitionFunction<GIF> partition_function_gif = null;//default(BF);
		protected IPartitionFunction<GIF> Partition_function_gif
		{
			get
			{
				if (this.partition_function_gif == null)
					this.partition_function_gif = (IPartitionFunction<GIF>) Services.getPort("partition_function_gif");
				return this.partition_function_gif;
			}
		}

		private GIF input_key_gif = default(GIF);
		protected GIF Input_key_gif
		{
			get
			{
				if (this.input_key_gif == null)
					this.input_key_gif = (GIF) Services.getPort("input_key_gif");
				return this.input_key_gif;
			}
		}

		private IInteger output_key_gif = null;
		protected IInteger Output_key_gif
		{
			get
			{
				if (this.output_key_gif == null)
					this.output_key_gif = (IInteger) Services.getPort("output_key_gif");
				return this.output_key_gif;
			}
		}
	}
}