/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.UndirectedContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.UndirectedContainerE
{
	public interface BaseIUndirectedContainerE<V, E> : BaseIUndirectedContainer<V, E>, IDataStructureKind 
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}