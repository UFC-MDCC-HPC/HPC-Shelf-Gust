using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.connector.Join
{
	public interface IJoinWriteSink<M3,OKey,OValue> : BaseIJoinWriteSink<M3,OKey,OValue>
		where M3:IMaintainer
		where OKey:IData
		where OValue:IData	
	{
	}
}