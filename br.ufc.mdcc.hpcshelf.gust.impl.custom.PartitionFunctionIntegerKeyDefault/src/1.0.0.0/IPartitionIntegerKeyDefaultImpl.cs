using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;



namespace br.ufc.mdcc.hpcshelf.gust.impl.custom.PartitionFunctionIntegerKeyDefault { 

	public class IPartitionIntegerKeyDefaultImpl<TKey> : BaseIPartitionIntegerKeyDefaultImpl<TKey>, IPartitionFunction<TKey>
	where TKey:IInteger
	{
		public IPartitionIntegerKeyDefaultImpl() { } 

		private int number_of_partitions;
		public int NumberOfPartitions {
			get { return number_of_partitions; }
			set { this.number_of_partitions = value; }
		}
		//Example, para um grafo particionado em dois subgrafos, graph0 e graph1, com os vértices ID 1,2,3 e 4, a tabela segue PartitionTABLE[ID-1]=partitionID:
		//PartitionTABLE[0]=1 PartitionTABLE[1]=0 PartitionTABLE[2]=0 PartitionTABLE[3]=1
		// 
		private int[] graph_partition_table;
		public int[] PartitionTABLE { get{ return graph_partition_table; } set { graph_partition_table = (int[]) value; } }

		public override void main() 
		{ 
			IIntegerInstance input_integer_instance = (IIntegerInstance) Input_key.Instance;
			IIntegerInstance output_integer_instance = (IIntegerInstance) Output_key.Instance;

			int value = PartitionTABLE[((int) input_integer_instance.Value) -1];

//			Trace.WriteLine("BIN FUNCTION " + (value % NumberOfPartitions) + "value=" + value + ", npart=" + NumberOfPartitions);

			output_integer_instance.Value = value % NumberOfPartitions;


		}
	}

}
