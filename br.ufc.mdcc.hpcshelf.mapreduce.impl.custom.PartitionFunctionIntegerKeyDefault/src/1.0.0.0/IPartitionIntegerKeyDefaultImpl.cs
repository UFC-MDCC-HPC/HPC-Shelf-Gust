using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;



namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.custom.PartitionFunctionIntegerKeyDefault { 

	public class IPartitionIntegerKeyDefaultImpl<TKey> : BaseIPartitionIntegerKeyDefaultImpl<TKey>, IPartitionFunction<TKey>
	where TKey:IInteger
	{
		public IPartitionIntegerKeyDefaultImpl() { } 

		private int number_of_partitions;
		public int NumberOfPartitions {
			get { return number_of_partitions; }
			set { this.number_of_partitions = value; }
		}

		private int[] graph_partition_table;
		public object PartitionTABLE { get{ return graph_partition_table; } set { graph_partition_table = (int[]) value; } }

		public override void main() 
		{ 
			IIntegerInstance input_integer_instance = (IIntegerInstance) Input_key.Instance;
			IIntegerInstance output_integer_instance = (IIntegerInstance) Output_key.Instance;

			int value = (int) input_integer_instance.Value;
			//int value = graph_partition_table[((int) input_integer_instance.Value) -1];

//			Trace.WriteLine("BIN FUNCTION " + (value % NumberOfPartitions) + "value=" + value + ", npart=" + NumberOfPartitions);

			output_integer_instance.Value = value % NumberOfPartitions;


		}
	}

}
