using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using System;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph
{
	public interface IUndirectedGraph<CTN, V, E> : BaseIUndirectedGraph<CTN, V, E>, IGraph<CTN, V, E>
		where CTN:IDataContainer<V, E>
		where V:IVertex
		where E:IEdge<V>
	{
		IUndirectedGraphInstance<V, E, TV, TE> newInstanceT<TV, TE> (TE e, int size)  where TE: IEdgeInstance<V, TV>;
		IUndirectedGraphInstance<V, E, int, IEdgeInstance<V, int>> newInstance (int size);

		object GraphInstanceT { get; }
	}

	public interface IUndirectedGraphInstance<V, E, TV, TE>: IGraphInstance<V, E, TV, TE> where V:IVertex where E:IEdge<V> where TE: IEdgeInstance<V, TV> {

	}
}