using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSinkInterface;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;

namespace br.ufc.mdcc.hpcshelf.gust.binding.environment.EnvironmentBindingWriteData
{
	public interface IWriteData<S> : BaseIWriteData<S>, IBindingDirect<IPortTypeIterator,S>
		where S:IPortTypeDataSinkInterface
	{
		void startWriteSink();
	}
}