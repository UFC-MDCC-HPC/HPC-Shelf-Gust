using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using System;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.Graph
{
	public interface IGraph<CTN, V, E> : BaseIGraph<CTN, V, E>
		where CTN:IDataContainer<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
		//IInstanceControl<V, E, int, IEdgeInstance<V, int>> InstanceControl { get; }
	}
	public interface IInstanceControl<V, E, TV, TE>: ICommon<V, E, TV, TE> where V:IVertex where E:IEdge<V> where TE: IEdgeInstance<V, TV> {
		ICollection<TE> getAllEdges(TV sourceVertex, TV targetVertex);
		TE getEdge(TV sourceVertex, TV targetVertex);
		TE addEdge(TV sourceVertex, TV targetVertex);
		bool addEdge(TE e);
		bool containsEdge(TV sourceVertex, TV targetVertex);
		ICollection<TV> neighborsOf (TV vertex);
		IEnumerator<TV> iteratorNeighborsOf (TV vertex);
		bool removeAllEdges(ICollection<TE> edges);
		ICollection<TE> removeAllEdges(TV sourceVertex, TV targetVertex);
		bool removeAllVertices(ICollection<TV> vertices);
		TE removeEdge(TV sourceVertex, TV targetVertex);
		bool removeEdge(TE e);
		TV getEdgeSource(TE e);
		TV getEdgeTarget(TE e);
		float getEdgeWeight (TE e);
		void setAllEdgeWeight (TE e, float weight);
		void setAllEdgeWeight (TV sourceVertex, TV targetVertex, float weight);
		float getEdgeWeight (TV sourceVertex, TV targetVertex);
	}
	public interface IGraphHelper<V, E, TV, TE>: ICommon<V, E, TV, TE> where V:IVertex	where E:IEdge<V> where TE: IEdgeInstance<V, TV>{
		void addIncomingEdge (TE e);
		void addOutgoingEdge (TE e);
		void removeIncomingEdge (TE e);
		void removeOutgoingEdge (TE e);
		ICollection<T> incoming<T> (TV vertex);
		ICollection<T> outgoing<T> (TV vertex);
	}
	public interface ICommon<V, E, TV, TE> 
		where V:IVertex
		where E:IEdge<V> 
		where TE: IEdgeInstance<V, TV> {
		IEnumerator<TE> edgeSet();
		ICollection<TE> edgesOf (TV vertex);
		IEnumerator<TE> iteratorEdgesOf (TV vertex);
		ICollection<TV> vertexSet ();
		void noSafeAdd (TE e);
		void noSafeAdd (TV source, TV target);
		void noSafeAdd (TV source, TV target, float weight);
		bool addVertex (TV vertex);
		bool containsEdge(TE e);
		bool containsVertex (TV v);
		bool removeVertex (TV v);
		int countE();
		int countV();
		int degreeOf(TV v);
		IDataContainerInstance<V, E> DataContainer { get; set; }
	}
}