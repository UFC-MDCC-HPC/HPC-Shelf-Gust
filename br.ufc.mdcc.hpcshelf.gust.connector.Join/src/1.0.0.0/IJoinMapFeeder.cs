using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.connector.Join
{
	public interface IJoinMapFeeder<M1,IKey, IValue> : BaseIJoinMapFeeder<M1,IKey, IValue>
	    where M1:IMaintainer
		where IKey:IData
		where IValue:IData
	{
	}
}