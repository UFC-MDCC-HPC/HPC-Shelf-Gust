using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.common.Data;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DEdge {
	public interface IDEdge<V> : BaseIDEdge<V>, IData where V:IVertex {
	}
	public interface IDEdgeInstance<V> : IDataInstance, ICloneable where V:IVertex {
		IVertexInstance Source { get; set;}
		IVertexInstance Target { set; get; }
	}
}