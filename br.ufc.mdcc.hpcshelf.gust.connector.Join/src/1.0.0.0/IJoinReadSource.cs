using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.connector.Join
{
	public interface IJoinReadSource<M2,IKey, IValue, BF> : BaseIJoinReadSource<M2,IKey, IValue, BF>
		where M2:IMaintainer
		where IKey:IData
		where IValue:IData
		where BF:IPartitionFunction<IKey>
	{
	}
}