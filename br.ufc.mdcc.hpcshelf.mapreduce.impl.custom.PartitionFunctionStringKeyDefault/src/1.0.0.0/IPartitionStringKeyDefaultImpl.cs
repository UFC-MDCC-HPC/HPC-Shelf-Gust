using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.String;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.custom.PartitionFunctionStringKeyDefault { 

	public class IPartitionStringKeyDefaultImpl<TKey> : BaseIPartitionStringKeyDefaultImpl<TKey>, IPartitionFunction<TKey>
	where TKey:IString
	{
		public IPartitionStringKeyDefaultImpl() { } 

		private int number_of_partitions;
		public int NumberOfPartitions {
			get { return number_of_partitions; }
			set { this.number_of_partitions = value; }
		}

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
