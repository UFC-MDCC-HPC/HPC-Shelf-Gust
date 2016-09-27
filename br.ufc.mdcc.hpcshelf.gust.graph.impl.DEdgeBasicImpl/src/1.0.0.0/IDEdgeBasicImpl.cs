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

		public IDEdgeBasicInstance<V> newInstance (int source, int target) {
			newInstance ();
			instance.Source.Id = source;
			instance.Target.Id = target;
			return instance;
		}

		public object newInstance () {
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>)Vertices.newInstance ();
			this.instance = new IDEdgeBasicInstanceImpl<V> (((IDVertexInstance)kv.Key), ((IDVertexInstance)kv.Value));
			return this.Instance;
		}

		private IDEdgeBasicInstance<V> instance;
		public object Instance {
			get { return instance; }
			set { this.instance = (IDEdgeBasicInstance<V>)value; }
		}

		public IDEdgeBasicInstance<V> EInstance {
			get { return instance; }
		}
	}

	[Serializable]
	public class IDEdgeBasicInstanceImpl<V> : IDEdgeBasicInstance<V> where V: IDVertex{

		public IDEdgeBasicInstanceImpl(IDVertexInstance s, IDVertexInstance t){
			this.source = s;
			this.target = t;
		}

		#region IDEdgeBasicInstance implementation
		private IDVertexInstance source;
		private IDVertexInstance target;

		public IDVertexInstance Source {
			get { return source; }
			set { this.source = value; }
		}

		public IDVertexInstance Target {
			get { return target; }
			set { this.target = value; }
		}

		public object ObjValue {
			get { return new Tuple<IDVertexInstance,IDVertexInstance>(source,target); }
			set { 
				this.source = ((Tuple<IDVertexInstance,IDVertexInstance>)value).Item1;
				this.target = ((Tuple<IDVertexInstance,IDVertexInstance>)value).Item2;
			}
		}
		public override int GetHashCode () {
			int a = this.source.Id;
			int b = this.target.Id;
			var A = (ulong)(a >= 0 ? 2 * (long)a : -2 * (long)a - 1);
			var B = (ulong)(b >= 0 ? 2 * (long)b : -2 * (long)b - 1);
			var C = (long)((A >= B ? A * A + A + B : A + B * B) / 2);
			var R = a < 0 && b < 0 || a >= 0 && b >= 0 ? C : -C - 1;
			return (int)R;
		}
		public override string ToString () {
			return "" + source.Id + ":" + target.Id + "";
		}
		public override bool Equals (object obj) {
			if (typeof(IDEdgeBasicInstance<V>).IsAssignableFrom (obj.GetType ())) {
				IDEdgeBasicInstance<V> o = (IDEdgeBasicInstance<V>)obj;
				if (o.Source.Id.Equals(this.source.Id) && o.Target.Id.Equals(this.target.Id))// && o.Weight == this.Weight)
					return true;
			}
			return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IDEdgeBasicInstance<V> clone = new IDEdgeBasicInstanceImpl<V>((IDVertexInstance)this.Source.Clone(), (IDVertexInstance)this.Target.Clone());
			return clone;
		}
		#endregion
	}
}
