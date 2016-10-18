using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.UndirectedGraphEImpl
{
	public class IUndirectedGraphEImpl : BaseIUndirectedGraphEImpl, IUndirectedGraph<CTN, V, E>
where CTN:IDataContainerE<V, E>
where V:IVertexBasic
where E:IEdgeBasic<V>
	{
		public override void main()
		{
		}
	}
}
