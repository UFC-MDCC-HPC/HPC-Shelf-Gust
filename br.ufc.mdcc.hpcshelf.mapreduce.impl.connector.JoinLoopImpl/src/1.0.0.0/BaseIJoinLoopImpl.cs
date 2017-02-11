/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.TerminateFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpcshelf.mapreduce.connector.JoinLoop;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.connector.JoinLoopImpl 
{
	public abstract class BaseIJoinLoopImpl<M1, TF, IKey, IValue, OKey, OValue, BF, GIF>: Synchronizer, BaseIJoinLoop<M1, TF, IKey, IValue, OKey, OValue, BF, GIF>
		where M1:IMaintainer
		where TF:ITerminateFunction<IKey, IValue, OKey, OValue>
		where IKey:IData
		where IValue:IData
		where OKey:IData
		where OValue:IData
		where BF:IPartitionFunction<IKey>
		where GIF:IInputFormat
	{
//		static protected int FACET_REDUCE = 0; //static protected int FACET_MAP = 1;
//		static protected int FACET_SOURCE = 2;
//		static protected int FACET_SINK = 3;
//
//		private TF terminate_function = default(TF);
//		protected TF Terminate_function
//		{
//			get
//			{
//				if (this.terminate_function == null)
//					this.terminate_function = (TF) Services.getPort("terminate_function");
//				return this.terminate_function;
//			}
//		}
//
//		private IIterator<IKVPair<IKey, IIterator<IValue>>> output = null;
//		protected IIterator<IKVPair<IKey, IIterator<IValue>>> Output
//		{
//			get
//			{
//				if (this.output == null)
//					this.output = (IIterator<IKVPair<IKey, IIterator<IValue>>>) Services.getPort("output");
//				return this.output;
//			}
//		}
//
//		private IInteger output_key_iterate = null;
//		protected IInteger Output_key_iterate
//		{
//			get
//			{
//				if (this.output_key_iterate == null)
//					this.output_key_iterate = (IInteger) Services.getPort("output_key_iterate");
//				return this.output_key_iterate;
//			}
//		}
//
//		private IInteger output_key_iterate_gif = null;
//		public IInteger Output_key_iterate_gif
//		{
//			get
//			{
//				if (this.output_key_iterate_gif == null)
//					this.output_key_iterate_gif = (IInteger) Services.getPort("output_key_iterate_gif");
//				return this.output_key_iterate_gif;
//			}
//		}
//
//		private ITaskPort<ITaskPortTypeData> task_binding_data = null;
//		public ITaskPort<ITaskPortTypeData> Task_binding_data
//		{
//			get
//			{
//				if (this.task_binding_data == null)
//					this.task_binding_data = (ITaskPort<ITaskPortTypeData>) Services.getPort("task_binding_data");
//				return this.task_binding_data;
//			}
//		}
//
//		private IIterator<GIF> value_factory_gif = null;
//		protected IIterator<GIF> Value_factory_gif
//		{
//			get
//			{
//				if (this.value_factory_gif == null)
//					this.value_factory_gif = (IIterator<GIF>) Services.getPort("value_factory_gif");
//				return this.value_factory_gif;
//			}
//		}
//
//		private IKey input_key_iterate = default(IKey);
//		protected IKey Input_key_iterate
//		{
//			get
//			{
//				if (this.input_key_iterate == null)
//					this.input_key_iterate = (IKey) Services.getPort("input_key_iterate");
//				return this.input_key_iterate;
//			}
//		}
//
//		private IIterator<IKVPair<IInteger, IIterator<GIF>>> output_gifs = null;
//		protected IIterator<IKVPair<IInteger, IIterator<GIF>>> Output_gifs
//		{
//			get
//			{
//				if (this.output_gifs == null)
//					this.output_gifs = (IIterator<IKVPair<IInteger, IIterator<GIF>>>) Services.getPort("output_gifs");
//				return this.output_gifs;
//			}
//		}
//
//		private GIF input_key_iterate_gif = default(GIF);
//		public GIF Input_key_iterate_gif
//		{
//			get
//			{
//				if (this.input_key_iterate_gif == null)
//					this.input_key_iterate_gif = (GIF) Services.getPort("input_key_iterate_gif");
//				return this.input_key_iterate_gif;
//			}
//		}
//
//		private ITaskPort<ITaskPortTypeAdvance> task_binding_split_first = null;
//		public ITaskPort<ITaskPortTypeAdvance> Task_binding_split_first
//		{
//			get
//			{
//				if (this.task_binding_split_first == null)
//					this.task_binding_split_first = (ITaskPort<ITaskPortTypeAdvance>) Services.getPort("task_binding_split_first");
//				return this.task_binding_split_first;
//			}
//		}
//
//		private ITaskPort<ITaskPortTypeAdvance> task_binding_split_next = null;
//		public ITaskPort<ITaskPortTypeAdvance> Task_binding_split_next
//		{
//			get
//			{
//				if (this.task_binding_split_next == null)
//					this.task_binding_split_next = (ITaskPort<ITaskPortTypeAdvance>) Services.getPort("task_binding_split_next");
//				return this.task_binding_split_next;
//			}
//		}
//
//		private BF bin_function_iterate = default(BF);
//		protected BF Bin_function_iterate
//		{
//			get
//			{
//				if (this.bin_function_iterate == null)
//					this.bin_function_iterate = (BF) Services.getPort("bin_function_iterate");
//				return this.bin_function_iterate;
//			}
//		}
//
//		private IPartitionFunction<GIF> bin_function_iterate_gif = null;
//		protected IPartitionFunction<GIF> Bin_function_iterate_gif
//		{
//			get
//			{
//				if (this.bin_function_iterate_gif == null)
//					this.bin_function_iterate_gif = (IPartitionFunction<GIF>) Services.getPort("bin_function_iterate_gif");
//				return this.bin_function_iterate_gif;
//			}
//		}
//
//		private IServerBase<IPortTypeIterator> feed_pairs = null;
//		public IServerBase<IPortTypeIterator> Feed_pairs
//		{
//			get
//			{
//				if (this.feed_pairs == null)
//					this.feed_pairs = (IServerBase<IPortTypeIterator>) Services.getPort("feed_pairs");
//				return this.feed_pairs;
//			}
//		}
//
//		private IClientBase<IPortTypeIterator> collect_pairs = null;
//		public IClientBase<IPortTypeIterator> Collect_pairs
//		{
//			get
//			{
//				if (this.collect_pairs == null)
//					this.collect_pairs = (IClientBase<IPortTypeIterator>) Services.getPort("collect_pairs");
//				return this.collect_pairs;
//			}
//		}
//
//		private IServerBase<IPortTypeIterator> feed_graph = null;
//		public IServerBase<IPortTypeIterator> Feed_graph
//		{
//			get
//			{
//				if (this.feed_graph == null)
//					this.feed_graph = (IServerBase<IPortTypeIterator>) Services.getPort("feed_graph");
//				return this.feed_graph;
//			}
//		}
//
//		private IClientBase<IPortTypeIterator> collect_graph = null;
//		public IClientBase<IPortTypeIterator> Collect_graph
//		{
//			get
//			{
//				if (this.collect_graph == null)
//					this.collect_graph = (IClientBase<IPortTypeIterator>) Services.getPort("collect_graph");
//				return this.collect_graph;
//			}
//		}
//
//		private IChannel split_channel = null;
//		protected IChannel Split_channel
//		{
//			get
//			{
//				if (this.split_channel == null)
//					this.split_channel = (IChannel) Services.getPort("split_channel");
//				return this.split_channel;
//			}
//		}
//
//		private IIterator<IValue> value_factory = null;
//		protected IIterator<IValue> Value_factory
//		{
//			get
//			{
//				if (this.value_factory == null)
//					this.value_factory = (IIterator<IValue>) Services.getPort("value_factory");
//				return this.value_factory;
//			}
//		}
	}
}