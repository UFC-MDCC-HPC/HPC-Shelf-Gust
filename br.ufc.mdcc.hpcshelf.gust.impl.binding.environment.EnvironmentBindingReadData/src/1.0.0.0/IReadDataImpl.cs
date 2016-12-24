using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSourceInterface;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.String;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.KVPair;
using System.Threading;
using System.Collections.Generic;
using br.ufc.mdcc.hpcshelf.gust.binding.environment.EnvironmentBindingReadData;

namespace br.ufc.mdcc.hpcshelf.gust.impl.binding.environment.EnvironmentBindingReadData {
	public class IReadDataImpl<S> : BaseIReadDataImpl<S>, IReadData<S>
		where S:IPortTypeDataSourceInterface {
		public override void main () {
		}

		private Thread thread_file_reader = null;

		public override void after_initialize () {
			client = Input_pairs_iterator.newIteratorInstance ();
		}

		public void startReadSource () {
			thread_file_reader = new Thread (file_reader);
			thread_file_reader.Start ();
		}

		private IPortTypeIterator client = null;

		public IPortTypeIterator Client { get { return client; } }

		private S server = default(S);

		public S Server { set { server = value; } }

		private static int CHUNK_SIZE = 50;

		int counter_write_chunk = 0;
		int counter_write_global = 0;

		private void file_reader () {
			IEnumerable<string> file_line_list = server.fetchFileContent ();	
			foreach (string line in file_line_list) {
				IKVPairInstance<IInteger,IString> item = (IKVPairInstance<IInteger,IString>)client.createItem ();
				((IIntegerInstance)item.Key).Value = counter_write_global;
				((IStringInstance)item.Value).Value = line;
				client.put (item);

				counter_write_chunk++;
				counter_write_global++;

				if (counter_write_chunk >= CHUNK_SIZE) {
					//Console.WriteLine ("NEW CHUNK size=" + counter_write_chunk);
					counter_write_chunk = 0;
					client.finish ();
				}
			}

			client.finish ();

			client.finish ();

			Console.WriteLine ("FINISHING READING DATA SOURCE");
		}
	}
}
