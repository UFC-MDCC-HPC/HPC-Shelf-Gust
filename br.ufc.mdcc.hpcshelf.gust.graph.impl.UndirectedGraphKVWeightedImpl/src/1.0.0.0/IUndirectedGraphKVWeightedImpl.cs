using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeWeighted;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.UndirectedGraphKVWeightedImpl
{
	public class IUndirectedGraphKVWeightedImpl : BaseIUndirectedGraphKVWeightedImpl, IUndirectedGraph<CTN, V, E>
where CTN:IDataContainerKV<V, E>
where V:IDVertexBasic
where E:IDEdgeWeighted<V>
	{
		public override void main()
		{
		}
	}
}
