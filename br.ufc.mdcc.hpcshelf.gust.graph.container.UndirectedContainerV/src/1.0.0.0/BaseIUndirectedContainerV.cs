/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.UndirectedContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.UndirectedContainerV
{
	public interface BaseIUndirectedContainerV<V, E> : BaseIUndirectedContainer<V, E>, IDataStructureKind 
		where V:IVertex
		where E:IDEdge<V>
	{
	}
}