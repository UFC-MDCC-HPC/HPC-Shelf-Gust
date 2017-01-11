/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.OutputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.datasource.DataSink
{
	public interface BaseIDataSink<P, GOF, OKey, OValue> : IComputationKind 
		where P:IMaintainer
		where GOF:IOutputFormat<OKey, OValue>
		where OKey:IData
		where OValue:IData
	{
	}
}