/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerDirected;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph
{
	public interface BaseIDirectedGraph<CTN, V, E> : BaseIGraph<CTN, V, E>, IComputationKind 
		where CTN:IDataContainerDirected<V, E>
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}