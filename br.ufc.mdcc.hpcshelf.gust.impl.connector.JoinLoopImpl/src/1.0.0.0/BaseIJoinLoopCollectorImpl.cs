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
using br.ufc.mdcc.hpcshelf.gust.connector.JoinLoop;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinLoopImpl 
{
	public abstract class BaseIJoinLoopCollectorImpl<M0,TF,IKey,IValue,OKey,OValue,BF,GIF>: Synchronizer, BaseIJoinLoopCollector<M0,TF,IKey,IValue,OKey,OValue,BF,GIF>
		where M0:IMaintainer
		where TF:ITerminateFunction<IKey,IValue,OKey,OValue>
		where IKey:IData
		where IValue:IData
		where OKey:IData
		where OValue:IData
		where BF:IPartitionFunction<IKey>
		where GIF:IInputFormat
	{
		static protected int FACET_REDUCE = 0;
		//static protected int FACET_MAP = 1;
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

		private IClientBase<IPortTypeIterator> collect_graph = null;

		public IClientBase<IPortTypeIterator> Collect_graph
		{
			get
			{
				if (this.collect_graph == null)
					this.collect_graph = (IClientBase<IPortTypeIterator>) Services.getPort("collect_graph");
				return this.collect_graph;
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
//		private GIF graph_input_format = default(GIF);
//		protected GIF Graph_input_format
//		{
//			get
//			{
//				if (this.graph_input_format == null)
//					this.graph_input_format = (GIF) Services.getPort("graph_input_format");
//				return this.graph_input_format;
//			}
//		}

//		private IIterator<IKVPair<IInteger, GIF>> output_gif = null;
//		protected IIterator<IKVPair<IInteger, GIF>> Output_gif
//		{
//			get
//			{
//				if (this.output_gif == null)
//					this.output_gif = (IIterator<IKVPair<IInteger, GIF>>) Services.getPort("output_gif");
//				return this.output_gif;
//			}
//		}
		private IPartitionFunction<GIF> bin_function_iterate_gif = null;//default(BF);
		protected IPartitionFunction<GIF> Bin_function_iterate_gif
		{
			get
			{
				if (this.bin_function_iterate_gif == null)
					this.bin_function_iterate_gif = (IPartitionFunction<GIF>) Services.getPort("bin_function_iterate_gif");
				return this.bin_function_iterate_gif;
			}
		}

		private GIF input_key_iterate_gif = default(GIF);
		protected GIF Input_key_iterate_gif
		{
			get
			{
				if (this.input_key_iterate_gif == null)
					this.input_key_iterate_gif = (GIF) Services.getPort("input_key_iterate_gif");
				return this.input_key_iterate_gif;
			}
		}

		private IInteger output_key_iterate_gif = null;
		protected IInteger Output_key_iterate_gif
		{
			get
			{
				if (this.output_key_iterate_gif == null)
					this.output_key_iterate_gif = (IInteger) Services.getPort("output_key_iterate_gif");
				return this.output_key_iterate_gif;
			}
		}
	}
}