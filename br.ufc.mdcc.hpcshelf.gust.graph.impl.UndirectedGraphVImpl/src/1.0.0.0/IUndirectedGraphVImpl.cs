using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.UndirectedGraphVImpl
{
	public class IUndirectedGraphVImpl : BaseIUndirectedGraphVImpl, IUndirectedGraph<CTN, V, E>
where CTN:IDataContainerV<V, E>
where V:IVertexBasic
where E:IEdgeBasic<V>
	{
		public override void main()
		{
		}
	}
}
