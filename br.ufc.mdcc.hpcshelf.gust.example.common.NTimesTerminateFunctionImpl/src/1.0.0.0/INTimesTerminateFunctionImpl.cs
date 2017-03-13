using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.example.common.NTimesTerminateFunction;

namespace br.ufc.mdcc.hpcshelf.gust.example.common.NTimesTerminateFunctionImpl
{
	public class INTimesTerminateFunctionImpl<IKey, IValue, OKey, OValue> : BaseINTimesTerminateFunctionImpl<IKey, IValue, OKey, OValue>, INTimesTerminateFunction<IKey, IValue, OKey, OValue>
where IKey:IData
where IValue:IData
where OKey:IData
where OValue:IData
	{
		private IPortTypeIterator iterate_pairs = null;
		public IPortTypeIterator Iterate_pairs { set {this.iterate_pairs = value; } }

		private int iterate_num = 0;

		public override void main()
		{
			IIteratorInstance<IKVPair<OKey, OValue>> output_pairs = (IIteratorInstance<IKVPair<OKey, OValue>>)Output_pairs.Instance;
			IIteratorInstance<IKVPair<IKey, IValue>> input_pairs = (IIteratorInstance<IKVPair<IKey, IValue>>)Input_pairs.Instance;

			object pair;

			while (iterate_pairs.has_next()) {
				while (iterate_pairs.fetch_next(out pair))
					input_pairs.put(pair);
				input_pairs.finish ();
			}
			iterate_pairs.fetch_next (out pair);
			input_pairs.finish ();

			while (iterate_pairs.fetch_next(out pair))
				output_pairs.put(pair);

			output_pairs.finish ();
		}
	}
}
