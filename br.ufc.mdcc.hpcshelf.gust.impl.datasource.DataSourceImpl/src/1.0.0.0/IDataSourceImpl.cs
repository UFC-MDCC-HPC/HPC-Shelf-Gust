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
			IInputFormatInstance shardInstance = ShardBins.newInstanceIF ();
			Reader.Server = new DataSourceReader(shardInstance);
		}
		//Variável de ambiente PATH_GRAPH_FILE já está dentro de InputFormat
		//private static string PATH_GRAPH_FILE = "PATH_GRAPH_FILE";

		private class DataSourceReader : IPortTypeDataSourceInterface {
			
			private IInputFormatInstance shardInstance;

			public IEnumerable<ShardInputFormat> fetchFileContent() {
				IDictionary<int, IInputFormatInstance> data_source = shardInstance.extractBins ();
				foreach(KeyValuePair<int, IInputFormatInstance> kv in data_source){
					int simbolicVertex = kv.Value.firstVertex (kv.Key); //OBS: kv.Key="partitionID" e firstVertex(kv.Key)="primeiro vertice da tabela de particionamento vinculado ao partitionID"
					ShardInputFormat part = new ShardInputFormat (simbolicVertex, kv.Value);
					yield return part;
				}
				//string fileName = System.Environment.GetEnvironmentVariable (PATH_GRAPH_FILE);
				//return File.ReadLines (fileName);
			}
			public DataSourceReader(IInputFormatInstance _shardInstance){
				shardInstance = _shardInstance;
			}

		}
	}
}
