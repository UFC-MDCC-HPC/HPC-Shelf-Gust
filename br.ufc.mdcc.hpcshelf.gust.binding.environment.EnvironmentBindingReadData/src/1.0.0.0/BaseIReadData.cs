/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSourceInterface;

namespace br.ufc.mdcc.hpcshelf.gust.binding.environment.EnvironmentBindingReadData
{
	public interface BaseIReadData<S> : BaseIBindingDirect<IPortTypeIterator,S>, ISynchronizerKind 
		//where C:IPortTypeIterator
		where S:IPortTypeDataSourceInterface
	{
	}
}