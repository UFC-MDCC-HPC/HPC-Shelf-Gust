using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.common.Data;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DEdge {
	public interface IDEdge<V> : BaseIDEdge<V>, IData where V:IDVertex {
	}
	public interface IDEdgeInstance<V> : IDataInstance, ICloneable where V:IDVertex {
		IDVertexInstance Source { get; set;}
		IDVertexInstance Target { set; get; }
	}
}