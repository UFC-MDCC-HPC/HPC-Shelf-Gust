using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.VertexImpl 
{
	public class IVertexImpl : BaseIVertexImpl, IVertex 
	{

		public IVertexImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}

		public IVertexInstance newInstance (int i) {
			IVertexInstance instance = (IVertexInstance)newInstance ();
			instance.Id = i;
			return instance;
		}

		public object newInstance () {
			this.instance = new IVertexInstanceImpl ();
			return this.Instance;
		}

		private IVertexInstance instance;

		public object Instance {
			get { return instance; }
			set { this.instance = (IVertexInstance)value; }
		}
		public IVertexInstance VInstance {
			get { return instance; }
		}
	}

	[Serializable]
	public class IVertexInstanceImpl : IVertexInstance {

		#region IVertexInstance implementation
		private int val;

		public int Id {
			get { return val; }
			set { this.val = value; }
		}

		public object ObjValue {
			get { return val; }
			set { this.val = (int)value; }
		}

		public override string ToString () {
			return Id.ToString ();
		}

		public override int GetHashCode () {
			return this.Id;
		}
		public override bool Equals (object obj) {
			if (typeof(IVertexInstance).IsAssignableFrom (obj.GetType ()))
				return this.Id == ((IVertexInstance)obj).Id;
			return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IVertexInstance clone = new IVertexInstanceImpl ();
			clone.Id = this.Id;
			return clone;
		}
		#endregion
	}
}
