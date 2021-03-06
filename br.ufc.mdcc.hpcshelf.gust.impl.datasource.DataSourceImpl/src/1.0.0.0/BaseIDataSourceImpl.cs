/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSourceInterface;
using br.ufc.mdcc.hpcshelf.gust.datasource.DataSource;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.gust.impl.datasource.DataSourceImpl 
{
	public abstract class BaseIDataSourceImpl<P, GIF>: Computation, BaseIDataSource<P, GIF>
		where P:IMaintainer
		where GIF:IInputFormat
	{
		private IServerBase<IPortTypeDataSourceInterface> reader = null;

		protected IServerBase<IPortTypeDataSourceInterface> Reader
		{
			get
			{
				if (this.reader == null)
					this.reader = (IServerBase<IPortTypeDataSourceInterface>) Services.getPort("reader");
				return this.reader;
			}
		}
		private P platform = default(P);

		protected P Platform
		{
			get
			{
				if (this.platform == null)
					this.platform = (P) Services.getPort("platform");
				return this.platform;
			}
		}
		
		private GIF data_format = default(GIF);
		protected GIF Data_format
		{
			get
			{
				if (this.data_format == null)
					this.data_format = (GIF) Services.getPort("data_format");
				return this.data_format;
			}
		}
	}
}