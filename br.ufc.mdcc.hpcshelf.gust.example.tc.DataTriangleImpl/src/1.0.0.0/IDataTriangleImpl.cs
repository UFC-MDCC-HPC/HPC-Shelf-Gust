using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.example.tc.DataTriangle;

namespace br.ufc.mdcc.hpcshelf.gust.example.tc.DataTriangleImpl {
	public class IDataTriangleImpl : BaseIDataTriangleImpl, IDataTriangle {
		public IDataTriangleImpl () {}

		override public void after_initialize () {
			newInstance (); 
		}

		public IDataTriangleInstance newInstance (int v) {
			IDataTriangleInstance instance = (IDataTriangleInstance)newInstance ();
			instance.V = v;
			return instance;
		}
		public IDataTriangleInstance newInstance (int v, int w) {
			IDataTriangleInstance instance = (IDataTriangleInstance)newInstance ();
			instance.V = v;
			instance.W = w;
			return instance;
		}
		public IDataTriangleInstance newInstance (int v, int w, int z) {
			IDataTriangleInstance instance = (IDataTriangleInstance)newInstance ();
			instance.V = v;
			instance.W = w;
			instance.Z = z;
			return instance;
		}

		public object newInstance () {
			this.instance = new IDataTriangleInstanceImpl ();
			return this.Instance;
		}

		private IDataTriangleInstance instance;

		public object Instance {
			get { return instance; }
			set { this.instance = (IDataTriangleInstance)value; }
		}
	}

	[Serializable]
	public class IDataTriangleInstanceImpl : IDataTriangleInstance {
		#region IDataTriangleInstance implementation

		private int v;
		public int V {
			get { return this.v; }
			set { this.v = value; }
		}
		private int w;
		public int W {
			get { return this.w; }
			set { this.w = value; }
		}
		private int z;
		public int Z {
			get { return this.z; }
			set { this.z = value; }
		}

		public object ObjValue {
			get { return new Tuple<int,int,int>(v,w,z); }
			set { 
				this.v = ((Tuple<int,int,int>)value).Item1;
				this.w = ((Tuple<int,int,int>)value).Item2;
				this.z = ((Tuple<int,int,int>)value).Item3;
			}
		}
		public override int GetHashCode () { return pairingFunction (this.v, pairingFunction (this.w, this.z) ); }
		public override string ToString () { return "["+this.v+","+this.w+","+this.z+"]"; }

		public override bool Equals (object obj) {
			if (obj is IDataTriangleInstanceImpl)
				return ((IDataTriangleInstanceImpl)obj).V==this.v && 
					((IDataTriangleInstanceImpl)obj).W == this.w && 
					((IDataTriangleInstanceImpl)obj).Z == this.z;
			else 
				return false;
		}
		public static int pairingFunction (int a, int b) {
			var A = (ulong)(a >= 0 ? 2 * (long)a : -2 * (long)a - 1);
			var B = (ulong)(b >= 0 ? 2 * (long)b : -2 * (long)b - 1);
			var C = (long)((A >= B ? A * A + A + B : A + B * B) / 2);
			var R = a < 0 && b < 0 || a >= 0 && b >= 0 ? C : -C - 1;
			return (int)R;
		}

		#endregion

		#region ICloneable implementation

		public object Clone () {
			IDataTriangleInstance clone = new IDataTriangleInstanceImpl ();
			clone.V = this.v;
			clone.W = this.w;
			clone.Z = this.z;
			return clone;
		}

		#endregion

	}


}
