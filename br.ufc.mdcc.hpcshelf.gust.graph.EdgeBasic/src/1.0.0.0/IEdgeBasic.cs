using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic {
	public interface IEdgeBasic<V> : BaseIEdgeBasic<V>, IEdge<V> where V:IVertex {
		IEdgeBasicInstance<V> newInstance (int source, int target);
		IEdgeBasicInstance<V> EInstance { get; }
	}
	public interface IEdgeBasicInstance<V> : IEdgeInstance<V>, ICloneable where V: IVertex{
	}
}