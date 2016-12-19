using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSinkInterface;
using br.ufc.mdcc.hpcshelf.gust.binding.environment.EnvironmentBindingWriteData;
using System.Threading;
using System.Collections.Generic;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.String;

namespace br.ufc.mdcc.hpcshelf.gust.impl.binding.environment.EnvironmentBindingWriteData
{
	public class IWriteDataImpl<S> : BaseIWriteDataImpl<S>, IWriteData<S>
		where S:IPortTypeDataSinkInterface
	{
		public override void main()
		{
		}

		private Thread thread_file_writer = null;

		public override void after_initialize()
		{
			client = Output_pairs_iterator.newIteratorInstance ();
		}

		public void startWriteSink()
		{
			thread_file_writer = new Thread (file_writer);
			thread_file_writer.Start ();
		}

		private IPortTypeIterator client = null;
		public IPortTypeIterator Client { get {	return client; } }

		private S server = default(S);
		public S Server { set {	server = value; } }

		private static int CHUNK_SIZE = 50;

		private string[] file_name_list = null;	

		int counter_write_chunk = 0;
		int counter_write_global = 0;

		private string[] output_buffer = null;

		private void file_writer()
		{
			object pair_obj = null;
			output_buffer = new string[CHUNK_SIZE];
			int pair_counter = 0;

			bool end_iteration = false;
			while (!end_iteration) 
			{
				if (!client.has_next ())
					end_iteration = true;

				while (client.fetch_next (out pair_obj)) 
				{
					IKVPairInstance<IString,IInteger> pair = (IKVPairInstance<IString,IInteger>)pair_obj;
					output_buffer [pair_counter] = pair.Key + ": " + pair.Value;
					pair_counter++;
					if (pair_counter >= CHUNK_SIZE) 
					{
						server.writeLines (output_buffer);
						pair_counter = 0;
					}
				}
			}

			for (int i = pair_counter; i < CHUNK_SIZE; i++)
				output_buffer [i] = "";

			server.writeLines (output_buffer);
		}
	}
}
