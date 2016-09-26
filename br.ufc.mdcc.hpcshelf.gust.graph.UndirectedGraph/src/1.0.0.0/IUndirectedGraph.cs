using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.UndirectedContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph
{
	public interface IUndirectedGraph<CTN, V, E> : BaseIUndirectedGraph<CTN, V, E>, IGraph<CTN, V, E>
		where CTN:IUndirectedContainer<V, E>
		where V:IVertex
		where E:IDEdge<V>
	{
	}
}