/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;

namespace br.ufc.mdcc.behavior.test.GraphContext
{
	public interface BaseIGraphContext<G, CTN, V, E> : IComputationKind 
		where G:IGraph<CTN, V, E>
		where CTN:IDataContainer<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
		G Graph {get;}
	}
}