using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic {
	public interface IEdgeBasic<V> : BaseIEdgeBasic<V>, IEdge<V> where V:IVertex {
		IEdgeBasicInstance<V, int> EdgeBasicInstance { get; }
		IEdgeBasicInstance<V, T> InstanceTFactory<T> (T s, T t, float w);
	}
	public interface IEdgeBasicInstance<V, TV> : IEdgeInstance<V, TV>, ICloneable where V: IVertex{
	}
//	public interface IRootEdgeBasic<RV>: IRootEdge<RV> {
//	}
}