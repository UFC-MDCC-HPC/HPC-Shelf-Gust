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

		public IEdgeBasicInstance<V> newInstance (int source, int target) {
			newInstance ();
			instance.Source.Id = source;
			instance.Target.Id = target;
			return instance;
		}

		public object newInstance () {
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>)Vertices.newInstance ();
			this.instance = new IEdgeBasicInstanceImpl<V> (((IVertexInstance)kv.Key), ((IVertexInstance)kv.Value));
			return this.Instance;
		}

		private IEdgeBasicInstance<V> instance;
		public object Instance {
			get { return instance; }
			set { this.instance = (IEdgeBasicInstance<V>)value; }
		}

		public IEdgeBasicInstance<V> EInstance {
			get { return instance; }
		}
	}

	[Serializable]
	public class IEdgeBasicInstanceImpl<V> : IEdgeBasicInstance<V> where V: IVertex{

		public IEdgeBasicInstanceImpl(IVertexInstance s, IVertexInstance t){
			this.source = s;
			this.target = t;
		}

		#region IEdgeBasicInstance implementation
		private IVertexInstance source;
		private IVertexInstance target;

		public IVertexInstance Source {
			get { return source; }
			set { this.source = value; }
		}

		public IVertexInstance Target {
			get { return target; }
			set { this.target = value; }
		}

		public object ObjValue {
			get { return new Tuple<IVertexInstance,IVertexInstance>(source,target); }
			set { 
				this.source = ((Tuple<IVertexInstance,IVertexInstance>)value).Item1;
				this.target = ((Tuple<IVertexInstance,IVertexInstance>)value).Item2;
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
			if (typeof(IEdgeBasicInstance<V>).IsAssignableFrom (obj.GetType ())) {
				IEdgeBasicInstance<V> o = (IEdgeBasicInstance<V>)obj;
				if (o.Source.Id.Equals(this.source.Id) && o.Target.Id.Equals(this.target.Id))// && o.Weight == this.Weight)
					return true;
			}
			return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IEdgeBasicInstance<V> clone = new IEdgeBasicInstanceImpl<V>((IVertexInstance)this.Source.Clone(), (IVertexInstance)this.Target.Clone());
			return clone;
		}
		#endregion
	}
}
