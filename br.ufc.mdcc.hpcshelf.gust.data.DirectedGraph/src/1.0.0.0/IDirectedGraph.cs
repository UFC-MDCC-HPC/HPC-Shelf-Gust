using System;
using System.Collections.Generic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.data.Graph;

namespace br.ufc.mdcc.hpcshelf.gust.data.DirectedGraph
{
	public interface IDirectedGraph<V,E> : BaseIDirectedGraph<V,E>, IGraph<V,E>
	{
		int inDegreeOf(V vertex);

		HashSet<E> incomingEdgesOf(V vertex);

		int outDegreeOf(V vertex);

		HashSet<E> outgoingEdgesOf(V vertex);
	}
}