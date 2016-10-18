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
	public interface IGraphHelper<V, E, TV, TE> 
		where V:IDVertex
		where E:IDEdge<V> 
		where TE: IDEdgeInstance<V, TV>{
		IEnumerator<TE> edgeSet ();
		ICollection<TE> edgesOf (TV vertex);
		IEnumerator<TE> iteratorEdgesOf (TV vertex);
		ICollection<TV> vertexSet ();
		void addIncomingEdge (TE e);
		void addOutgoingEdge (TE e);
		void removeIncomingEdge (TE e);
		void removeOutgoingEdge (TE e);
		ICollection<T> incoming<T> (TV vertex);
		ICollection<T> outgoing<T> (TV vertex);
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
	}
}