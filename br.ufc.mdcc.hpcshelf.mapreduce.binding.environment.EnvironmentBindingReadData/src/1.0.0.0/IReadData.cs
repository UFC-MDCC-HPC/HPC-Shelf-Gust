using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSourceInterface;

namespace br.ufc.mdcc.hpcshelf.mapreduce.binding.environment.EnvironmentBindingReadData
{
	public interface IReadData<S> : BaseIReadData<S>, IBindingDirect<IPortTypeIterator,S>
		//where C:IPortTypeIterator
		where S:IPortTypeDataSourceInterface
	{

		void startReadSource(int part_size);
		//int[] PartitionTABLE{ get; set; }
		object PartitionTABLE{ get; set; }

	}
}