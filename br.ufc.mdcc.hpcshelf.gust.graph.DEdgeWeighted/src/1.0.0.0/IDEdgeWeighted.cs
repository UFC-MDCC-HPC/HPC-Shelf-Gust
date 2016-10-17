using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.common.Float;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DEdgeWeighted {
	public interface IDEdgeWeighted<V> : BaseIDEdgeWeighted<V>, IDEdge<V> where V:IDVertex {
		//IDEdgeWeightedInstance<V> DEdgeWeightedInstance { get; }

		IDEdgeWeightedInstance<V, int> DEdgeWeightedInstance { get; }
		IDEdgeWeightedInstance<V, T> InstanceTFactory<T> (T s, T t, float w);
	}
	public interface IDEdgeWeightedInstance<V, RV> : IDEdgeInstance<V, RV>, ICloneable where V:IDVertex {
		float Weight { get; set; }
	}
//	public interface IRootDEdgeWeighted<RV>: IRootDEdge<RV> {
//		IRootDEdgeWeighted<RV> newInstance (RV source, RV target, float weight);
//	}
}