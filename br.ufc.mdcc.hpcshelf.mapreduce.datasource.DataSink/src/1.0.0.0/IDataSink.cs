using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.OutputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.datasource.DataSink
{
	public interface IDataSink<P, GOF> : BaseIDataSink<P, GOF>
		where P:IMaintainer
		where GOF:IOutputFormat
	{
	}
}