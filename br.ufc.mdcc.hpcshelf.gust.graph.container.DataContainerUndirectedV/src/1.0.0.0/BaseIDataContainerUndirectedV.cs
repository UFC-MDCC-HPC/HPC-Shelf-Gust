/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirected;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirectedV
{
	public interface BaseIDataContainerUndirectedV<V, E> : BaseIDataContainerUndirected<V, E>, IDataStructureKind 
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}