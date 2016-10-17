using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DEdgeImpl {
	public class IDEdgeImpl<V>: BaseIDEdgeImpl<V>, IDEdge<V> where V:IDVertex {

		public IDEdgeImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}

		public object newInstance () {
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>)Vertices.newInstance ();
			this.instance = new IDEdgeInstanceImpl<V, int> (((IDVertexInstance)kv.Key).Id, ((IDVertexInstance)kv.Value).Id);
			return this.instance;
		}

		private object instance;
		public object Instance {
			get { return instance; }
			set { this.instance = value; }
		}

		public IDEdgeInstance<V, int> DEdgeInstance {
			get { 
				IDEdgeInstance<V, int> e=null;
				try{
					e = (IDEdgeInstance<V, int>)instance; 
				} catch {
					new InvalidCastException ("Cast Error: EInstance IDEdgeImpl");
				}
				return e;
			}
		}
		public IDEdgeInstance<V, T> InstanceTFactory<T> (T s, T t){
			IDEdgeInstance<V, T> instanceT = new IDEdgeInstanceImpl<V, T> (s, t);
			return instanceT;
		}
	}

	[Serializable]
	public class IDEdgeInstanceImpl<V, RV> : IDEdgeInstance<V, RV> where V: IDVertex{
		public IDEdgeInstanceImpl(){}
		public IDEdgeInstanceImpl(RV s, RV t):this(){
			this.source = s;
			this.target = t;
		}
		public IDEdgeInstanceImpl(RV s, RV t, float w):this(s, t){ }

		#region IDEdgeInstance implementation
		private RV source;
		private RV target;

		public RV Source { get { return source; } set { this.source = value; } }
		public RV Target { get { return target; } set { this.target = value; } }
		public float Weight { get { return 1.0f; } }

		public object ObjValue {
			get { return new Tuple<RV,RV>(source,target); }
			set { 
				this.source = ((Tuple<RV,RV>)value).Item1;
				this.target = ((Tuple<RV,RV>)value).Item2;
			}
		}
		public override string ToString () { return CommonFunc.edgeToString(source.GetHashCode (), target.GetHashCode ()); }
		public override bool Equals (object obj) {
			if (typeof(IDEdgeInstance<V, RV>).IsAssignableFrom (obj.GetType ())) {
				IDEdgeInstance<V, RV> o = (IDEdgeInstance<V,RV>)obj;
				if (o.Source.Equals(this.source) && o.Target.Equals(this.target))
					return true;
			}
			return false;
		}
		public override int GetHashCode () { return CommonFunc.pairingFunction (this.source.GetHashCode(), this.target.GetHashCode()); }

		public IDEdgeInstance<V,RV> newInstance () { return new IDEdgeInstanceImpl<V, RV> (); }
		public IDEdgeInstance<V,RV> newInstance (RV s, RV t) { return new IDEdgeInstanceImpl<V, RV> (s,t); }
		public IDEdgeInstance<V,RV> newInstance (RV s, RV t, float w) { return new IDEdgeInstanceImpl<V, RV> (s, t, w); }
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IDEdgeInstance<V,RV> clone;
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [1])) {
				return new IDEdgeInstanceImpl<V, RV> ((RV)((ICloneable)this.Source).Clone (), (RV)((ICloneable)this.Target).Clone ());
			}
			try {return this.MemberwiseClone(); } catch (NotSupportedException e) { }
			clone = new IDEdgeInstanceImpl<V, RV> (source, target);
			return clone;
		}
		#endregion
	}
	internal class CommonFunc{
		public static int pairingFunction (int a, int b) {
			var A = (ulong)(a >= 0 ? 2 * (long)a : -2 * (long)a - 1);
			var B = (ulong)(b >= 0 ? 2 * (long)b : -2 * (long)b - 1);
			var C = (long)((A >= B ? A * A + A + B : A + B * B) / 2);
			var R = a < 0 && b < 0 || a >= 0 && b >= 0 ? C : -C - 1;
			return (int)R;
		}
		public static string edgeToString(int a, int b) { return "" + a + "," + b + ""; }
	}
}
