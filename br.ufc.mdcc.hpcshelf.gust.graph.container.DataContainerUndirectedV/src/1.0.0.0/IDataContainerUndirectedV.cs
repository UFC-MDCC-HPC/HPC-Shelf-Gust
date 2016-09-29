using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirected;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirectedV
{
	public interface IDataContainerUndirectedV<V, E> : BaseIDataContainerUndirectedV<V, E>, IDataContainerUndirected<V, E>
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}