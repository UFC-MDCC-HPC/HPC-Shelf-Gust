using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.mapreduce.connector.Splitter;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Iterator;
using System.Collections.Generic;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using System.Threading;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeData;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.connector.SplitterImpl
{
	public class ISplitterReadSourceImpl<M2,IKey, IValue, BF> : BaseISplitterReadSourceImpl<M2,IKey, IValue, BF>, ISplitterReadSource<M2,IKey, IValue, BF>
		where M2:IMaintainer
		where IKey:IData
		where IValue:IData
		where BF:IPartitionFunction<IKey>
	{
		static private int TAG_SPLIT_NEW_CHUNK = 246;
		static private int TAG_SPLIT_END_CHUNK = 247;

		public override void main()
		{
			Console.WriteLine (this.Rank + ": SPLITTER 1 ");

			IPortTypeIterator input_instance = (IPortTypeIterator) Source.Client;

			// TODO: será que READ_SOURCE é necessária ???
			Task_binding_data.TraceFlag = true;
			Task_binding_data.invoke (ITaskPortData.READ_SOURCE);

			// CALCULATE SubGraph TARGET
			int r_size = 0;
			foreach (int i in this.FacetIndexes[FACET_REDUCE]) {   
				r_size += this.UnitSizeInFacet [i] ["reduce_collector"];
			}
			Console.WriteLine (this.Rank + ": SPLITTER 2 ");

			Source.startReadSource (r_size);

			Console.WriteLine (this.Rank + ": SPLITTER 3 ");

			object bin_object = null;
			bool set_table_partition = true;

			// CALCULATE TARGETs
			IDictionary<int,Tuple<int,int>> unit_ref = new Dictionary<int, Tuple<int,int>> ();
			int nf = this.FacetMultiplicity [FACET_MAP];
			int m_size = 0;
			foreach (int i in this.FacetIndexes[FACET_MAP]) {   
				int nr0 = m_size;
				m_size += this.UnitSizeInFacet [i] ["map_feeder"];
				for (int k = 0, j = nr0; j < m_size; k++, j++)
					unit_ref [j] = new Tuple<int,int> (i/*, 0 INDEX OF map_feeder */,k);
			}

			Console.WriteLine (this.Rank + ": SPLITTER 4 ");

			Task_binding_split_first.TraceFlag = true;
			Split_channel.TraceFlag = true;

			Thread t_output = new Thread (new ThreadStart (delegate 
				{
					Task_binding_data.invoke (ITaskPortData.TERMINATE);
					Task_binding_data.invoke (ITaskPortData.WRITE_SINK);
				}));

			t_output.Start ();

			Console.WriteLine (this.Rank + ": SPLITTER 5 ");

			bool end_iteration = false;
			while (!end_iteration) // take next chunk
			{
				IActionFuture sync_perform;

				Console.WriteLine (this.Rank + ": SPLITTER READ SOURCE - BEFORE READ_CHUNK " + m_size);

				/* All the pairs will be read from the source in a single chunk */

				Task_binding_split_first.invoke (ITaskPortAdvance.READ_CHUNK); //****
				Task_binding_split_first.invoke (ITaskPortAdvance.PERFORM, out sync_perform);

				Bin_function.NumberOfPartitions = m_size;

				IList<IKVPairInstance<IKey,IValue>>[] buffer = new IList<IKVPairInstance<IKey,IValue>>[m_size];
				for (int i = 0; i < m_size; i++)
					buffer [i] = new List<IKVPairInstance<IKey,IValue>> ();

				Console.WriteLine (this.Rank + ": BEGIN READING CHUNKS and distributing to MAPPERS m_size=" + m_size);

				if (!input_instance.has_next()) 
					end_iteration = true;

				Bin_function.PartitionTABLE = Source.PartitionTABLE;

				while (input_instance.fetch_next (out bin_object)) 
				{
					IKVPairInstance<IKey,IValue> item = (IKVPairInstance<IKey,IValue>)bin_object;

					this.Input_key.Instance = item.Key;
					Bin_function.go ();
					int index = ((IIntegerInstance)this.Output_key.Instance).Value;

					buffer[index].Add(item);
				}

				Console.WriteLine (this.Rank + ": END READING CHUNKS and distributing to MAPPERS");
				for (int i = 0; i < m_size; i++)
					Console.WriteLine ("buffer[" + i + "].Count = " + buffer [i].Count);

				Console.WriteLine (this.Rank + ": BEGIN SENDING CHUNKS to the MAPPERS ");
				// PERFORM
				for (int i = 0; i < m_size; i++)
					Split_channel.Send (buffer [i], unit_ref [i], end_iteration ? TAG_SPLIT_END_CHUNK : TAG_SPLIT_NEW_CHUNK);
				
				sync_perform.wait ();

				Console.WriteLine (this.Rank + ": END SENDING CHUNKS to the MAPPERS ");

				Task_binding_split_first.invoke (ITaskPortAdvance.CHUNK_READY);
			}

			Console.WriteLine (this.Rank + ": FINISH SPLITTER READ SOURCE ");

			t_output.Join ();

			Console.WriteLine (this.Rank + ": FINISH SPLITTER READ SOURCE - INVOKE TERMINATE");


		}
	}
}
