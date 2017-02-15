using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.example.sssp.DataSSSP;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.example.sssp.TerminateFunctionSSSP;

namespace br.ufc.mdcc.hpcshelf.gust.example.sssp.TerminateFunctionSSSPImpl
{
	public class ITerminateFunctionSSSPImpl : BaseITerminateFunctionSSSPImpl, ITerminateFunctionSSSP
	{
		private IPortTypeIterator iterate_pairs = null;
		public IPortTypeIterator Iterate_pairs { set {this.iterate_pairs = value; } }

		private int iterate_num = 0;

		public override void main() 
		{
			IIteratorInstance<IKVPair<IInteger, IDataSSSP>> output_pairs = (IIteratorInstance<IKVPair<IInteger, IDataSSSP>>)Output_pairs.Instance;
			IIteratorInstance<IKVPair<IInteger, IDataSSSP>> input_pairs = (IIteratorInstance<IKVPair<IInteger, IDataSSSP>>)Input_pairs.Instance;

			object pair;

			while (iterate_pairs.has_next()) {//Espera por um finish() dado no reduce_function, o que ocorre quando todo IDataSSSPInstancez.Activated=false
				while (iterate_pairs.fetch_next(out pair))
					input_pairs.put(pair);
				input_pairs.finish ();
			}
			iterate_pairs.fetch_next (out pair);//Faz o fetch_next na finalizacao
			input_pairs.finish ();

			while (iterate_pairs.fetch_next(out pair)) // Espera pelos dados definitivos de saida
				output_pairs.put(pair);

			output_pairs.finish ();
		}
	}
}
