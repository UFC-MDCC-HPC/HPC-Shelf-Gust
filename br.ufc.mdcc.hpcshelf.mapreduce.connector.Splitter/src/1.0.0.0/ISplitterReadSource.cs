using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.connector.Splitter
{
	public interface ISplitterReadSource<M2, BF, IKey, IValue, GIF> : BaseISplitterReadSource<M2, BF, IKey, IValue, GIF>
		where M2:IMaintainer
		where BF:IPartitionFunction<IKey>
		where IKey:IData
		where IValue:IData
		where GIF:IInputFormat
	{
	}
}