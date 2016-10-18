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

		public IEdgeInstance newInstance (int source, int target) {
			IEdgeInstance instance = (IEdgeInstance)newInstance ();
			instance.Source = source;
			instance.Target = target;
			return instance;
		}

		public object newInstance () {
			this.instance = new IEdgeInstanceImpl ();
			return this.Instance;
		}

		private IEdgeInstance instance;

		public object Instance {
			get { return instance; }
			set { this.instance = (IEdgeInstance)value; }
		}
//		public IEdgeInstance Vertices {
//			get { return instance; }
//		}
	}

	[Serializable]
	public class IEdgeInstanceImpl : IEdgeInstance {

		#region IEdgeInstance implementation
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
			return "" + source + "," + target + "";
		}

		public override bool Equals (object obj) {
			if (typeof(IEdgeInstance).IsAssignableFrom (obj.GetType ())) {
				IEdgeInstance o = (IEdgeInstance)obj;
				if (o.Source.Equals(this.source) && o.Target.Equals(this.target))// && o.Weight == this.Weight)
					return true;
			}
			return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IEdgeInstance clone = new IEdgeInstanceImpl ();
			clone.Source = this.source;
			clone.Target = this.target;
			return clone;
		}
		#endregion
	}
}
