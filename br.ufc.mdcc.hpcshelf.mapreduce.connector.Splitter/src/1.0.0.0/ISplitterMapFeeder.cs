using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.connector.Splitter
{
	public interface ISplitterMapFeeder<M1,IKey, IValue, GIF> : BaseISplitterMapFeeder<M1,IKey, IValue, GIF>
	    where M1:IMaintainer
		where IKey:IData
		where IValue:IData
		where GIF:IInputFormat
	{
	}
}