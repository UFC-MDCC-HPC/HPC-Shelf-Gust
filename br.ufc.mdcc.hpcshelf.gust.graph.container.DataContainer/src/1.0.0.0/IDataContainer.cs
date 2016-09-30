using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.common.Data;
using System.Collections.Generic;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer {
	public interface IDataContainer<V, E> : BaseIDataContainer<V, E>, IData where V:IDVertex where E:IDEdge<V> {
		
	}

	public interface IDataContainerInstance<V, E> : IDataInstance, ICloneable where V:IDVertex where E:IDEdge<V> {
		IDVertexInstance Vertex { get; set; }
		IDEdgeInstance<V> EdgeFactory { set; get; }
	}

	public interface IContainer<RV, RE> where RE: IRootDEdge<RV> {
		IEnumerator<RE> edgeSet ();
		ICollection<RE> edgesOf (RV vertex);
		IEnumerator<RE> iteratorEdgesOf (RV vertex);
		ICollection<RV> vertexSet ();
		void addIncomingEdge (RE e);
		void addOutgoingEdge (RE e);
		void removeIncomingEdge (RE e);
		void removeOutgoingEdge (RE e);
		void noSafeAdd (RE e);
		void noSafeAdd (RV source, RV target);
		void noSafeAdd (RV source, RV target, float weight);
		ICollection<T> incoming<T> (RV vertex);
		ICollection<T> outgoing<T> (RV vertex);
		bool addVertex (RV vertex);
		bool containsEdge (RE e);
		bool containsVertex (RV v);
		bool removeVertex (RV v);
		int countE ();
		int countV ();
		int degreeOf (RV v);

		RE edgeFactory { get; }
		bool allowingMultipleEdges { get; }
		bool allowingLoops { get; }
	}
	public struct EdgeContainer<T> {
		public ICollection<T> incoming;
		public ICollection<T> outgoing;
	}
}