using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DVertexBasicImpl {
	public class IDVertexBasicImpl : BaseIDVertexBasicImpl, IDVertexBasic {

		public IDVertexBasicImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}

		public IDVertexInstance newInstance (int i) {
			IDVertexBasicInstance instance = (IDVertexBasicInstance)newInstance ();
			instance.Id = i;
			return instance;
		}

		public object newInstance () {
			this.instance = new IDVertexBasicInstanceImpl ();
			return this.Instance;
		}

		private IDVertexBasicInstance instance;

		public object Instance {
			get { return instance; }
			set { this.instance = (IDVertexBasicInstance)value; }
		}
		public IDVertexInstance VInstance {
			get { return instance; }
		}
	}

	[Serializable]
	public class IDVertexBasicInstanceImpl : IDVertexBasicInstance {

		#region IDVertexBasicInstance implementation
		private int val;

		public int Id {
			get { return val; }
			set { this.val = value; }
		}

		public object ObjValue {
			get { return val; }
			set { this.val = (int)value; }
		}

		public override int GetHashCode () {
			return Id.GetHashCode ();	
		}

		public override string ToString () {
			return Id.ToString ();
		}

		public override bool Equals (object obj) {
			if (obj is IDVertexBasicInstanceImpl)
				return Id == (((IDVertexBasicInstanceImpl)obj).Id);
			else if (obj is int)
					return Id == (int)obj;
				else
					return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IDVertexBasicInstance clone = new IDVertexBasicInstanceImpl ();
			clone.Id = this.Id;
			return clone;
		}
		#endregion
	}
}
