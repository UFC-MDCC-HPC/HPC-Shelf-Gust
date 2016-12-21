/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.hpcshelf.gust.computation.Gusty;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.impl.computation.GustyImpl 
{
	public abstract class BaseIGustyImpl<M,TKey, TValue, OKey, OValue, RF>: Computation, BaseIGusty<M,TKey, TValue, OKey, OValue, RF>
		where M:IMaintainer
		where RF:IGustyFunction<TKey, TValue, OKey, OValue>
		where OKey:IData
		where OValue:IData
		where TKey:IData
		where TValue:IData
	{
		private IKVPair<OKey, OValue> output_value = null;
		protected IKVPair<OKey, OValue> Output_value
		{
			get
			{
				if (this.output_value == null)
					this.output_value = (IKVPair<OKey, OValue>) Services.getPort("output_value");
				return this.output_value;
			}
		}

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
		private RF gusty_function = default(RF);

		protected RF Gusty_function
		{
			get
			{
				if (this.gusty_function == null)
					this.gusty_function = (RF) Services.getPort("gusty_function");
				return this.gusty_function;
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
		private ITaskPort<ITaskPortTypeAdvance> task_gusty = null;

		public ITaskPort<ITaskPortTypeAdvance> Task_gusty
		{
			get
			{
				if (this.task_gusty == null)
					this.task_gusty = (ITaskPort<ITaskPortTypeAdvance>) Services.getPort("task_gusty");
				return this.task_gusty;
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

	}
}