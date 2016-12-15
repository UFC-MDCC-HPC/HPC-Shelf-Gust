using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using System;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph
{
	public interface IDirectedGraph<CTN, V, E> : BaseIDirectedGraph<CTN, V, E>, IGraph<CTN, V, E>
		where CTN:IDataContainer<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
		IInstanceControlDirected<V, E, TV, TE> newInstanceControlT<TV, TE> (TE e, int size)  where TE: IEdgeInstance<V, TV>;
		IInstanceControlDirected<V, E, int, IEdgeInstance<V, int>> newInstanceControl (int size);

		object InstanceControlT { get; }
	}

	public interface IInstanceControlDirected<V, E, TV, TE>: IInstanceControl<V, E, TV, TE> where V:IVertex where E:IEdge<V> where TE: IEdgeInstance<V, TV> {
		int inDegreeOf(TV vertex);
		ICollection<TE> incomingEdgesOf(TV vertex);
		int outDegreeOf(TV vertex);
		ICollection<TE> outgoingEdgesOf(TV vertex);
	}
}