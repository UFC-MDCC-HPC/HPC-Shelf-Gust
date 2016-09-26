/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.ContainerData;

namespace br.ufc.mdcc.hpcshelf.gust.graph.app.TesterContext
{
	public interface BaseITesterContext<G, CTN, V, E> : IComputationKind 
		where G:IGraph<CTN, V, E>
		where CTN:IContainerData<V, E>
		where V:IVertex
		where E:IDEdge<V>
	{
		E DEdgeFactory {get;}
		V Vertex {get;}
	}
}