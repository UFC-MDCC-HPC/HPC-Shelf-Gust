using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.Data;
using System.Collections.Generic;

namespace br.ufc.mdcc.common.impl.IntegerImpl 
{ 

	public class IIntegerImpl : BaseIIntegerImpl, IInteger
	{

		public IIntegerImpl() { } 

		override public void after_initialize()
		{
			newInstance(); 
		}


		public IIntegerInstance newInstance (int i)
		{
			IIntegerInstance instance = (IIntegerInstance)newInstance ();
			instance.Value = i;
			return instance;
		}

		public object newInstance ()
		{
			this.instance = new IIntegerInstanceImpl ();
			return this.Instance;
		}

		private IIntegerInstance instance;

		public object Instance {
			get { return instance;	}
			set { this.instance = (IIntegerInstance) value; }
		}
	}

	[Serializable]
	public class IIntegerInstanceImpl : IIntegerInstance
	{
		#region IIntegerInstance implementation

		private int val;

		public int Value {
			get { return val; }
			set { this.val = value;	}
		}

		public object ObjValue {
			get { return val; }
			set { this.val = (int) value;}
		}

		public override int GetHashCode ()
		{
			return Value.GetHashCode();	
		}

		public override string ToString ()
		{
			return Value.ToString();
		}

		public override bool Equals (object obj)
		{
			if (obj is IIntegerInstanceImpl)
				return Value==(((IIntegerInstanceImpl) obj).Value);
			else if (obj is int)
				return Value==(int)obj;
			else
				return false;
		}

		#endregion

		#region ICloneable implementation

		public object Clone ()
		{
			IIntegerInstance clone = new IIntegerInstanceImpl();
			clone.Value = this.Value;
			return clone;
		}

		#endregion

	}


}
