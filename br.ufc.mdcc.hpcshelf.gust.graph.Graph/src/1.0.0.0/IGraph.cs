using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using System;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.Graph
{
	public interface IGraph<CTN, V, E> : BaseIGraph<CTN, V, E>
		where CTN:IDataContainer<V, E>
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
	public interface IGraphDelegate<V, E, RV, RE> 
		where V:IDVertex
		where E:IDEdge<V> 
		where RE: IDEdgeInstance<V, RV>{
		IEnumerator<RE> edgeSet ();
		ICollection<RE> edgesOf (RV vertex);
		IEnumerator<RE> iteratorEdgesOf (RV vertex);
		ICollection<RV> vertexSet ();
		void addIncomingEdge (RE e);
		void addOutgoingEdge (RE e);
		void removeIncomingEdge (RE e);
		void removeOutgoingEdge (RE e);
		ICollection<T> incoming<T> (RV vertex);
		ICollection<T> outgoing<T> (RV vertex);
		void noSafeAdd (RE e);
		void noSafeAdd (RV source, RV target);
		void noSafeAdd (RV source, RV target, float weight);
		bool addVertex (RV vertex);
		bool containsEdge(RE e);
		bool containsVertex (RV v);
		bool removeVertex (RV v);
		int countE();
		int countV();
		int degreeOf(RV v);
	}
}