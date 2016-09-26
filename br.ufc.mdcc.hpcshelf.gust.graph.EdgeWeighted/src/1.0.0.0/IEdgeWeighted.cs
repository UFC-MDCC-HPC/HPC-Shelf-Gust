using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.common.Float;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted {
	public interface IEdgeWeighted<V> : BaseIEdgeWeighted<V>, IEdge<V> where V:IVertex {
//		IVertexInstance ESourceInstance { get; }
//		IVertexInstance ETargetInstance { get; }

		IEdgeWeightedInstance newInstance (int source, int target, float weight);
//		void setEdge (int source, int target, float weight);
		IFloatInstance EWeightInstance { get; }
	}
	public interface IEdgeWeightedInstance : IEdgeInstance, ICloneable{
		//int Source { get; }
		//int Target { get; }
		float Weight { get; }
	}
}