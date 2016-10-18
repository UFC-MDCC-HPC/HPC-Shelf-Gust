/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.app.TesterContext
{
	public interface BaseITesterContext<G, CTN, V, E> : IComputationKind 
		where G:IGraph<CTN, V, E>
		where CTN:IDataContainer<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
		E EdgeFactory {get;}
		V Vertex {get;}
	}
}