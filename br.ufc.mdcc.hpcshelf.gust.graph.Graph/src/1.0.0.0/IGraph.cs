using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.ContainerData;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;

namespace br.ufc.mdcc.hpcshelf.gust.graph.Graph
{
	public interface IGraph<CTN, V, E> : BaseIGraph<CTN, V, E>
		where CTN:IContainerData<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
	}
}