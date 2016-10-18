/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph
{
	public interface BaseIUndirectedGraph<CTN, V, E> : BaseIGraph<CTN, V, E>, IComputationKind 
		where CTN:IDataContainer<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
	}
}