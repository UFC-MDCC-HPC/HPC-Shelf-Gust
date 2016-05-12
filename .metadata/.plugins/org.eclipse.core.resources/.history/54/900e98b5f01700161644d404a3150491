using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.data.Edge;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.data.Graph
{
	public interface IGraph<V, E> : BaseIGraph<V, E>, IData
		where V:IData
		where E:IEdge
	{
		HashSet<E> getAllEdges(V sourceVertex, V targetVertex);
		E getEdge(V sourceVertex, V targetVertex);
		EdgeFactory<V, E> getEdgeFactory();
		E addEdge(V sourceVertex, V targetVertex);
		bool addEdge(V sourceVertex, V targetVertex, E e);
		bool addVertex(V v);
		bool containsEdge(V sourceVertex, V targetVertex);
		bool containsEdge(E e);
		bool containsVertex(V v);
		HashSet<E> edgeSet();
		HashSet<E> edgesOf(V vertex);
		bool removeAllEdges(ICollection<E> edges);
		HashSet<E> removeAllEdges(V sourceVertex, V targetVertex);
		bool removeAllVertices(ICollection<V> vertices);
		E removeEdge(V sourceVertex, V targetVertex);
		bool removeEdge(E e);
		bool removeVertex(V v);
		HashSet<V> vertexSet();
		V getEdgeSource(E e);
		V getEdgeTarget(E e);
		double getEdgeWeight(E e);
	}
}