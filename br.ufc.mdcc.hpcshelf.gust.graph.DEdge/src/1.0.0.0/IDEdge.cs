using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.common.Data;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DEdge {
	public interface IDEdge<V> : BaseIDEdge<V>, IData where V:IDVertex {
		//IDEdgeInstance<V> newInstance(IRootDEdge<int> rde);
		//IDEdgeInstance<V, int> EInstance { get; }
		//IDEdgeInstance<V, T> InstanceTFactory<T> (T s, T t, float f);
		IDEdgeInstance<V, T> InstanceTFactory<T> (T s, T t);
	}
	public interface IDEdgeInstance<V, TV> : IDataInstance, ICloneable where V:IDVertex {
		TV Source { get; set; }
		TV Target { set; get; }
		float Weight { get; }
		IDEdgeInstance<V, TV> newInstance();
		IDEdgeInstance<V, TV> newInstance(TV s, TV t);
		IDEdgeInstance<V, TV> newInstance(TV s, TV t, float w);
//		IDVertexInstance Source { get; set; }
//		IDVertexInstance Target { set; get; }
//		IRootDEdge<int> RootDEdge { get; }
	}
//	public interface IRootDEdge<RV>: ICloneable {
//		RV Source { get; set; }
//		RV Target { set; get; }
//		float Weight { get; }
//		IRootDEdge<RV> newInstance ();
//		IRootDEdge<RV> newInstance (RV source, RV target);
//	}
}