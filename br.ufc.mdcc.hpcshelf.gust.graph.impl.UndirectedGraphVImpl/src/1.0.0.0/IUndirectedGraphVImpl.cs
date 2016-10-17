using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.UndirectedGraphVImpl
{
	public class IUndirectedGraphVImpl : BaseIUndirectedGraphVImpl, IUndirectedGraph<CTN, V, E>
where CTN:IDataContainerV<V, E>
where V:IDVertexBasic
where E:IDEdgeBasic<V>
	{
		public override void main()
		{
		}
	}
}
