/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV
{
	public interface BaseIDataContainerV<V, E> : BaseIDataContainer<V, E>, IDataStructureKind 
		where V:IVertex
		where E:IEdge<V>
	{
	}
}