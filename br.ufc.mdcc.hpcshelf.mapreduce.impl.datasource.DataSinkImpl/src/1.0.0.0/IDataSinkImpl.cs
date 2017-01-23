using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.mapreduce.datasource.DataSink;
using System.Collections.Generic;
using System.IO;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSinkInterface;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.OutputFormat;
          
namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.datasource.DataSinkImpl
{
	public class IDataSinkImpl<P, GOF, OKey, OValue> : BaseIDataSinkImpl<P, GOF, OKey, OValue>, IDataSink<P, GOF, OKey, OValue>
		where P:IMaintainer
		where GOF:IOutputFormat<OKey, OValue>
		where OKey:IData
		where OValue:IData
	{
		public override void main()
		{
			IOutputFormatInstance<OKey, OValue> o = (IOutputFormatInstance<OKey, OValue>)Output_format.Instance;
			Writer.Server = new DataSinkWriter<OKey, OValue>(o);
		}

		private static string PATH_GRAPH_FILE_RESULT = "PATH_GRAPH_FILE_RESULT"; //("PATH_GRAPH_FILE_RESULT");

		private class DataSinkWriter<OKey, OValue> : IPortTypeDataSinkInterface
			where OKey:IData
			where OValue:IData
		{
			public DataSinkWriter(object o){
				this.output_format = (IOutputFormatInstance<OKey, OValue>)o;
			}
			public void resetOutput() 
			{
				string path = System.Environment.GetEnvironmentVariable (PATH_GRAPH_FILE_RESULT);
				File.Delete (path);
			}

			public void writeLines(IEnumerable<string> pairs)
			{
				string path = System.Environment.GetEnvironmentVariable (PATH_GRAPH_FILE_RESULT);
				File.AppendAllLines(path, pairs);
			}

			public IEnumerable<string> readOutput()
			{
				string path = System.Environment.GetEnvironmentVariable (PATH_GRAPH_FILE_RESULT);
				return File.ReadAllLines (path);
			}

			private IOutputFormatInstance<OKey, OValue> output_format = null;
			public object IteratorProvider{ get { return this.output_format.Iterator; } }

			public string formatRepresentation(object kv_pair){ return this.output_format.formatRepresentation (kv_pair); }

		}
	}
}
