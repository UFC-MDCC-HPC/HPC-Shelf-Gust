using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.datasource.DataSink;
using System.Collections.Generic;
using System.IO;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSinkInterface;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.graph.OutputFormat;
          
namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.datasource.DataSinkImpl
{
	public class IDataSinkImpl<P, GOF> : BaseIDataSinkImpl<P, GOF>, IDataSink<P, GOF>
    where P:IMaintainer
	where GOF:IOutputFormat
	{
		public override void main()
		{
			IOutputFormatInstance o = Output_format.newInstanceT ();
			Writer.Server = new DataSinkWriter(o);
		}

		private static string PATH_GRAPH_FILE_RESULT = "PATH_GRAPH_FILE_RESULT"; //("PATH_GRAPH_FILE_RESULT");

		private class DataSinkWriter : IPortTypeDataSinkInterface
		{
			public DataSinkWriter(object o){
				this.output_format = o;
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
			private object output_format = null;
			public object representationObject(){
				return this.output_format;
			}
		}
	}
}
