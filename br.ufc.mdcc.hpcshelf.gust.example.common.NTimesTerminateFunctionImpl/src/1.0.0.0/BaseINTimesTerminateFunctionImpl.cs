/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.example.common.NTimesTerminateFunction;

namespace br.ufc.mdcc.hpcshelf.gust.example.common.NTimesTerminateFunctionImpl
{
	public abstract class BaseINTimesTerminateFunctionImpl<IKey,IValue,OKey,OValue>: Computation, BaseINTimesTerminateFunction<IKey, IValue, OKey, OValue>
		where IKey:IData
		where IValue:IData
		where OKey:IData
		where OValue:IData
	{
		private IIterator<IKVPair<OKey, OValue>> output_pairs = null;

		public IIterator<IKVPair<OKey, OValue>> Output_pairs
		{
			get
			{
				if (this.output_pairs == null)
					this.output_pairs = (IIterator<IKVPair<OKey, OValue>>) Services.getPort("output_pairs");
				return this.output_pairs;
			}
		}
		private IIterator<IKVPair<IKey, IValue>> input_pairs = null;

		public IIterator<IKVPair<IKey, IValue>> Input_pairs
		{
			get
			{
				if (this.input_pairs == null)
					this.input_pairs = (IIterator<IKVPair<IKey, IValue>>) Services.getPort("input_pairs");
				return this.input_pairs;
			}
		}
		/**
		private IPortTypeIterator iterate_pairs = null;
		protected IPortTypeIterator Iterate_pairs
		{
			get
			{
				if (this.iterate_pairs == null)
					this.iterate_pairs = (IPortTypeIterator) Services.getPort("iterate_pairs");
				return this.iterate_pairs;
			}
		}
		*/
	}
}