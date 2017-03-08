using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;

namespace br.ufc.mdcc.hpcshelf.gust.impl.custom.PartitionFunctionInputFormatDefault
{
	public class IPartitionFunctionInputFormatDefault<TKey> : BaseIPartitionFunctionInputFormatDefault<TKey>, IPartitionFunction<TKey>
where TKey:IInputFormat
	{
		public IPartitionFunctionInputFormatDefault() { } 

		private int number_of_partitions = 1;
		public int NumberOfPartitions {
			get { return number_of_partitions; }
			set { this.number_of_partitions = value; }
		}

		private int[] graph_partition_table = null;
		public object PartitionTABLE { get{ return graph_partition_table; } set { graph_partition_table = (int[]) value; } }

		public override void main() 
		{ 
			IInputFormatInstance input_gif_instance = (IInputFormatInstance) Input_key.Instance;
			IIntegerInstance output_integer_instance = (IIntegerInstance) Output_key.Instance;

			int value = input_gif_instance.PARTID;
			//int value = graph_partition_table[((int) input_integer_instance.Value) -1];

			//Trace.WriteLine("BIN FUNCTION " + (value % NumberOfPartitions) + "value=" + value + ", npart=" + NumberOfPartitions);

			output_integer_instance.Value = value % NumberOfPartitions;

			if(this.graph_partition_table==null) 
				this.graph_partition_table = input_gif_instance.PartitionTABLE;
			Input_key.Instance = null;

		}
	}
}
