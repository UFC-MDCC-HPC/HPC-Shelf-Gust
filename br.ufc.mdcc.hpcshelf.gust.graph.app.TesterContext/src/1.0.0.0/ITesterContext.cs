using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.ContainerData;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;

namespace br.ufc.mdcc.hpcshelf.gust.graph.app.TesterContext
{
	public interface ITesterContext<G, CTN, V, E> : BaseITesterContext<G, CTN, V, E>
		where G:IGraph<CTN, V, E>
		where CTN:IContainerData<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
	}
}