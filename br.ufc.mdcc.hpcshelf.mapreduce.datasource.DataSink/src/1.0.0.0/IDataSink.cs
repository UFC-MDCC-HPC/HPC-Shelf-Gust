using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.datasource.DataSink
{
	public interface IDataSink<P> : BaseIDataSink<P>
		where P:IMaintainer
	{
	}
}