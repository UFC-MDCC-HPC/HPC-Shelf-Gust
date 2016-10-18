using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DEdgeBasic {
	public interface IDEdgeBasic<V> : BaseIDEdgeBasic<V>, IDEdge<V> where V:IDVertex {
		IDEdgeBasicInstance<V, int> DEdgeBasicInstance { get; }
		IDEdgeBasicInstance<V, T> InstanceTFactory<T> (T s, T t, float w);
	}
	public interface IDEdgeBasicInstance<V, TV> : IDEdgeInstance<V, TV>, ICloneable where V: IDVertex{
	}
//	public interface IRootDEdgeBasic<RV>: IRootDEdge<RV> {
//	}
}