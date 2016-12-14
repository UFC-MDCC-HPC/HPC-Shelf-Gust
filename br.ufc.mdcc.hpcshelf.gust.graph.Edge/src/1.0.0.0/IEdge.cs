using System;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.graph.Edge 
{
	public interface IEdge<V> : BaseIEdge<V>, IData 
	    where V:IVertex 
	{
		//IEdgeInstance<V> newInstance(IRootEdge<int> rde);
		//IEdgeInstance<V, int> EInstance { get; }
		IEdgeInstance<V, T> InstanceTFactory<T> (T s, T t, float f);
		IEdgeInstance<V, T> InstanceTFactory<T> (T s, T t);
	}
	public interface IEdgeInstance<V, TV> : IDataInstance, ICloneable where V:IVertex {
		TV Source { get; set; }
		TV Target { set; get; }
		float Weight { get; set; }
		IEdgeInstance<V, TV> newInstance();
		IEdgeInstance<V, TV> newInstance(TV s, TV t);
		IEdgeInstance<V, TV> newInstance(TV s, TV t, float w);
//		IVertexInstance Source { get; set; }
//		IVertexInstance Target { set; get; }
//		IRootEdge<int> RootEdge { get; }
	}
//	public interface IRootEdge<RV>: ICloneable {
//		RV Source { get; set; }
//		RV Target { set; get; }
//		float Weight { get; }
//		IRootEdge<RV> newInstance ();
//		IRootEdge<RV> newInstance (RV source, RV target);
//	}
}