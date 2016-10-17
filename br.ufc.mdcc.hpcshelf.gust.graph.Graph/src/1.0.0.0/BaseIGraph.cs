/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.Graph
{
	public interface BaseIGraph<CTN, V, E> : IComputationKind 
		where CTN:IDataContainer<V, E>
		where V:IDVertex
		where E:IDEdge<V>
	{
		E EdgeFactory {get;}
		V Vertex {get;}
		CTN DataContainer {get;}
	}
}