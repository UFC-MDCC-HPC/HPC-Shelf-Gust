/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
//using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
//using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSourceInterface;
//using br.ufc.mdcc.hpcshelf.platform.Platform;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
//using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.datasource.DataSource
{
	public interface BaseIDataSource<P, GIF> : IComputationKind 
		where P:IMaintainer
		where GIF:IData //InputFormat
	{
		//IServerBase<IPortTypeDataSourceInterface> Reader {get;}
		//IProcessingNode<M> Platform_data_source {get;}
	}
}