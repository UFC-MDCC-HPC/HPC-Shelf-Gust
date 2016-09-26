/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.EdgeBasicImpl {
	public abstract class BaseIEdgeBasicImpl<V>: DataStructure, BaseIEdgeBasic<V>
		where V:IVertex {
//		private V vertexFactory = default(V);
//
//		protected V VertexFactory {
//			get {
//				if (this.vertexFactory == null)
//					this.vertexFactory = (V)Services.getPort ("vertexFactory");
//				return this.vertexFactory;
//			}
//		}

		private IKVPair<V,V> vertices = null;

		protected IKVPair<V,V> Vertices {
			get {
				if (this.vertices == null)
					this.vertices = (IKVPair<V,V>)Services.getPort ("vertices");
				return this.vertices;
			}
		}

		protected IKVPairInstance<V,V> KVInstance {
			get {
				return (IKVPairInstance<V,V>)Vertices.Instance;
			}
		}
	}
}