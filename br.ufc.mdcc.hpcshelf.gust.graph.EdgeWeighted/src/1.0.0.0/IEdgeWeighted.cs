using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.common.Float;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted {
	public interface IEdgeWeighted<V> : BaseIEdgeWeighted<V>, IEdge<V> where V:IVertex {
		//IEdgeWeightedInstance<V> EdgeWeightedInstance { get; }

		IEdgeWeightedInstance<V, int> EdgeWeightedInstance { get; }
		IEdgeWeightedInstance<V, T> InstanceTFactory<T> (T s, T t, float w);
	}
	public interface IEdgeWeightedInstance<V, TV> : IEdgeInstance<V, TV>, ICloneable where V:IVertex {
		//float Weight { get; set; }
	}
//	public interface IRootEdgeWeighted<RV>: IRootEdge<RV> {
//		IRootEdgeWeighted<RV> newInstance (RV source, RV target, float weight);
//	}
}