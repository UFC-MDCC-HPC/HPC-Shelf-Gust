using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.data.Edge;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.data;
using br.ufc.mdcc.hpcshelf.gust.data.EdgeFactory;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.data.Graph
{
	public interface IGraph<V, E> : BaseIGraph<V, E>, IData
		where V:IData
		where E:IEdge
	{
		HashSet<E> getAllEdges(V sourceVertex, V targetVertex);
		E getEdge(V sourceVertex, V targetVertex);
		IEdgeFactory<V, E> getEdgeFactory();
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
		//extends for Gust
		HashSet<object> getBuffer(V v);
		bool isActive(V v);
		void setActive(V v);
		void setInactive(V v);
	}
}