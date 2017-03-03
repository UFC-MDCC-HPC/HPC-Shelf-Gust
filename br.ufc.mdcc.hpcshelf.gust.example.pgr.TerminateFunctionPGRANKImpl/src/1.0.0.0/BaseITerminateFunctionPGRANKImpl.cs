/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.example.pgr.DataPGRANK;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.example.pgr.TerminateFunctionPGRANK;

namespace br.ufc.mdcc.hpcshelf.gust.example.pgr.TerminateFunctionPGRANKImpl 
{
	public abstract class BaseITerminateFunctionPGRANKImpl: Computation, BaseITerminateFunctionPGRANK
	{
		private IIterator<IKVPair<IInteger, IDataPGRANK>> input_pairs = null;

		public IIterator<IKVPair<IInteger, IDataPGRANK>> Input_pairs
		{
			get
			{
				if (this.input_pairs == null)
					this.input_pairs = (IIterator<IKVPair<IInteger, IDataPGRANK>>) Services.getPort("input_pairs");
				return this.input_pairs;
			}
		}
		private IIterator<IKVPair<IInteger, IDataPGRANK>> output_pairs = null;

		public IIterator<IKVPair<IInteger, IDataPGRANK>> Output_pairs
		{
			get
			{
				if (this.output_pairs == null)
					this.output_pairs = (IIterator<IKVPair<IInteger, IDataPGRANK>>) Services.getPort("output_pairs");
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