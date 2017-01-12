/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.MapFunction;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.custom.MapFunctionForward 
{
	public abstract class BaseIMapFunctionForward<IKey, IValue, TKey, TValue>: Computation, BaseIMapFunction<IKey, IValue, TKey, TValue>
		where IKey:IData
		where IValue:IData
		where TKey:IData
		where TValue:IData
	{
		private IKey input_key = default(IKey);

		public IKey Input_key
		{
			get
			{
				if (this.input_key == null)
					this.input_key = (IKey) Services.getPort("input_key");
				return this.input_key;
			}
		}
		private IValue input_value = default(IValue);

		public IValue Input_value
		{
			get
			{
				if (this.input_value == null)
					this.input_value = (IValue) Services.getPort("input_value");
				return this.input_value;
			}
		}
		private IIterator<IKVPair<TKey, TValue>> output_data = null;

		public IIterator<IKVPair<TKey, TValue>> Output_data
		{
			get
			{
				if (this.output_data == null)
					this.output_data = (IIterator<IKVPair<TKey, TValue>>) Services.getPort("output_data");
				return this.output_data;
			}
		}
	}
}