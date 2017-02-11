using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.connector.JoinLoop
{
	//public interface IJoinLoopReadSource<M2, BF, IKey, IValue, GIF> : BaseIJoinLoopReadSource<M2, BF, IKey, IValue, GIF>
	public interface IJoinLoopReadSource<M2, GIF> : BaseIJoinLoopReadSource<M2, GIF>
		where M2:IMaintainer
		//where BF:IPartitionFunction<GIF>
		//where IKey:IData
		//where IValue:IData
		where GIF:IInputFormat
	{
	}
}