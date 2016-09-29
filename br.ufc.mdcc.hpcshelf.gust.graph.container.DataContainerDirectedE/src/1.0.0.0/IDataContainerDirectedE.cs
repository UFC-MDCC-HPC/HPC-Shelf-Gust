using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerDirected;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerDirectedE
{
	public interface IDataContainerDirectedE<V, E> : BaseIDataContainerDirectedE<V, E>, IDataContainerDirected<V, E>
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}