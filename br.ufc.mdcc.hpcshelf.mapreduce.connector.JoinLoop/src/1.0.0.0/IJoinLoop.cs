using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.TerminateFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.connector.JoinLoop
{
	public interface IJoinLoop<M1, TF, IKey, IValue, OKey, OValue, BF, GIF> : BaseIJoinLoop<M1, TF, IKey, IValue, OKey, OValue, BF, GIF>
		where M1:IMaintainer
		where TF:ITerminateFunction<IKey, IValue, OKey, OValue>
		where IKey:IData
		where IValue:IData
		where OKey:IData
		where OValue:IData
		where BF:IPartitionFunction<IKey>
		where GIF:IInputFormat
	{
	}
}