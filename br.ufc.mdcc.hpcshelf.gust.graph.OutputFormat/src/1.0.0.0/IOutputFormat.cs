using System;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;

namespace br.ufc.mdcc.hpcshelf.gust.graph.OutputFormat
{
	public interface IOutputFormat<OKey,OValue> : BaseIOutputFormat<OKey,OValue>, IData
		where OKey:IData
		where OValue:IData
	{
		//IOutputFormatInstance<OKey,OValue> newInstance(object iterator);
	}

	public interface IOutputFormatInstance<OKey,OValue> : IDataInstance, ICloneable
		where OKey:IData
		where OValue:IData
	{
        object Iterator{get;}
        string formatRepresentation(object kv_pair);
	}

}