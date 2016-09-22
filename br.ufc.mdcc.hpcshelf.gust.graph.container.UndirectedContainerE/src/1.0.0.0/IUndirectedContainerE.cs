using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.UndirectedContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.UndirectedContainerE
{
	public interface IUndirectedContainerE<V, E> : BaseIUndirectedContainerE<V, E>, IUndirectedContainer<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
	}
}