/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.example.tc.DataTriangle;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.example.tc.TerminateFunctionTC;

namespace br.ufc.mdcc.hpcshelf.gust.example.tc.TerminateFunctionTCImpl 
{
	public abstract class BaseITerminateFunctionTCImpl: Computation, BaseITerminateFunctionTC
	{
		private IIterator<IKVPair<IVertex, IDataTriangle>> input_pairs = null;

		public IIterator<IKVPair<IVertex, IDataTriangle>> Input_pairs
		{
			get
			{
				if (this.input_pairs == null)
					this.input_pairs = (IIterator<IKVPair<IVertex, IDataTriangle>>) Services.getPort("input_pairs");
				return this.input_pairs;
			}
		}
		private IIterator<IKVPair<IVertex, IDataTriangle>> output_pairs = null;

		public IIterator<IKVPair<IVertex, IDataTriangle>> Output_pairs
		{
			get
			{
				if (this.output_pairs == null)
					this.output_pairs = (IIterator<IKVPair<IVertex, IDataTriangle>>) Services.getPort("output_pairs");
				return this.output_pairs;
			}
		}
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
	}
}