/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.KVPair;

namespace br.ufc.mdcc.common.impl.KVPairImpl { 

	public abstract class BaseIKVPairImpl<K, V>: DataStructure, BaseIKVPair<K, V>
	where K:IData
	where V:IData
	{

		private V value_type = default(V);

		public V Value_type {
			get {
				if (this.value_type == null)
					this.value_type = (V) Services.getPort("value_type");
				return this.value_type;
			}
		}

		private K key_type = default(K);

		public K Key_type {
			get {
				if (this.key_type == null)
					this.key_type = (K) Services.getPort("key_type");
				return this.key_type;
			}
		}


	}

}
