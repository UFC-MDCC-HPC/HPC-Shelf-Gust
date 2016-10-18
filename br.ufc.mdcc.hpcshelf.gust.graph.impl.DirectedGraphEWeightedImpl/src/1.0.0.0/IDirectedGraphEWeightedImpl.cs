using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DirectedGraphEWeightedImpl
{
	public class IDirectedGraphEWeightedImpl : BaseIDirectedGraphEWeightedImpl, IDirectedGraph<CTN, V, E>
where CTN:IDataContainerE<V, E>
where V:IVertexBasic
where E:IEdgeWeighted<V>
	{
		public override void main()
		{
		}
	}
}
