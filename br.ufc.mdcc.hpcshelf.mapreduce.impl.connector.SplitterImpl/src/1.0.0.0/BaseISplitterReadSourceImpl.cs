/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.connector.Splitter;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.mapreduce.binding.environment.EnvironmentBindingReadData;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSourceInterface;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.connector.SplitterImpl 
{
	//public abstract class BaseISplitterReadSourceImpl<M2,BF,IKey,IValue,GIF>: Synchronizer, BaseISplitterReadSource<M2,BF,IKey,IValue,GIF>
	public abstract class BaseISplitterReadSourceImpl<M2,GIF>: Synchronizer, BaseISplitterReadSource<M2,GIF>
		where M2:IMaintainer
		//where BF:IPartitionFunction<GIF>
		//where IKey:IData
		//where IValue:IData
		where GIF:IInputFormat
	{
		static protected int FACET_REDUCE = 0;
		static protected int FACET_MAP = 1;
		static protected int FACET_SOURCE = 2;
		static protected int FACET_SINK = 3;

		private IPartitionFunction<GIF> bin_function_gif = default(IPartitionFunction<GIF>);
		protected IPartitionFunction<GIF> Bin_function_gif
		{
			get
			{
				if (this.bin_function_gif == null)
					this.bin_function_gif = (IPartitionFunction<GIF>) Services.getPort("bin_function_gif");
				return this.bin_function_gif;
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

//		private IKey input_key = default(IKey);
//		protected IKey Input_key
//		{
//			get
//			{
//				if (this.input_key == null)
//					this.input_key = (IKey) Services.getPort("input_key");
//				return this.input_key;
//			}
//		}
//
//		private IInteger output_key = null;
//		protected IInteger Output_key
//		{
//			get
//			{
//				if (this.output_key == null)
//					this.output_key = (IInteger) Services.getPort("output_key");
//				return this.output_key;
//			}
//		}

//		private IKVPair<IInteger, GIF> input_format = null;
//		protected IKVPair<IInteger, GIF> Input_format
//		{
//			get
//			{
//				if (this.input_format == null)
//					this.input_format = (IKVPair<IInteger, GIF>) Services.getPort("input_format");
//				return this.input_format;
//			}
//		}
//		private IPartitionFunction<GIF> bin_function_gif = null;//default(BF);
//		protected IPartitionFunction<GIF> Bin_function_gif
//		{
//			get
//			{
//				if (this.bin_function_gif == null)
//					this.bin_function_gif = (IPartitionFunction<GIF>) Services.getPort("bin_function_gif");
//				return this.bin_function_gif;
//			}
//		}

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