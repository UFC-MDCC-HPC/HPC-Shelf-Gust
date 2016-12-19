using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.datasource.DataSource;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSourceInterface;
using System.Collections.Generic;
using System.IO;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.impl.datasource.DataSourceImpl
{
	public class IDataSourceImpl<P> : BaseIDataSourceImpl<P>, IDataSource<P>
    where P:IMaintainer
	{
		public override void main()
		{
			Reader.Server = new DataSourceReader();
		}

		private static string PATH_TEXT_FILES_WORD_COUNTER_INPUT = "PATH_TEXT_FILES_WORD_COUNTER_INPUT";

		private class DataSourceReader : IPortTypeDataSourceInterface
		{
			public string[] fetchFileNames ()
			{
				string path = System.Environment.GetEnvironmentVariable (PATH_TEXT_FILES_WORD_COUNTER_INPUT);
				string[] filenames = Directory.GetFiles (path);

				Console.WriteLine ("FETCH FILE NAMES: ");
				foreach (string fn in filenames)
					Console.WriteLine (fn);

				return filenames;	
			}

			public IEnumerable<string> fetchFileContents(string fileName)
			{
				string path = System.Environment.GetEnvironmentVariable (PATH_TEXT_FILES_WORD_COUNTER_INPUT);
				return File.ReadLines (fileName);
			}

		}
	}
}
