using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer
{
	public interface IDataContainer<V, E> : BaseIDataContainer<V, E>, IData
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}