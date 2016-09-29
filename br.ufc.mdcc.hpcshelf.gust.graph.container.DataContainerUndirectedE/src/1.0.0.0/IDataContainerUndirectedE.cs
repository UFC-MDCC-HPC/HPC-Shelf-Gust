using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirected;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirectedE
{
	public interface IDataContainerUndirectedE<V, E> : BaseIDataContainerUndirectedE<V, E>, IDataContainerUndirected<V, E>
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}