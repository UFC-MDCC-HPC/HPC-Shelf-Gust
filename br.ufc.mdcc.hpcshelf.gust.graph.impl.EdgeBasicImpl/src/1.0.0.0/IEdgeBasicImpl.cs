using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.EdgeBasicImpl {
	public class IEdgeBasicImpl<V>: BaseIEdgeBasicImpl<V>, IEdgeBasic<V> where V:IVertex {
		
		public IEdgeBasicImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}

		public IEdgeBasicInstance newInstance (int source, int target) {
			IEdgeBasicInstance instance = (IEdgeBasicInstance)newInstance ();
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>) Vertices.newInstance ();
			((IVertexInstance)kv.Key).Id = source;
			((IVertexInstance)kv.Value).Id = source;
			//instance.Source = source;
			//instance.Target = target;
			return instance;
		}

		public void setEdge (int source, int target) {
			//ESourceInstance.Id = source;
			//ETargetInstance.Id = target;
			//instance.Source = source;
			//instance.Target = target;
		}

		public object newInstance () {
			Vertices.newInstance ();
			this.instance = new IEdgeBasicInstanceImpl ();
			return this.Instance;
		}

		private IEdgeBasicInstance instance;

		public object Instance {
			get { return instance; }
			set { this.instance = (IEdgeBasicInstance)value; }
		}

		public IEdgeBasicInstance EInstance {
			get { return instance; }
		}
		public int ESource {
			get {
				return ((IVertexInstance)KVInstance.Key).Id;
			}
			set { 
				((IVertexInstance)KVInstance.Key).Id = value;
			}
		}
		public int ETarget {
			get {
				return ((IVertexInstance)KVInstance.Value).Id;
			}
			set { 
				((IVertexInstance)KVInstance.Value).Id = value;

			}
		}
	}

	[Serializable]
	public class IEdgeBasicInstanceImpl : IEdgeBasicInstance {

		#region IEdgeBasicInstance implementation
		private int source;
		private int target;

		public int Source {
			get { return source; }
			set { this.source = value; }
		}

		public int Target {
			get { return target; }
			set { this.target = value; }
		}

		public object ObjValue {
			get { return new Tuple<int,int>(source,target); }
			set { 
				this.source = ((Tuple<int,int>)value).Item1;
				this.target = ((Tuple<int,int>)value).Item2;
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
			return "" + source + ":" + target + "";
		}

		public override bool Equals (object obj) {
			if (typeof(IEdgeBasicInstance).IsAssignableFrom (obj.GetType ())) {
				IEdgeBasicInstance o = (IEdgeBasicInstance)obj;
				if (o.Source.Equals(this.source) && o.Target.Equals(this.target))// && o.Weight == this.Weight)
					return true;
			}
			return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IEdgeBasicInstance clone = new IEdgeBasicInstanceImpl ();
			clone.Source = this.source;
			clone.Target = this.target;
			return clone;
		}
		#endregion
	}
}
