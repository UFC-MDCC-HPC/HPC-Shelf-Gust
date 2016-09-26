using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.common.Float;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DEdgeWeighted {
	public interface IDEdgeWeighted<V> : BaseIDEdgeWeighted<V>, IDEdge<V> where V:IVertex {
		IDEdgeWeightedInstance<V> newInstance (int source, int target, float weight);
		IDEdgeWeightedInstance<V> EInstance { get; }
	}
	public interface IDEdgeWeightedInstance<V> : IDEdgeInstance<V>, ICloneable where V:IVertex {
		IFloatInstance Weight { get; set; }
	}
}