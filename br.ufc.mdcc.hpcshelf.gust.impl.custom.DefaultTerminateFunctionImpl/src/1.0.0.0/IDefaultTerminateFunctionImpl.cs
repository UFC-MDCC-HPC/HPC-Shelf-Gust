using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;

namespace br.ufc.mdcc.hpcshelf.gust.impl.custom.DefaultTerminateFunctionImpl
{
	public class IDefaultTerminateFunctionImpl<IKey,IValue,OKey,OValue> : BaseIDefaultTerminateFunctionImpl<IKey,IValue,OKey,OValue>, ITerminateFunction<IKey, IValue, OKey, OValue>
where IKey:IData
where IValue:IData
where OKey:IData
where OValue:IData
	{
		private IPortTypeIterator iterate_pairs = null;
		public IPortTypeIterator Iterate_pairs { set {this.iterate_pairs = value; } }

		public override void main()
		{
			Console.WriteLine (this.Rank + ": TERMINATE FUNCTION 1");

			IIteratorInstance<IKVPair<OKey, OValue>> output_pairs = (IIteratorInstance<IKVPair<OKey, OValue>>)Output_pairs.Instance;
			IIteratorInstance<IKVPair<IKey, IValue>> input_pairs = (IIteratorInstance<IKVPair<IKey, IValue>>)Input_pairs.Instance;

			Console.WriteLine (this.Rank + ": TERMINATE FUNCTION 2");

			input_pairs.finish ();

			// All pairs are sent to the output, i.e. the algorithm is not iterative.
			object pair;
			while (iterate_pairs.fetch_next(out pair))
				output_pairs.put(pair);

			output_pairs.finish ();

			Console.WriteLine (this.Rank + ": TERMINATE FUNCTION 3");
		}
	}
}
