/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction;
//using br.ufc.mdcc.hpcshelf.gust.binding.environment.BindingDirectMxNIterator;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance;
//using br.ufc.mdcc.hpcshelf.platform.Platform;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.computation.Gusty;

namespace br.ufc.mdcc.hpcshelf.gust.impl.computation.GustyImpl 
{
	public abstract class BaseIGustyImpl<M, GF, TKey, TValue, OKey, OValue, PType, G>: Computation, BaseIGusty<M, GF, TKey, TValue, OKey, OValue, PType, G>
		where M:IMaintainer
		where GF:IGustyFunction<TKey, TValue, OKey, OValue, PType, G>
		where TKey:IData
		where TValue:IData
		where OKey:IData
		where OValue:IData
		where PType:IData
		where G:IData
	{
		private IIterator<IKVPair<OKey, OValue>> output = null;

		public IIterator<IKVPair<OKey, OValue>> Output
		{
			get
			{
				if (this.output == null)
					this.output = (IIterator<IKVPair<OKey, OValue>>) Services.getPort("output");
				return this.output;
			}
		}
		private IKVPair<IInteger, PType> partition_graph = null;

		public IKVPair<IInteger, PType> Partition_graph
		{
			get
			{
				if (this.partition_graph == null)
					this.partition_graph = (IKVPair<IInteger, PType>) Services.getPort("partition_graph");
				return this.partition_graph;
			}
		}
		private G graph = default(G);

		public G Graph
		{
			get
			{
				if (this.graph == null)
					this.graph = (G) Services.getPort("graph");
				return this.graph;
			}
		}
		private GF gusty_function = default(GF);

		protected GF Gusty_function
		{
			get
			{
				if (this.gusty_function == null)
					this.gusty_function = (GF) Services.getPort("gusty_function");
				return this.gusty_function;
			}
		}
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
		private OValue continuation_value = default(OValue);

		public OValue Continue_value
		{
			get
			{
				if (this.continuation_value == null)
					this.continuation_value = (OValue) Services.getPort("continuation_value");
				return this.continuation_value;
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
//		private IProcessingNode<M> platform_gusty = null;

//		public IProcessingNode<M> Platform_gusty
//		{
//			get
//			{
//				if (this.platform_gusty == null)
//					this.platform_gusty = (IProcessingNode<M>) Services.getPort("platform_gusty");
//				return this.platform_gusty;
//			}
//		}
		private IKVPair<TKey, IIterator<TValue>> input_values = null;

		public IKVPair<TKey, IIterator<TValue>> Input_values
		{
			get
			{
				if (this.input_values == null)
					this.input_values = (IKVPair<TKey, IIterator<TValue>>) Services.getPort("input_values");
				return this.input_values;
			}
		}
	}
}