using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Float;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeWeighted;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DEdgeWeightedImpl {
	public class IDEdgeWeightedImpl<V> : BaseIDEdgeWeightedImpl<V>, IDEdgeWeighted<V> where V:IDVertex {
		
		public IDEdgeWeightedImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}

		public IDEdgeWeightedInstance<V> newInstance (int source, int target, float weight) {
			newInstance ();
			instance.Source.Id = source;
			instance.Target.Id = target;
			instance.Weight.Value = weight;
			return instance;
		}

		public object newInstance () {
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>)Vertices.newInstance ();
			IFloatInstance f = (IFloatInstance) Weight.newInstance (); 
			this.instance = new IDEdgeWeightedInstanceImpl<V> (((IDVertexInstance)kv.Key), ((IDVertexInstance)kv.Value), f);
			return this.Instance;
		}

		private IDEdgeWeightedInstance<V> instance;
		public object Instance {
			get { return instance; }
			set { this.instance = (IDEdgeWeightedInstance<V>)value; }
		}

		public IDEdgeWeightedInstance<V> EInstance {
			get { return instance; }
		}
	}

	[Serializable]
	public class IDEdgeWeightedInstanceImpl<V> : IDEdgeWeightedInstance<V> where V: IDVertex{

		public IDEdgeWeightedInstanceImpl(IDVertexInstance s, IDVertexInstance t, IFloatInstance w){
			this.source = s;
			this.target = t;
			this.weight = w;
		}

		#region IDEdgeWeightedInstance implementation
		private IDVertexInstance source;
		private IDVertexInstance target;
		private IFloatInstance weight;

		public IDVertexInstance Source {
			get { return source; }
			set { this.source = value; }
		}

		public IDVertexInstance Target {
			get { return target; }
			set { this.target = value; }
		}

		public IFloatInstance Weight {
			get { return weight; }
			set { this.weight = value; }
		}

		public object ObjValue {
			get { return new Tuple<IDVertexInstance,IDVertexInstance,IFloatInstance>(source,target,weight); }
			set { 
				this.source = ((Tuple<IDVertexInstance,IDVertexInstance,IFloatInstance>)value).Item1;
				this.target = ((Tuple<IDVertexInstance,IDVertexInstance,IFloatInstance>)value).Item2;
				this.weight = ((Tuple<IDVertexInstance,IDVertexInstance,IFloatInstance>)value).Item3;
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
			if (typeof(IDEdgeWeightedInstance<V>).IsAssignableFrom (obj.GetType ())) {
				IDEdgeWeightedInstance<V> o = (IDEdgeWeightedInstance<V>)obj;
				if (o.Source.Id.Equals(this.source.Id) && o.Target.Id.Equals(this.target.Id) && o.Weight.Value == this.Weight.Value)
					return true;
			}
			return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IDEdgeWeightedInstance<V> clone = new IDEdgeWeightedInstanceImpl<V>((IDVertexInstance)this.Source.Clone(), (IDVertexInstance)this.Target.Clone(), (IFloatInstance)this.Weight.Clone());
			return clone;
		}
		#endregion
	}
}
