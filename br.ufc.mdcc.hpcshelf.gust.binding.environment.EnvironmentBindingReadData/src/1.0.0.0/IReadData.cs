using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSourceInterface;

namespace br.ufc.mdcc.hpcshelf.gust.binding.environment.EnvironmentBindingReadData
{
	public interface IReadData<S> : BaseIReadData<S>, IBindingDirect<IPortTypeIterator,S>
		//where C:IPortTypeIterator
		where S:IPortTypeDataSourceInterface
	{

		void startReadSource(int part_size);

	}
}