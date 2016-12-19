using System;
using System.Threading;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.example.cw.Tallier;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.String;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.Integer;

using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic;

namespace br.ufc.mdcc.hpcshelf.gust.example.cw.impl.TallierImpl
{
	public class ITallierImpl : BaseITallierImpl, ITallier
	{
		public ITallierImpl() { } 

		public override void after_initialize() 
		{
			IIntegerInstance continuation_value_instance = (IIntegerInstance)Continuation_value.Instance;
			continuation_value_instance.Value = 0;
		}

		public override void main() 
		{ 
			IKVPairInstance<IString,IIterator<IInteger>> input_values_instance = (IKVPairInstance<IString,IIterator<IInteger>>)Input_values.Instance;
			IIteratorInstance<IInteger> counts_iterator = (IIteratorInstance<IInteger>)input_values_instance.Value;

			IIntegerInstance continuation_value_instance = (IIntegerInstance)Continuation_value.Instance;
			int acc = continuation_value_instance.Value;

			int total_count = acc;
			object integer_object;
			while (counts_iterator.fetch_next (out integer_object))
				total_count += ((IIntegerInstance)integer_object).Value;

			//IKVPairInstance<IString,IInteger> output_value_instance = (IKVPairInstance<IString,IInteger>)Output_value.Instance;
			IKVPairInstance<IString,IInteger> item = (IKVPairInstance<IString,IInteger>) Output.createItem();
			IIteratorInstance<IKVPair<IString, IInteger>> output_value_instance = (IIteratorInstance<IKVPair<IString, IInteger>>) Output.Instance;

			((IStringInstance)item.Key).Value = ((IStringInstance)input_values_instance.Key).Value;
			((IIntegerInstance)item.Value).Value = total_count;

			output_value_instance.put(item);

		}
		public void Compute(){ main (); }
		public void Optimize(){}
		public void InputGraph(){}
	}
}
