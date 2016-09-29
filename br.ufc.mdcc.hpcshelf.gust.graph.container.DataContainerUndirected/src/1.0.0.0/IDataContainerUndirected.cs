using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirected
{
	public interface IDataContainerUndirected<V, E> : BaseIDataContainerUndirected<V, E>, IDataContainer<V, E>
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}