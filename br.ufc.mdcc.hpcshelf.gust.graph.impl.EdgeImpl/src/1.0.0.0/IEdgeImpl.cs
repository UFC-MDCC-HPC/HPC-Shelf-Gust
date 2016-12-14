using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.EdgeImpl {
	public class IEdgeImpl<V>: BaseIEdgeImpl<V>, IEdge<V> where V:IVertex {

		public IEdgeImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}

		public object newInstance () {
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>)Vertices.newInstance ();
			this.instance = new IEdgeInstanceImpl<V, int> (((IVertexInstance)kv.Key).Id, ((IVertexInstance)kv.Value).Id);
			return this.instance;
		}

		private object instance;
		public object Instance {
			get { return instance; }
			set { this.instance = value; }
		}

		public IEdgeInstance<V, int> EdgeInstance {
			get { 
				IEdgeInstance<V, int> e=null;
				try{
					e = (IEdgeInstance<V, int>)instance; 
				} catch {
					new InvalidCastException ("Cast Error: EInstance IEdgeImpl");
				}
				return e;
			}
		}
		public IEdgeInstance<V, T> InstanceTFactory<T> (T s, T t){
			IEdgeInstance<V, T> instanceT = new IEdgeInstanceImpl<V, T> (s, t);
			return instanceT;
		}
		public IEdgeInstance<V, T> InstanceTFactory<T> (T s, T t, float w){
			IEdgeInstance<V, T> instanceT = new IEdgeInstanceImpl<V, T> (s, t, w);
			return instanceT;
		}
	}

	[Serializable]
	public class IEdgeInstanceImpl<V, TV> : IEdgeInstance<V, TV> where V: IVertex{
		public IEdgeInstanceImpl(){}
		public IEdgeInstanceImpl(TV s, TV t):this(){
			this.source = s;
			this.target = t;
		}
		public IEdgeInstanceImpl(TV s, TV t, float w):this(s, t){ this.Weight = w; }

		#region IEdgeInstance implementation
		private TV source;
		private TV target;

		public TV Source { get { return source; } set { this.source = value; } }
		public TV Target { get { return target; } set { this.target = value; } }
		public float Weight { get { return CommonFunc.WeightDefault; } set { CommonFunc.WeightDefault = value; } }

		public object ObjValue {
			get { return new Tuple<TV,TV>(source,target); }
			set { 
				this.source = ((Tuple<TV,TV>)value).Item1;
				this.target = ((Tuple<TV,TV>)value).Item2;
			}
		}
		public override string ToString () { return CommonFunc.edgeToString(source.GetHashCode (), target.GetHashCode ()); }
		public override bool Equals (object obj) {
			if (typeof(IEdgeInstance<V, TV>).IsAssignableFrom (obj.GetType ())) {
				IEdgeInstance<V, TV> o = (IEdgeInstance<V,TV>)obj;
				if (o.Source.Equals(this.source) && o.Target.Equals(this.target))
					return true;
			}
			return false;
		}
		public override int GetHashCode () { return CommonFunc.pairingFunction (this.source.GetHashCode(), this.target.GetHashCode()); }

		public IEdgeInstance<V,TV> newInstance () { return new IEdgeInstanceImpl<V, TV> (); }
		public IEdgeInstance<V,TV> newInstance (TV s, TV t) { return new IEdgeInstanceImpl<V, TV> (s,t); }
		public IEdgeInstance<V,TV> newInstance (TV s, TV t, float w) { return new IEdgeInstanceImpl<V, TV> (s, t, w); }
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IEdgeInstance<V,TV> clone;
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [1])) {
				return new IEdgeInstanceImpl<V, TV> ((TV)((ICloneable)this.Source).Clone (), (TV)((ICloneable)this.Target).Clone ());
			}
			try {return this.MemberwiseClone(); } catch (NotSupportedException e) { }
			clone = new IEdgeInstanceImpl<V, TV> (source, target);
			return clone;
		}
		#endregion
	}
	internal static class CommonFunc{
		public static float WeightDefault = 1.0f;
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
