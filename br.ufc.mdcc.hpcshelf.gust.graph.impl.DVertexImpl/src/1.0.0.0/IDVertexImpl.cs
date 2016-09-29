using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DVertexImpl {
	public class IDVertexImpl : BaseIDVertexImpl, IDVertex {

		public IDVertexImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}

		public IDVertexInstance newInstance (int i) {
			IDVertexInstance instance = (IDVertexInstance)newInstance ();
			instance.Id = i;
			return instance;
		}

		public object newInstance () {
			this.instance = new IDVertexInstanceImpl ();
			return this.Instance;
		}

		private IDVertexInstance instance;

		public object Instance {
			get { return instance; }
			set { this.instance = (IDVertexInstance)value; }
		}
		public IDVertexInstance VInstance {
			get { return instance; }
		}
	}

	[Serializable]
	public class IDVertexInstanceImpl : IDVertexInstance {

		#region IDVertexInstance implementation
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
			if (obj is IDVertexInstanceImpl)
				return Id == (((IDVertexInstanceImpl)obj).Id);
			else if (obj is int)
				return Id == (int)obj;
			else
				return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IDVertexInstance clone = new IDVertexInstanceImpl ();
			clone.Id = this.Id;
			return clone;
		}
		#endregion
	}
}
