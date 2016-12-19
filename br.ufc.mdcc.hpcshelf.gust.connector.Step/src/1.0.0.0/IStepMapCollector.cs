using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.connector.Step
{
	public interface IStepMapCollector<M1,TKey,TValue,PF> : BaseIStepMapCollector<M1,TKey,TValue,PF>
		where M1:IMaintainer
		where PF:IPartitionFunction<TKey>
		where TKey:IData
		where TValue:IData
	{
	}
}