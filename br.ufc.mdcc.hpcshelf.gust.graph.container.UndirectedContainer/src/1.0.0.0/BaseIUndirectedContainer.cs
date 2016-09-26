/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.ContainerData;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.UndirectedContainer
{
	public interface BaseIUndirectedContainer<V, E> : BaseIContainerData<V, E>, IDataStructureKind 
		where V:IVertex
		where E:IDEdge<V>
	{
	}
}