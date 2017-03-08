using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction
{
	public interface IPartitionFunction<IPK> : BaseIPartitionFunction<IPK>
		where IPK:IData
	{
		int NumberOfPartitions {set; get;}
		//int[] PartitionTABLE { get; set; }
		object PartitionTABLE { get; set; }
	}
}