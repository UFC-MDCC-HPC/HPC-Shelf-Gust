/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.container.ContainerData;

namespace br.ufc.mdcc.hpcshelf.gust.graph.Graph
{
	public interface BaseIGraph<CTN, V, E> : IComputationKind 
		where CTN:IContainerData<V, E>
		where V:IDVertex
		where E:IDEdge<V>
	{
		E DEdgeFactory {get;}
		V DVertex {get;}
	}
}