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
using br.ufc.mdcc.hpcshelf.gust.connector.Step;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.impl.connector.StepImpl 
{
	public abstract class BaseIStepGustyFeederImpl<M0,TKey, TValue,GIF>: Synchronizer, BaseIStepGustyFeeder<M0,TKey, TValue,GIF>
		where M0:IMaintainer
		where TKey:IData
		where TValue:IData	
		where GIF:IInputFormat
	{
		static protected int FACET_REDUCE = 0;
		static protected int FACET_MAP = 1;


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

		private IServerBase<IPortTypeIterator> feed_pairs = null;
		protected IServerBase<IPortTypeIterator> Feed_pairs
		{
			get
			{
				if (this.feed_pairs == null)
					this.feed_pairs = (IServerBase<IPortTypeIterator>) Services.getPort("feed_pairs");
				return this.feed_pairs;
			}
		}

		private IServerBase<IPortTypeIterator> feed_graph = null;
		protected IServerBase<IPortTypeIterator> Feed_graph
		{
			get
			{
				if (this.feed_graph == null)
					this.feed_graph = (IServerBase<IPortTypeIterator>) Services.getPort("feed_graph");
				return this.feed_graph;
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

		private IIterator<IKVPair<TKey,IIterator<TValue>>> output = null;
		protected IIterator<IKVPair<TKey,IIterator<TValue>>> Output {
			get {
				if (this.output == null)
					this.output = (IIterator<IKVPair<TKey,IIterator<TValue>>>)Services.getPort("output");
				return this.output;
			}
		}

		private IIterator<TValue> value_factory = null;
		protected IIterator<TValue> Value_factory {
			get {
				if (this.value_factory == null)
					this.value_factory = (IIterator<TValue>)Services.getPort("value_factory");
				return this.value_factory;
			}
		}

		private IIterator<GIF> value_factory_gif = null;
		protected IIterator<GIF> Value_factory_gif {
			get {
				if (this.value_factory_gif == null)
					this.value_factory_gif = (IIterator<GIF>)Services.getPort("value_factory_gif");
				return this.value_factory_gif;
			}
		}

		private IIterator<IKVPair<IInteger,IIterator<GIF>>> output_gifs = null;
		protected IIterator<IKVPair<IInteger,IIterator<GIF>>> Output_gifs {
			get {
				if (this.output_gifs == null)
					this.output_gifs = (IIterator<IKVPair<IInteger,IIterator<GIF>>>)Services.getPort("output_gifs");
				return this.output_gifs;
			}
		}
	}
}