using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeBasic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DEdgeBasicImpl {
	public class IDEdgeBasicImpl<V>: BaseIDEdgeBasicImpl<V>, IDEdgeBasic<V> where V:IVertex {
		
		public IDEdgeBasicImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}

		public IDEdgeInstance newInstance (int source, int target) {
			IDEdgeInstance instance = (IDEdgeInstance)newInstance ();
			instance.Source = source;
			instance.Target = target;
			return instance;
		}

		public object newInstance () {
			this.instance = new IDEdgeInstanceImpl ();
			return this.Instance;
		}

		private IDEdgeInstance instance;

		public object Instance {
			get { return instance; }
			set { this.instance = (IDEdgeInstance)value; }
		}
//		public IDEdgeInstance Vertices {
//			get { return instance; }
//		}
	}

	[Serializable]
	public class IDEdgeInstanceImpl : IDEdgeInstance {

		#region IDEdgeInstance implementation
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
			if (typeof(IDEdgeInstance).IsAssignableFrom (obj.GetType ())) {
				IDEdgeInstance o = (IDEdgeInstance)obj;
				if (o.Source.Equals(this.source) && o.Target.Equals(this.target))// && o.Weight == this.Weight)
					return true;
			}
			return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IDEdgeInstance clone = new IDEdgeInstanceImpl ();
			clone.Source = this.source;
			clone.Target = this.target;
			return clone;
		}
		#endregion
	}
}
