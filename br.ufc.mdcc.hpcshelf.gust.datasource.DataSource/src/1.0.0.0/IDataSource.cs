using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;


namespace br.ufc.mdcc.hpcshelf.gust.datasource.DataSource
{
	public interface IDataSource<P> : BaseIDataSource<P>
		where P:IMaintainer
	{
	}
}