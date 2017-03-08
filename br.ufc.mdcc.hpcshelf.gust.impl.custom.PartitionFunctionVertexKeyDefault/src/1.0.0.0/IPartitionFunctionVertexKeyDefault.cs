using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;

namespace br.ufc.mdcc.hpcshelf.gust.impl.custom.PartitionFunctionVertexKeyDefault
{
	public class IPartitionFunctionVertexKeyDefault<TKey> : BaseIPartitionFunctionVertexKeyDefault<TKey>, IPartitionFunction<TKey>
where TKey:IVertex
	{
		public IPartitionFunctionVertexKeyDefault() { } 

		private int number_of_partitions;
		public int NumberOfPartitions {
			get { return number_of_partitions; }
			set { this.number_of_partitions = value; }
		}

		private int[] graph_partition_table;
		public object PartitionTABLE { get{ return graph_partition_table; } set { graph_partition_table = (int[]) value; } }

		public override void main() 
		{ 
			IVertexInstance input_vertex_instance = (IVertexInstance) Input_key.Instance;
			IIntegerInstance output_integer_instance = (IIntegerInstance) Output_key.Instance;
			int value = graph_partition_table[ Math.Abs(input_vertex_instance.Id) -1];
			output_integer_instance.Value = value % NumberOfPartitions;
		}
	}
}
