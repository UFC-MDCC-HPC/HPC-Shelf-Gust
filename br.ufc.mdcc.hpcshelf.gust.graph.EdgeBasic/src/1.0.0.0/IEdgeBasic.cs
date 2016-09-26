using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic {
	public interface IEdgeBasic<V> : BaseIEdgeBasic<V>, IEdge<V> where V:IVertex {
//		IVertexInstance ESourceInstance { get; }
//		IVertexInstance ETargetInstance { get; }
		IEdgeBasicInstance newInstance (int source, int target);
		IEdgeBasicInstance EInstance { get; }
	}
	public interface IEdgeBasicInstance : IEdgeInstance, ICloneable{
		//int Source { get; }
		//int Target { get; }
	}
}