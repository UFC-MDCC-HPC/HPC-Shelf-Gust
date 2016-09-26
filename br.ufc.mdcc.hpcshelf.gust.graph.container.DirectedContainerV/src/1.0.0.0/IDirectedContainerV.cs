using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DirectedContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DirectedContainerV
{
	public interface IDirectedContainerV<V, E> : BaseIDirectedContainerV<V, E>, IDirectedContainer<V, E>
		where V:IVertex
		where E:IDEdge<V>
	{
	}
}