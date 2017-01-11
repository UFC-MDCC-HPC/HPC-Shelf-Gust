/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.graph.OutputFormat;

namespace br.ufc.mdcc.hpcshelf.gust.graph.OutputFormatDefaultImpl 
{
	public abstract class BaseIOutputFormatDefaultImpl<OKey, OValue>: DataStructure, BaseIOutputFormat<OKey, OValue>
		where OKey:IData
		where OValue:IData
	{
		private IIterator<IKVPair<OKey, OValue>> output_pairs_iterator = null;

		protected IIterator<IKVPair<OKey, OValue>> Output_pairs_iterator
		{
			get
			{
				if (this.output_pairs_iterator == null)
					this.output_pairs_iterator = (IIterator<IKVPair<OKey, OValue>>) Services.getPort("output_pairs_iterator");
				return this.output_pairs_iterator;
			}
		}
	}
}