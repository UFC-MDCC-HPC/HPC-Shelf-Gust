/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction
{
	public interface BaseIPartitionFunction<IPK> : IComputationKind 
		where IPK:IData
	{
		IPK Input_key {get;}
		IInteger Output_key {get;}
	}
}