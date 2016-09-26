using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DirectedContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph
{
	public interface IDirectedGraph<CTN, V, E> : BaseIDirectedGraph<CTN, V, E>, IGraph<CTN, V, E>
		where CTN:IDirectedContainer<V, E>
		where V:IVertex
		where E:IDEdge<V>
	{
	}
}