using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.datasource.DataSource;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSourceInterface;
using System.Collections.Generic;
using System.IO;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.impl.datasource.DataSourceImpl {
	public class IDataSourceImpl<P> : BaseIDataSourceImpl<P>, IDataSource<P>
    where P:IMaintainer {
		public override void main() {
			IInputFormatInstance shardInstance = Data_format.newInstanceIF ();
			Reader.Server = new DataSourceReader(shardInstance);
		}
		private class DataSourceReader : IPortTypeDataSourceInterface {
			
			private IInputFormatInstance shardInstance;

			public object fetchFileContentObject() {
				return shardInstance;
				//string fileName = System.Environment.GetEnvironmentVariable (PATH_GRAPH_FILE);
				//return File.ReadLines (fileName);
			}
			public DataSourceReader(IInputFormatInstance _shardInstance){
				shardInstance = _shardInstance;
			}

		}
	}
}
