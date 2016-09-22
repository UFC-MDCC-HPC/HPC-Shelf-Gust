using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;

namespace br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic
{
	public interface IEdgeBasic<V> : BaseIEdgeBasic<V>, IEdge<V>
		where V:IVertex
	{
	}
}