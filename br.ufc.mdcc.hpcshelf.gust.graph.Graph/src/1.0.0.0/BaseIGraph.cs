/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.container.ContainerData;

namespace br.ufc.mdcc.hpcshelf.gust.graph.Graph
{
	public interface BaseIGraph<CTN, V, E> : IComputationKind 
		where CTN:IContainerData<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
		E EdgeFactory {get;}
		V Vertex {get;}
	}
}