using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DEdgeImpl {
	public class IDEdgeImpl<V>: BaseIDEdgeImpl<V>, IDEdge<V> where V:IDVertex {

		public IDEdgeImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}

		public IDEdgeInstance<V> newInstance (int source, int target) {
			newInstance ();
			instance.Source.Id = source;
			instance.Target.Id = target;
			return instance;
		}

		public object newInstance () {
			IKVPairInstance<V,V> kv = (IKVPairInstance<V,V>)Vertices.newInstance ();
			this.instance = new IDEdgeInstanceImpl<V> (((IDVertexInstance)kv.Key), ((IDVertexInstance)kv.Value));
			return this.Instance;
		}

		private IDEdgeInstance<V> instance;
		public object Instance {
			get { return instance; }
			set { this.instance = (IDEdgeInstance<V>)value; }
		}

		public IDEdgeInstance<V> EInstance {
			get { return instance; }
		}

		public IRootDEdge<int> RootDEdge {
			get {
				return new IRootDEdgeImpl<int> (instance.Source.Id, instance.Target.Id);
			}
		}
	}

	[Serializable]
	public class IDEdgeInstanceImpl<V> : IDEdgeInstance<V> where V: IDVertex{

		public IDEdgeInstanceImpl(IDVertexInstance s, IDVertexInstance t){
			this.source = s;
			this.target = t;
		}

		#region IDEdgeInstance implementation
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
			return CommonFunc.pairingFunction (this.source.Id, this.target.Id);
		}
		public override string ToString () {
			return "" + source.Id + ":" + target.Id + "";
		}
		public override bool Equals (object obj) {
			if (typeof(IDEdgeInstance<V>).IsAssignableFrom (obj.GetType ())) {
				IDEdgeInstance<V> o = (IDEdgeInstance<V>)obj;
				if (o.Source.Id.Equals(this.source.Id) && o.Target.Id.Equals(this.target.Id))// && o.Weight == this.Weight)
					return true;
			}
			return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IDEdgeInstance<V> clone = new IDEdgeInstanceImpl<V>((IDVertexInstance)this.Source.Clone(), (IDVertexInstance)this.Target.Clone());
			return clone;
		}
		#endregion
	}

	//************************************** IRootDEdge **************************************
	public class IRootDEdgeImpl<RV>: IRootDEdge<RV> {

		private RV _source = default(RV);
		private RV _target = default(RV);

		public IRootDEdgeImpl(){ }

		public IRootDEdgeImpl(RV source, RV target):this(){ 
			this._source = source;
			this._target = target;
		}
		public virtual float Weight {
			get { return 1.0f; }
		}
		public RV Source {
			get { return _source; }
			set { this._source = value;	}
		}
		public RV Target {
			get { return _target; }
			set { this._target = value;	}
		}

		public virtual IRootDEdge<RV> newInstance (){
			return new IRootDEdgeImpl<RV> ();
		}
		public virtual IRootDEdge<RV> newInstance (RV source, RV target){
			return new IRootDEdgeImpl<RV> (source, target);
		}
		public virtual IRootDEdge<RV> newInstance (RV source, RV target, float weight){
			throw new NotSupportedException();
		}
		public virtual string ToString () {
			return "" + Source + "," + Target + "";
		}
		public override bool Equals (object obj) {
			if (typeof(IRootDEdge<RV>).IsAssignableFrom (obj.GetType ())) {
				IRootDEdge<RV> o = (IRootDEdge<RV>)obj;
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
