/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpcshelf.mapreduce.connector.Splitter;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.connector.SplitterImpl 
{
	public abstract class BaseISplitterMapFeederImpl<M1,IKey,IValue,GIF>: Synchronizer, BaseISplitterMapFeeder<M1,IKey,IValue,GIF>
		where M1:IMaintainer
		where IKey:IData
		where IValue:IData
		where GIF:IInputFormat
	{
		static protected int FACET_REDUCE = 0;
		static protected int FACET_MAP = 1;
		static protected int FACET_SOURCE = 2;
		static protected int FACET_SINK = 3;


		private IServerBase<IPortTypeIterator> feed_pairs = null;

		public IServerBase<IPortTypeIterator> Feed_pairs
		{
			get
			{
				if (this.feed_pairs == null)
					this.feed_pairs = (IServerBase<IPortTypeIterator>) Services.getPort("feed_pairs");
				return this.feed_pairs;
			}
		}
		private IServerBase<IPortTypeIterator> feed_graph = null;

		public IServerBase<IPortTypeIterator> Feed_graph
		{
			get
			{
				if (this.feed_graph == null)
					this.feed_graph = (IServerBase<IPortTypeIterator>) Services.getPort("feed_graph");
				return this.feed_graph;
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

		private IIterator<IKVPair<IKey,IValue>> output = null;
		protected IIterator<IKVPair<IKey,IValue>> Output {
			get {
				if (this.output == null)
					this.output = (IIterator<IKVPair<IKey,IValue>>)Services.getPort("output");
				return this.output;
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
		
		private IIterator<IKVPair<IInteger, GIF>> output_gif = null;
		protected IIterator<IKVPair<IInteger, GIF>> Output_gif
		{
			get
			{
				if (this.output_gif == null)
					this.output_gif = (IIterator<IKVPair<IInteger, GIF>>) Services.getPort("output_gif");
				return this.output_gif;
			}
		}
	}
}