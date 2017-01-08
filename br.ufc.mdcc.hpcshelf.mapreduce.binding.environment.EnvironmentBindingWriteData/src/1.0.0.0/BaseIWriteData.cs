/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSinkInterface;

namespace br.ufc.mdcc.hpcshelf.mapreduce.binding.environment.EnvironmentBindingWriteData
{
	public interface BaseIWriteData<S> : BaseIBindingDirect<IPortTypeIterator,S>, ISynchronizerKind 
		where S:IPortTypeDataSinkInterface
	{
	}
}