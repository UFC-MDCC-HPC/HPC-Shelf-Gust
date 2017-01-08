using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.String;
using System.Collections.Generic;

namespace br.ufc.mdcc.common.impl.StringImpl { 

	public class IStringImpl : BaseIStringImpl, IString
	{

		public IStringImpl() { 	} 

		override public void after_initialize()
		{
			newInstance(); 
		}

		public IStringInstance newInstance (string s)
		{
			IStringInstance instance = (IStringInstance)newInstance ();
			instance.Value = s;
			return instance;
		}

		public object newInstance ()
		{
			this.instance = new IStringInstanceImpl ();
			return this.Instance;
		}

		private IStringInstance instance;

		public object Instance {
			get { return instance;	}
			set { this.instance = (IStringInstance) value; }
		}
	}

	[Serializable]
	public class IStringInstanceImpl : IStringInstance
	{
		#region IStringInstance implementation

		private string val;

		public string Value {
			get { return val; }
			set { this.val = value;	}
		}

		public object ObjValue {
			get { return val; }
			set { this.val = (string) value;}
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
			if (obj is IStringInstanceImpl)
				return Value.Equals(((IStringInstanceImpl) obj).Value);
			else if (obj is string)
				return Value.Equals(obj);
			else
				return false;
		}

		#endregion

		#region ICloneable implementation

		public object Clone ()
		{
			IStringInstance clone = new IStringInstanceImpl();
			clone.Value = this.Value;
			return clone;
		}

		#endregion

	}

}
