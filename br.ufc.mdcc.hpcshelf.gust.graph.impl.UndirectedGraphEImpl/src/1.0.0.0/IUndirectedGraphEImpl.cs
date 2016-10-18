using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.UndirectedGraphEImpl
{
	public class IUndirectedGraphEImpl : BaseIUndirectedGraphEImpl, IUndirectedGraph<CTN, V, E>
where CTN:IDataContainerE<V, E>
where V:IDVertexBasic
where E:IDEdgeBasic<V>
	{
		public override void main()
		{
		}
	}
}