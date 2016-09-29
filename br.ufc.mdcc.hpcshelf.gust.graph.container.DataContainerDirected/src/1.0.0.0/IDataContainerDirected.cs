using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerDirected
{
	public interface IDataContainerDirected<V, E> : BaseIDataContainerDirected<V, E>, IDataContainer<V, E>
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}