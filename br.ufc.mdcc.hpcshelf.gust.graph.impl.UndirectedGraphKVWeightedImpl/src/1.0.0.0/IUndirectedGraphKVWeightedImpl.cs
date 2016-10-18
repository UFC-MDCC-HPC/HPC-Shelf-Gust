using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.UndirectedGraphKVWeightedImpl
{
	public class IUndirectedGraphKVWeightedImpl : BaseIUndirectedGraphKVWeightedImpl, IUndirectedGraph<CTN, V, E>
where CTN:IDataContainerKV<V, E>
where V:IVertexBasic
where E:IEdgeWeighted<V>
	{
		public override void main()
		{
		}
	}
}
