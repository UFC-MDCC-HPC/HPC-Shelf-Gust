using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Float;
using br.ufc.mdcc.common.Data;
using System.Collections.Generic;

namespace br.ufc.mdcc.common.impl.FloatImpl {

	public class IFloatImpl : BaseIFloatImpl, IFloat {

		public IFloatImpl () {
		}

		override public void after_initialize () {
			newInstance (); 
		}


		public IFloatInstance newInstance (float i) {
			IFloatInstance instance = (IFloatInstance)newInstance ();
			instance.Value = i;
			return instance;
		}

		public object newInstance () {
			this.instance = new IFloatInstanceImpl ();
			return this.Instance;
		}

		private IFloatInstance instance;

		public object Instance {
			get { return instance; }
			set { this.instance = (IFloatInstance)value; }
		}
	}

	[Serializable]
	public class IFloatInstanceImpl : IFloatInstance {
		#region IFloatInstance implementation

		private float val;

		public float Value {
			get { return val; }
			set { this.val = value; }
		}

		public object ObjValue {
			get { return val; }
			set { this.val = (float)value; }
		}

		public override int GetHashCode () {
			return Value.GetHashCode ();	
		}

		public override string ToString () {
			return Value.ToString ();
		}

		public override bool Equals (object obj) {
			if (obj is IFloatInstanceImpl)
				return Value == (((IFloatInstanceImpl)obj).Value);
			else if (obj is float)
					return Value == (float)obj;
				else
					return false;
		}

		#endregion

		#region ICloneable implementation

		public object Clone () {
			IFloatInstance clone = new IFloatInstanceImpl ();
			clone.Value = this.Value;
			return clone;
		}

		#endregion

	}


}
