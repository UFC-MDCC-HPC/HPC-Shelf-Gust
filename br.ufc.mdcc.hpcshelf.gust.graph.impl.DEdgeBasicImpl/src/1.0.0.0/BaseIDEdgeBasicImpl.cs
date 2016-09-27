/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeBasic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DEdgeBasicImpl {
	public abstract class BaseIDEdgeBasicImpl<V>: DataStructure, BaseIDEdgeBasic<V>
		where V:IDVertex {

		private IKVPair<V,V> vertices = null;

		protected IKVPair<V,V> Vertices {
			get {
				if (this.vertices == null)
					this.vertices = (IKVPair<V,V>)Services.getPort ("vertices");
				return this.vertices;
			}
		}
	}
}