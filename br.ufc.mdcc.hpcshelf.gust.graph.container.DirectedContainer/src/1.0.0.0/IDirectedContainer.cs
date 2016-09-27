using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.ContainerData;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DirectedContainer
{
	public interface IDirectedContainer<V, E> : BaseIDirectedContainer<V, E>, IContainerData<V, E>
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}