using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.common.Data;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.Edge {
	public interface IEdge<V> : BaseIEdge<V>, IData where V:IVertex {
		int ESource { get; set; }
		int ETarget { get; set; }
	}
	public interface IEdgeInstance : IDataInstance, ICloneable{
		int Source { get; }
		int Target { get; }
	}
}