using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.ContainerData
{
	public interface IContainerData<V, E> : BaseIContainerData<V, E>, IData
		where V:IVertex
		where E:IDEdge<V>
	{
	}
}