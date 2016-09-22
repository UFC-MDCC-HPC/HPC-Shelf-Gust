using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.KVPair;
using System.Collections.Generic;

namespace br.ufc.mdcc.common.impl.KVPairImpl { 

	public class IKVPairImpl<K, V> : BaseIKVPairImpl<K, V>, IKVPair<K, V>
	where K:IData
	where V:IData
	{
		override public void after_initialize()
		{
			newInstance(); 
		}

	//	public IKVPairInstance<K,V> newInstance (object k, object v)
	//	{
	//		IKVPairInstance<K,V> instance = new IKVPairInstanceImpl<K,V> (k,v);
	//		return ( IKVPairInstance<K,V>) (this.Instance = instance);
	//	}

		public object newInstance ()
		{
			object k = Key_type.newInstance();
			object v = Value_type.newInstance();
			return this.Instance = new IKVPairInstanceImpl<K,V> (k,v);
		}

		private IKVPairInstance<K,V> instance;

		public object Instance {
			get { return instance;	}
			set { this.instance = (IKVPairInstance<K,V>) value;	}
		}
	}

	[Serializable]
	public class IKVPairInstanceImpl<K,V> : IKVPairInstance<K,V>
		where K:IData
		where V:IData
	{
		public IKVPairInstanceImpl(object k, object v)
		{
			this.k = k;
			this.v = v;
		}

		#region IKVPairInstance implementation

		private object k;
		private object v;

		public object Key {
			get { return k; }
			set { this.k = value; }
		}

		public object Value {
			get { return v; }
			set { this.v = value; }
		}

		public object ObjValue {
			get { return new Tuple<object,object>(k,v); }
			set 
			{ 
				this.k = ((Tuple<object,object>)value).Item1;
				this.v = ((Tuple<object,object>)value).Item2;
			}
		}

		#endregion

		#region ICloneable implementation

		public object Clone ()
		{
			IKVPairInstance<K,V> clone = new IKVPairInstanceImpl<K,V>(((ICloneable)this.Key).Clone(), ((ICloneable)this.Value).Clone());

			return clone;
		}

		#endregion

	}

}
