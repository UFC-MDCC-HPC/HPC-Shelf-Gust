using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.common.Data;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.Edge {
	public interface IEdge<V> : BaseIEdge<V>, IData where V:IVertex {
	}
	public interface IEdgeInstance<V> : IDataInstance, ICloneable where V:IVertex {
		IVertexInstance Source { get; set;}
		IVertexInstance Target { set; get; }
	}
}