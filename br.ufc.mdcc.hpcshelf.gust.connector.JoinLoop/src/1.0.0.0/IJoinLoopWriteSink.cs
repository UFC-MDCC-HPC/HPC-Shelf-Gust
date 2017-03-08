using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.connector.JoinLoop
{
	public interface IJoinLoopWriteSink<M3,OKey,OValue> : BaseIJoinLoopWriteSink<M3,OKey,OValue>
		where M3:IMaintainer
		where OKey:IData
		where OValue:IData	
	{
	}
}