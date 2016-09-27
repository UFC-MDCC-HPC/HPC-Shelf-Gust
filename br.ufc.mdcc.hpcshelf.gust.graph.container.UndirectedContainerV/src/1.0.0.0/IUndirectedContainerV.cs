using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.UndirectedContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.UndirectedContainerV
{
	public interface IUndirectedContainerV<V, E> : BaseIUndirectedContainerV<V, E>, IUndirectedContainer<V, E>
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}