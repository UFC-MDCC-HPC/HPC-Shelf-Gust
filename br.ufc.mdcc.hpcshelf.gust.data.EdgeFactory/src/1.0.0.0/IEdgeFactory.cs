using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.data.Edge;

namespace br.ufc.mdcc.hpcshelf.gust.data.EdgeFactory
{
	public interface IEdgeFactory<V, E> : BaseIEdgeFactory<V, E>
		where V:IData
		where E:IEdge
	{
      E createEdge(V source, V target);
	}
}