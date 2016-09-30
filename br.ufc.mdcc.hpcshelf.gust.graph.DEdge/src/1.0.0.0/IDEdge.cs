using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.common.Data;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DEdge {
	public interface IDEdge<V> : BaseIDEdge<V>, IData where V:IDVertex {
		IRootDEdge<int> RootDEdge { get; }
		//void setDEdgeWith (IRootDEdge<int> rde); //
		//IDEdge<V> newInstance(IRootDEdge<int> rde); //
	}
	public interface IDEdgeInstance<V> : IDataInstance, ICloneable where V:IDVertex {
		IDVertexInstance Source { get; set;}
		IDVertexInstance Target { set; get; }
	}
	public interface IRootDEdge<RV> {
		RV Source { get; set; }
		RV Target { set; get; }
		IRootDEdge<RV> newInstance ();
		IRootDEdge<RV> newInstance (RV source, RV target);
		//IRootDEdge<RV> Instance { get; set;}
	}
}