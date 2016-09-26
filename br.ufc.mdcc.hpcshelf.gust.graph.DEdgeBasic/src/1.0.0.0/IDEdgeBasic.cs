using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DEdgeBasic {
	public interface IDEdgeBasic<V> : BaseIDEdgeBasic<V>, IDEdge<V> where V:IVertex {
		IDEdgeBasicInstance<V> newInstance (int source, int target);
		IDEdgeBasicInstance<V> EInstance { get; }
	}
	public interface IDEdgeBasicInstance<V> : IDEdgeInstance<V>, ICloneable where V: IVertex{
	}
}