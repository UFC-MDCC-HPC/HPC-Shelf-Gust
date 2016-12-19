using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.datasource.DataSink;
using System.Collections.Generic;
using System.IO;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSinkInterface;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
          
namespace br.ufc.mdcc.hpcshelf.gust.impl.datasource.DataSinkImpl
{
	public class IDataSinkImpl<P> : BaseIDataSinkImpl<P>, IDataSink<P>
    where P:IMaintainer
	{
		public override void main()
		{
			Writer.Server = new DataSinkWriter();
		}

		private static string PATH_TEXT_FILES_WORD_COUNTER_OUTPUT = "PATH_TEXT_FILES_WORD_COUNTER_OUTPUT";

		private class DataSinkWriter : IPortTypeDataSinkInterface
		{
			public void resetOutput() 
			{
				string path = System.Environment.GetEnvironmentVariable (PATH_TEXT_FILES_WORD_COUNTER_OUTPUT);
				File.Delete (path);
			}

			public void writeLines(IEnumerable<string> pairs)
			{
				string path = System.Environment.GetEnvironmentVariable (PATH_TEXT_FILES_WORD_COUNTER_OUTPUT);
				File.AppendAllLines(path, pairs);
			}

			public IEnumerable<string> readOutput()
			{
				string path = System.Environment.GetEnvironmentVariable (PATH_TEXT_FILES_WORD_COUNTER_OUTPUT);
				return File.ReadAllLines (path);
			}

		}
	}
}
