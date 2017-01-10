/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.ReduceFunction;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpcshelf.mapreduce.computation.Reducer;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.computation.ReducerImpl 
{
	public abstract class BaseIReducerImpl<M, RF, GIF, TKey, TValue, OKey, OValue, G>: Computation, BaseIReducer<M, RF, GIF, TKey, TValue, OKey, OValue, G>
		where M:IMaintainer
		where RF:IReduceFunction<GIF, TKey, TValue, OKey, OValue, G>
		where GIF:IInputFormat
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where G:IData
	{
//		private IKVPair<OKey, OValue> output_value = null;
//		protected IKVPair<OKey, OValue> Output_value
//		{
//			get
//			{
//				if (this.output_value == null)
//					this.output_value = (IKVPair<OKey, OValue>) Services.getPort("output_value");
//				return this.output_value;
//			}
//		}

		private OValue continuation_value = default(OValue);
		protected OValue Continue_value
		{
			get
			{
				if (this.continuation_value == null)
					this.continuation_value = (OValue) Services.getPort("continuation_value");
				return this.continuation_value;
			}
		}

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
		private RF reduce_function = default(RF);

		protected RF Reduce_function
		{
			get
			{
				if (this.reduce_function == null)
					this.reduce_function = (RF) Services.getPort("reduce_function");
				return this.reduce_function;
			}
		}

		private IKVPair<TKey, IIterator<TValue>> input_values = null;

		protected IKVPair<TKey, IIterator<TValue>> Input_values
		{
			get
			{
				if (this.input_values == null)
					this.input_values = (IKVPair<TKey, IIterator<TValue>>) Services.getPort("input_values");
				return this.input_values;
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
		private ITaskPort<ITaskPortTypeAdvance> task_reduce = null;

		public ITaskPort<ITaskPortTypeAdvance> Task_reduce
		{
			get
			{
				if (this.task_reduce == null)
					this.task_reduce = (ITaskPort<ITaskPortTypeAdvance>) Services.getPort("task_reduce");
				return this.task_reduce;
			}
		}

		private IIterator<IKVPair<OKey,OValue>> output = null;
		protected IIterator<IKVPair<OKey,OValue>> Output {
			get {
				if (this.output == null)
					this.output = (IIterator<IKVPair<OKey,OValue>>)Services.getPort("output");
				return this.output;
			}
		}
		
		private IIterator<IKVPair<IInteger, GIF>> graph_format = null;
		public IIterator<IKVPair<IInteger, GIF>> Graph_format
		{
			get
			{
				if (this.graph_format == null)
					this.graph_format = (IIterator<IKVPair<IInteger, GIF>>) Services.getPort("graph_format");
				return this.graph_format;
			}
		}
	}
}