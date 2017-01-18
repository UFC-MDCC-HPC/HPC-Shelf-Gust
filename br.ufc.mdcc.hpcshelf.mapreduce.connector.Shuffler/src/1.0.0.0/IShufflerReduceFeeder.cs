using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.connector.Shuffler
{
	public interface IShufflerReduceFeeder<M0,TKey, TValue,GIF> : BaseIShufflerReduceFeeder<M0,TKey, TValue,GIF>
		where M0:IMaintainer
		where TKey:IData
		where TValue:IData
		where GIF:IInputFormat
	{
	}
}