using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.example.tc.TerminateFunctionTC;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.example.tc.DataTriangle;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;

namespace br.ufc.mdcc.hpcshelf.gust.example.tc.TerminateFunctionTCImpl
{
	public class ITerminateFunctionTCImpl : BaseITerminateFunctionTCImpl, ITerminateFunctionTC
	{
		private IPortTypeIterator iterate_pairs = null;
		public IPortTypeIterator Iterate_pairs { set {this.iterate_pairs = value; } }

		private int iterate_num = 0;

		public override void main() 
		{
			IIteratorInstance<IKVPair<IVertex, IDataTriangle>> output_pairs = (IIteratorInstance<IKVPair<IVertex, IDataTriangle>>)Output_pairs.Instance;
			IIteratorInstance<IKVPair<IVertex, IDataTriangle>> input_pairs = (IIteratorInstance<IKVPair<IVertex, IDataTriangle>>)Input_pairs.Instance;

			object pair;

			// For 3 iterations: startup_push, gust0, gust1
			while ((iterate_num++) < 2) {
				while (iterate_pairs.fetch_next(out pair))
					input_pairs.put(pair);
				input_pairs.finish ();
			}
			input_pairs.finish ();

			// Finally, all pairs are sent to the output.
			while (iterate_pairs.fetch_next(out pair))
				output_pairs.put(pair);

			output_pairs.finish ();
		}
	}
}
