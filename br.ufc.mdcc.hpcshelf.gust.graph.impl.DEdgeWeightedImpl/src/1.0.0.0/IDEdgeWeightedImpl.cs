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

		public IRootDEdge<int> RootDEdge {
			get {
				return new IRootDEdgeWeightedImpl<int> (instance.Source.Id, instance.Target.Id);
			}
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
			return CommonFunc.pairingFunction (this.source.Id, this.target.Id);
		}
		public override string ToString () {
			return "" + source.Id + ":" + target.Id + "|"+string.Format("{0:N1}",weight.Value);
		}
		public override bool Equals (object obj) {
			if (typeof(IDEdgeWeightedInstance<V>).IsAssignableFrom (obj.GetType ())) {
				IDEdgeWeightedInstance<V> o = (IDEdgeWeightedInstance<V>)obj;
				if (o.Source.Id.Equals(this.source.Id) && o.Target.Id.Equals(this.target.Id))// && o.Weight.Value == this.Weight.Value)
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

	//************************************** IRootDEdgeWeighted **************************************
	public class IRootDEdgeWeightedImpl<RV>: IRootDEdgeWeighted<RV> {

		private RV _source = default(RV);
		private RV _target = default(RV);
		private float _weight = 1.0f;

		public IRootDEdgeWeightedImpl(){ }
		public IRootDEdgeWeightedImpl(RV source, RV target):this(){ 
			this._source = source;
			this._target = target;
		}
		public IRootDEdgeWeightedImpl(RV source, RV target, float weight):this(source, target) { 
			this._weight = weight;
		}

		public RV Source {
			get { return _source; }
			set { this._source = value;	}
		}
		public RV Target {
			get { return _target; }
			set { this._target = value;	}
		}
		public virtual float Weight {
			get { return _weight; }
		}

		public virtual IRootDEdge<RV> newInstance (){
			return new IRootDEdgeWeightedImpl<RV> ();
		}
		public virtual IRootDEdge<RV> newInstance (RV source, RV target){
			return new IRootDEdgeWeightedImpl<RV> (source, target);
		}
		public virtual IRootDEdgeWeighted<RV> newInstance (RV source, RV target, float weight){
			return new IRootDEdgeWeightedImpl<RV> (source, target, weight);
		}
		public virtual string ToString () {
			return "" + Source + ":"+ Target + "|"+string.Format("{0:N1}",Weight);
		}
		public override bool Equals (object obj) {
			if (typeof(IRootDEdgeWeighted<RV>).IsAssignableFrom (obj.GetType ())) {
				IRootDEdgeWeighted<RV> o = (IRootDEdgeWeighted<RV>)obj;
				if (o.Source.Equals(this.Source) && o.Target.Equals(this.Target))// && o.Weight == this.Weight)
					return true;
			}
			return false;
		}
		public override int GetHashCode () {
			return CommonFunc.pairingFunction (this.Source.GetHashCode (), this.Target.GetHashCode ());
		}
	}
	internal class CommonFunc{
		public static int pairingFunction (int a, int b) {
			var A = (ulong)(a >= 0 ? 2 * (long)a : -2 * (long)a - 1);
			var B = (ulong)(b >= 0 ? 2 * (long)b : -2 * (long)b - 1);
			var C = (long)((A >= B ? A * A + A + B : A + B * B) / 2);
			var R = a < 0 && b < 0 || a >= 0 && b >= 0 ? C : -C - 1;
			return (int)R;
		}
	}
}
