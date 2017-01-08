using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.connector.Splitter
{
	public interface ISplitterMapFeeder<M1,IKey, IValue> : BaseISplitterMapFeeder<M1,IKey, IValue>
	    where M1:IMaintainer
		where IKey:IData
		where IValue:IData
	{
	}
}