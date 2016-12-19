using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.connector.Join
{
	public interface IJoinGustyCollector<M0,IKey,IValue,OKey,OValue,BF,TF> : BaseIJoinGustyCollector<M0,IKey,IValue,OKey,OValue,BF,TF>
		where M0:IMaintainer
		where IKey:IData
		where IValue:IData
		where OKey:IData
		where OValue:IData
		where BF:IPartitionFunction<IKey>
		where TF:ITerminateFunction<IKey,IValue,OKey,OValue>
	{
	}
}