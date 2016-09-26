using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.common.Float;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted {
	public interface IEdgeWeighted<V> : BaseIEdgeWeighted<V>, IEdge<V> where V:IVertex {
		IEdgeWeightedInstance<V> newInstance (int source, int target, float weight);
		IEdgeWeightedInstance<V> EInstance { get; }
	}
	public interface IEdgeWeightedInstance<V> : IEdgeInstance<V>, ICloneable where V:IVertex {
		IFloatInstance Weight { get; set; }
	}
}