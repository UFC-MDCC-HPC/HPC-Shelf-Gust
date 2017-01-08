using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.connector.Splitter
{
	public interface ISplitterWriteSink<M3,OKey,OValue> : BaseISplitterWriteSink<M3,OKey,OValue>
		where M3:IMaintainer
		where OKey:IData
		where OValue:IData	
	{
	}
}