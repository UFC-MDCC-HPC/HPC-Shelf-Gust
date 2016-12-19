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

		public override void main() 
		{ 
			IIntegerInstance input_string_instance = (IIntegerInstance) Input_key.Instance;
			IIntegerInstance output_string_instance = (IIntegerInstance) Output_key.Instance;

			int value = (int) input_string_instance.Value;

//			Trace.WriteLine("BIN FUNCTION " + (value % NumberOfPartitions) + "value=" + value + ", npart=" + NumberOfPartitions);

			output_string_instance.Value = value % NumberOfPartitions;


		}
	}

}
