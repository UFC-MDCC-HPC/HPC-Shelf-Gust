using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.EdgeWeightedImpl {
	public class IEdgeWeightedImpl<V> : BaseIEdgeWeightedImpl<V>, IEdgeWeighted<V> where V:IVertex {
		public IEdgeWeightedImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}

		public IEdgeInstance newInstance (int source, int target) {
			IEdgeWeightedInstance instance = (IEdgeWeightedInstance)newInstance ();
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>) Vertices.newInstance ();
			((IVertexInstance)kv.Key).Id = source;
			((IVertexInstance)kv.Value).Id = source;
			EWeightInstance.Value = 1.0f;
			instance.Source = source;
			instance.Target = target;
			instance.Weight = EWeightInstance.Value;
			return instance;
		}

		public IEdgeWeightedInstance newInstance (int source, int target, float weight) {
			IEdgeWeightedInstance instance = (IEdgeWeightedInstance)newInstance ();
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>) Vertices.newInstance ();
			((IVertexInstance)kv.Key).Id = source;
			((IVertexInstance)kv.Value).Id = source;
			EWeightInstance.Value = weight;
			instance.Source = source;
			instance.Target = target;
			instance.Weight = EWeightInstance.Value;
			return instance;
		}

		public void setEdge (int source, int target) {
			ESourceInstance.Id = source;
			ETargetInstance.Id = target;
			instance.Source = source;
			instance.Target = target;
		}

		public object newInstance () {
			Vertices.newInstance ();
			Weight.newInstance ();
			this.instance = new IEdgeWeightedInstanceImpl ();
			return this.Instance;
		}

		private IEdgeWeightedInstance instance;

		public object Instance {
			get { return instance; }
			set { this.instance = (IEdgeWeightedInstance)value; }
		}
	}

	[Serializable]
	public class IEdgeWeightedInstanceImpl : IEdgeWeightedInstance {

		#region IEdgeWeightedInstance implementation
		private int source;
		private int target;
		private float weight;

		public int Source {
			get { return source; }
			set { this.source = value; }
		}

		public int Target {
			get { return target; }
			set { this.target = value; }
		}

		public float Weight {
			get { return weight; }
			set { this.weight = value; }
		}

		public object ObjValue {
			get { return new Tuple<int,int,float>(source,target,weight); }
			set { 
				this.source = ((Tuple<int,int,float>)value).Item1;
				this.target = ((Tuple<int,int,float>)value).Item2;
				this.weight = ((Tuple<int,int,float>)value).Item3;
			}
		}
		public override int GetHashCode () {
			int a = this.source;
			int b = this.target; 
			var A = (ulong)(a >= 0 ? 2 * (long)a : -2 * (long)a - 1);
			var B = (ulong)(b >= 0 ? 2 * (long)b : -2 * (long)b - 1);
			var C = (long)((A >= B ? A * A + A + B : A + B * B) / 2);
			var R = a < 0 && b < 0 || a >= 0 && b >= 0 ? C : -C - 1;
			return (int)R;
		}
		public override string ToString () {
			return "" + source + ":"+ target + "|"+string.Format("{0:N1}",weight);
		}

		public override bool Equals (object obj) {
			if (typeof(IEdgeWeightedInstance).IsAssignableFrom (obj.GetType ())) {
				IEdgeWeightedInstance o = (IEdgeWeightedInstance)obj;
				if (o.Source.Equals(this.source) && o.Target.Equals(this.target) && o.Weight == this.Weight)
					return true;
			}
			return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IEdgeWeightedInstance clone = new IEdgeWeightedInstanceImpl ();
			clone.Source = this.source;
			clone.Target = this.target;
			clone.Weight = this.weight;
			return clone;
		}
		#endregion
	}
}
