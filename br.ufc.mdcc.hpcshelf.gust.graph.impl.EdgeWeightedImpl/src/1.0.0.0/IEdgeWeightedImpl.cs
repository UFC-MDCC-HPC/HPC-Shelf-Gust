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

		public IEdgeWeightedInstance<V> newInstance (int source, int target, float weight) {
			newInstance ();
			instance.Source.Id = source;
			instance.Target.Id = target;
			instance.Weight.Value = weight;
			return instance;
		}

		public object newInstance () {
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>)Vertices.newInstance ();
			IFloatInstance f = (IFloatInstance) Weight.newInstance (); 
			this.instance = new IEdgeWeightedInstanceImpl<V> (((IVertexInstance)kv.Key), ((IVertexInstance)kv.Value), f);
			return this.Instance;
		}

		private IEdgeWeightedInstance<V> instance;
		public object Instance {
			get { return instance; }
			set { this.instance = (IEdgeWeightedInstance<V>)value; }
		}

		public IEdgeWeightedInstance<V> EInstance {
			get { return instance; }
		}
	}

	[Serializable]
	public class IEdgeWeightedInstanceImpl<V> : IEdgeWeightedInstance<V> where V: IVertex{

		public IEdgeWeightedInstanceImpl(IVertexInstance s, IVertexInstance t, IFloatInstance w){
			this.source = s;
			this.target = t;
			this.weight = w;
		}

		#region IEdgeWeightedInstance implementation
		private IVertexInstance source;
		private IVertexInstance target;
		private IFloatInstance weight;

		public IVertexInstance Source {
			get { return source; }
			set { this.source = value; }
		}

		public IVertexInstance Target {
			get { return target; }
			set { this.target = value; }
		}

		public IFloatInstance Weight {
			get { return weight; }
			set { this.weight = value; }
		}

		public object ObjValue {
			get { return new Tuple<IVertexInstance,IVertexInstance,IFloatInstance>(source,target,weight); }
			set { 
				this.source = ((Tuple<IVertexInstance,IVertexInstance,IFloatInstance>)value).Item1;
				this.target = ((Tuple<IVertexInstance,IVertexInstance,IFloatInstance>)value).Item2;
				this.weight = ((Tuple<IVertexInstance,IVertexInstance,IFloatInstance>)value).Item3;
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
			return "" + source.Id + ":" + target.Id + "|"+string.Format("{0:N1}",weight.Value);
		}
		public override bool Equals (object obj) {
			if (typeof(IEdgeWeightedInstance<V>).IsAssignableFrom (obj.GetType ())) {
				IEdgeWeightedInstance<V> o = (IEdgeWeightedInstance<V>)obj;
				if (o.Source.Id.Equals(this.source.Id) && o.Target.Id.Equals(this.target.Id) && o.Weight.Value == this.Weight.Value)
					return true;
			}
			return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IEdgeWeightedInstance<V> clone = new IEdgeWeightedInstanceImpl<V>((IVertexInstance)this.Source.Clone(), (IVertexInstance)this.Target.Clone(), (IFloatInstance)this.Weight.Clone());
			return clone;
		}
		#endregion
	}
}
