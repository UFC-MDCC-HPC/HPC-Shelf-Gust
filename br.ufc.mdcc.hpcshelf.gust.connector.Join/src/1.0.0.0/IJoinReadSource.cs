using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.gust.connector.Join
{
	//public interface IJoinReadSource<M2, BF, IKey, IValue, GIF> : BaseIJoinReadSource<M2, BF, IKey, IValue, GIF>
	public interface IJoinReadSource<M2, GIF> : BaseIJoinReadSource<M2, GIF>
		where M2:IMaintainer
		//where BF:IPartitionFunction<GIF>
		//where IKey:IData
		//where IValue:IData
		where GIF:IInputFormat
	{
	}
}