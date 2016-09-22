using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;

namespace br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted
{
	public interface IEdgeWeighted<V> : BaseIEdgeWeighted<V>, IEdge<V>
		where V:IVertex
	{
	}
}