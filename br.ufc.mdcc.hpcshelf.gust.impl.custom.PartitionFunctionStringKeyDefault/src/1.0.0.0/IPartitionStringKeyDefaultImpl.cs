using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.String;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.gust.impl.custom.PartitionFunctionStringKeyDefault { 

	public class IPartitionStringKeyDefaultImpl<TKey> : BaseIPartitionStringKeyDefaultImpl<TKey>, IPartitionFunction<TKey>
	where TKey:IString
	{
		public IPartitionStringKeyDefaultImpl() { } 

		private int number_of_partitions;
		public int NumberOfPartitions {
			get { return number_of_partitions; }
			set { this.number_of_partitions = value; }
		}

		private int[] graph_partition_table;
		public object PartitionTABLE { get{ return graph_partition_table; } set { graph_partition_table = (int[]) value; } }

		public override void main() 
		{ 
			IStringInstance input_string_instance = (IStringInstance) Input_key.Instance;
			IIntegerInstance output_string_instance = (IIntegerInstance) Output_key.Instance;

			int value = Math.Abs(input_string_instance.Value.GetHashCode());
			
			// Trace.WriteLine("PARTITION FUNCTION " + (value % NumberOfPartitions));
			
			output_string_instance.Value = value % NumberOfPartitions;
		}
	}

}
