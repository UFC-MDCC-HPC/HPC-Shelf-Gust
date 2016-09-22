using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.ContainerData;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.UndirectedContainer
{
	public interface IUndirectedContainer<V, E> : BaseIUndirectedContainer<V, E>, IContainerData<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
	}
}