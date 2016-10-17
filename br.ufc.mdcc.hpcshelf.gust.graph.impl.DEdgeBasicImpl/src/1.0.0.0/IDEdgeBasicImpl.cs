using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeBasic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DEdgeBasicImpl {
	public class IDEdgeBasicImpl<V>: BaseIDEdgeBasicImpl<V>, IDEdgeBasic<V> where V:IDVertex {
		
		public IDEdgeBasicImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}
		public object newInstance () {
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>)Vertices.newInstance ();
			this.instance = new IDEdgeBasicInstanceImpl<V, int> (((IDVertexInstance)kv.Key).Id, ((IDVertexInstance)kv.Value).Id);
			return this.instance;
		}
		private object instance;
		public object Instance {
			get { return instance; }
			set { this.instance = value; }
		}

		public IDEdgeBasicInstance<V, int> DEdgeBasicInstance {
			get { 
				IDEdgeBasicInstance<V, int> e=null;
				try{
					e = (IDEdgeBasicInstance<V, int>)instance; 
				} catch {
					new InvalidCastException ("Cast Error: IDEdgeBasicInstance<V, int> IDEdgeBasicImpl");
				}
				return e;
			}
		}
		public IDEdgeBasicInstance<V, T> InstanceTFactory<T> (T s, T t, float w){
			IDEdgeBasicInstance<V, T> instanceT = new IDEdgeBasicInstanceImpl<V, T> (s, t);
			return instanceT;
		}
		public IDEdgeInstance<V, T> InstanceTFactory<T> (T s, T t){
			return InstanceTFactory<T>(s,t,1.0f);
		}
	}

	[Serializable]
	public class IDEdgeBasicInstanceImpl<V, RV> : IDEdgeBasicInstance<V, RV> where V: IDVertex{
		public IDEdgeBasicInstanceImpl(){}
		public IDEdgeBasicInstanceImpl(RV s, RV t):this(){
			this.source = s;
			this.target = t;
		}
		public IDEdgeBasicInstanceImpl(RV s, RV t, float w):this(s, t){ }

		#region IDEdgeBasicInstance implementation
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
		public override int GetHashCode () { return CommonFunc.pairingFunction (this.source.GetHashCode(), this.target.GetHashCode()); }
		public override string ToString () { return CommonFunc.edgeToString(source.GetHashCode(),target.GetHashCode());}
		public override bool Equals (object obj) {
			if (typeof(IDEdgeBasicInstance<V, RV>).IsAssignableFrom (obj.GetType ())) {
				IDEdgeBasicInstance<V, RV> o = (IDEdgeBasicInstance<V, RV>)obj;
				if (o.Source.Equals(this.source) && o.Target.Equals(this.target))// && o.Weight == this.Weight)
					return true;
			}
			return false;
		}

		public IDEdgeInstance<V,RV> newInstance () { return new IDEdgeBasicInstanceImpl<V, RV> (); }
		public IDEdgeInstance<V,RV> newInstance (RV s, RV t) { return new IDEdgeBasicInstanceImpl<V, RV> (s,t); }
		public IDEdgeInstance<V,RV> newInstance (RV s, RV t, float w) { return new IDEdgeBasicInstanceImpl<V, RV> (s, t, w); }
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IDEdgeBasicInstance<V,RV> clone;
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [1])) {
				return new IDEdgeBasicInstanceImpl<V, RV> ((RV)((ICloneable)this.Source).Clone (), (RV)((ICloneable)this.Target).Clone ());
			}
			try {return this.MemberwiseClone(); } catch (NotSupportedException e) { }
			clone = new IDEdgeBasicInstanceImpl<V, RV> (source, target);
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
