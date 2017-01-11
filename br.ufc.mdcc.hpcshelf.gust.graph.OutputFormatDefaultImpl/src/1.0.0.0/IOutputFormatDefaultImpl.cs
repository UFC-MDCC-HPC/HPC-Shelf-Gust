using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.hpcshelf.gust.graph.OutputFormat;

namespace br.ufc.mdcc.hpcshelf.gust.graph.OutputFormatDefaultImpl
{
	public class IOutputFormatDefaultImpl<OKey, OValue> : BaseIOutputFormatDefaultImpl<OKey, OValue>, IOutputFormat<OKey, OValue>
where OKey:IData
where OValue:IData
	{
		override public void after_initialize()
		{
			newInstance(); 
		}
		public object newInstance ()
		{
			IIteratorInstance<IKVPair<OKey,OValue>> output_instance = (IIteratorInstance<IKVPair<OKey,OValue>>)this.Output_pairs_iterator.Instance;
			return this.Instance = new IOutputFormatInstanceImpl<OKey,OValue> (output_instance);
		}
		private IOutputFormatInstance<OKey,OValue> instance;

		public object Instance {
			get { return instance;	}
			set { this.instance = (IOutputFormatInstance<OKey,OValue>) value;	}
		}
	}

	[Serializable]
	public class IOutputFormatInstanceImpl<OKey,OValue> : IOutputFormatInstance<OKey,OValue>
		where OKey:IData
		where OValue:IData
	{
		public IOutputFormatInstanceImpl(object _iterator)
		{
			this.iterator = (IIteratorInstance<IKVPair<OKey,OValue>>) _iterator;
		}

		#region IOutputFormatInstance implementation

		private object iterator;

		public object Iterator {
			get { return this.iterator; }
		}

		public object ObjValue {
			get { return new Tuple<object>(this.iterator); }
			set 
			{ 
				this.iterator = ((Tuple<object,object>)value).Item1;
			}
		}

		public string formatRepresentation(object kv_pair){
			IKVPairInstance<OKey,OValue> kv = (IKVPairInstance<OKey,OValue>)kv_pair;
			return kv.Key.ToString() + ":" + kv.Value.ToString();
		}

		#endregion

		#region ICloneable implementation

		public object Clone ()
		{
			IOutputFormatInstance<OKey,OValue> clone = new IOutputFormatInstanceImpl<OKey,OValue>(((ICloneable)this.iterator).Clone());

			return clone;
		}

		#endregion

	}

}
