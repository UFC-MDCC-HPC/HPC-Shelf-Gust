/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSourceInterface;

namespace br.ufc.mdcc.hpcshelf.mapreduce.binding.environment.EnvironmentBindingReadData
{
	public interface BaseIReadData<S> : BaseIBindingDirect<IPortTypeIterator,S>, ISynchronizerKind 
		//where C:IPortTypeIterator
		where S:IPortTypeDataSourceInterface
	{
	}
}