using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DirectedContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DirectedContainerE
{
	public interface IDirectedContainerE<V, E> : BaseIDirectedContainerE<V, E>, IDirectedContainer<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
	}
}