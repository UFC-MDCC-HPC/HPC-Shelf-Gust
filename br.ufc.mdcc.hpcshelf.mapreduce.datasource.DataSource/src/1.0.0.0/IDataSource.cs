using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
//using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.datasource.DataSource
{
	public interface IDataSource<P, GIF> : BaseIDataSource<P, GIF>
		where P:IMaintainer
		where GIF:IData //InputFormat
	{
	}
}