/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.common.Float;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeWeighted;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DEdgeWeightedImpl {
	public abstract class BaseIDEdgeWeightedImpl<V>: DataStructure, BaseIDEdgeWeighted<V> where V:IDVertex {
		private IKVPair<V,V> vertices = null;

		protected IKVPair<V,V> Vertices {
			get {
				if (this.vertices == null)
					this.vertices = (IKVPair<V,V>)Services.getPort ("vertices");
				return this.vertices;
			}
		}

		private IFloat weight = null;
		protected IFloat Weight {
			get {
				if (this.weight == null)
					this.weight = (IFloat)Services.getPort ("weight");
				return this.weight;
			}
		}
	}
}