using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.datasource.DataSource;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSourceInterface;
using System.Collections.Generic;
using System.IO;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.datasource.DataSourceImpl
{
	public class IDataSourceImpl<P> : BaseIDataSourceImpl<P>, IDataSource<P>
    where P:IMaintainer
	{
		public override void main()
		{
			//Reader.Server = new DataSourceReader();
			IInputFormatInstance input_format_instance = Data_format.newInstanceIF ();
			Reader.Server = new DataSourceReader(input_format_instance);
		}
		private class DataSourceReader : IPortTypeDataSourceInterface {
			
			private IInputFormatInstance input_format_instance;

			public object fetchFileContentObject() {
				return input_format_instance;
				//string fileName = System.Environment.GetEnvironmentVariable (PATH_GRAPH_FILE);
				//return File.ReadLines (fileName);
			}
			public DataSourceReader(IInputFormatInstance o){
				input_format_instance = o;
			}
		}
	}
}
