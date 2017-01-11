using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.OutputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.datasource.DataSink
{
	public interface IDataSink<P, GOF, OKey, OValue> : BaseIDataSink<P, GOF, OKey, OValue>
		where P:IMaintainer
		where GOF:IOutputFormat<OKey, OValue>
		where OKey:IData
		where OValue:IData
	{
	}
}