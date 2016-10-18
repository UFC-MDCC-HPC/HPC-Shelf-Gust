using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Float;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.EdgeWeightedImpl {
	public class IEdgeWeightedImpl<V> : BaseIEdgeWeightedImpl<V>, IEdgeWeighted<V> where V:IVertex {
		
		public IEdgeWeightedImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}
		public object newInstance () {
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>)Vertices.newInstance ();
			this.instance = new IEdgeWeightedInstanceImpl<V, int> (((IVertexInstance)kv.Key).Id, ((IVertexInstance)kv.Value).Id, 1.0f);
			return this.instance;
		}
		private object instance;
		public object Instance {
			get { return instance; }
			set { this.instance = value; }
		}

		public IEdgeWeightedInstance<V, int> EdgeWeightedInstance {
			get { 
				IEdgeWeightedInstance<V, int> e=null;
				try{
					e = (IEdgeWeightedInstance<V, int>)instance; 
				} catch {
					new InvalidCastException ("Cast Error: IEdgeWeightedInstance<V, int> IEdgeWeightedInstanceImpl");
				}
				return e;
			}
		}
		public IEdgeWeightedInstance<V, T> InstanceTFactory<T> (T s, T t, float w){
			//IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>)Vertices.newInstance ();
			IEdgeWeightedInstance<V, T> instanceT = new IEdgeWeightedInstanceImpl<V, T> (s, t, w);
			return instanceT;
		}
		public IEdgeInstance<V, T> InstanceTFactory<T> (T s, T t){
			return InstanceTFactory<T>(s,t,1.0f);
		}
	}

	[Serializable]
	public class IEdgeWeightedInstanceImpl<V, TV> : IEdgeWeightedInstance<V, TV> where V: IVertex{
		public IEdgeWeightedInstanceImpl(){}
		public IEdgeWeightedInstanceImpl(TV s, TV t, float w){
			this.source = s;
			this.target = t;
			this.weight = w;
		}

		#region IEdgeWeightedInstance implementation
		private TV source;
		private TV target;
		private float weight = 1.0f;

		public TV Source { get { return source; } set { this.source = value; } }
		public TV Target { get { return target; } set { this.target = value; } }
		public float Weight { get { return weight; } set { this.weight = value; } }

		public object ObjValue {
			get { return new Tuple<TV,TV,float>(source,target,weight); }
			set { 
				this.source = ((Tuple<TV,TV,float>)value).Item1;
				this.target = ((Tuple<TV,TV,float>)value).Item2;
				this.weight = ((Tuple<TV,TV,float>)value).Item3;
			}
		}
		public override int GetHashCode () {
			return CommonFunc.pairingFunction (this.source.GetHashCode(), this.target.GetHashCode());
		}
		public override string ToString () { return CommonFunc.edgeToString(source.GetHashCode(),target.GetHashCode(),weight);}
		public override bool Equals (object obj) {
			if (typeof(IEdgeWeightedInstance<V, TV>).IsAssignableFrom (obj.GetType ())) {
				IEdgeWeightedInstance<V, TV> o = (IEdgeWeightedInstance<V, TV>)obj;
				if (o.Source.Equals(this.source) && o.Target.Equals(this.target))// && o.Weight.Value == this.Weight.Value)
					return true;
			}
			return false;
		}
		public IEdgeInstance<V,TV> newInstance () { return new IEdgeWeightedInstanceImpl<V, TV> (); }
		public IEdgeInstance<V,TV> newInstance (TV s, TV t) { return new IEdgeWeightedInstanceImpl<V, TV> (s,t, 1.0f); }
		public IEdgeInstance<V,TV> newInstance (TV s, TV t, float w) { return new IEdgeWeightedInstanceImpl<V, TV> (s, t, w); }
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IEdgeWeightedInstance<V,TV> clone;
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [1])) {
				return new IEdgeWeightedInstanceImpl<V, TV> ((TV)((ICloneable)this.Source).Clone (), (TV)((ICloneable)this.Target).Clone (), 1.0f);
			}
			try {return this.MemberwiseClone(); } catch (NotSupportedException e) { }
			clone = new IEdgeWeightedInstanceImpl<V, TV> (source, target, 1.0f);
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
		public static string edgeToString(int a, int b, float w) { return "" + a + ":"+ b + "|"+string.Format("{0:N1}",w); }
	}
}
