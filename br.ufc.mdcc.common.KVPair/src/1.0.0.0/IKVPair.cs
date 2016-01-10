using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using System.Collections.Generic;
using System;

namespace br.ufc.mdcc.common.KVPair { 

	public interface IKVPair<K, V> : IData, BaseIKVPair<K, V>
		where K:IData
		where V:IData
	{
	//	IKVPairInstance<K,V> newInstance(object key, object value);
	} // end main interface 

	public interface IKVPairInstance<K,V> : ICloneable
		where K:IData
		where V:IData
	{
		object Key { get; set;}
		object Value { set; get; }
	}

} // end namespace 
