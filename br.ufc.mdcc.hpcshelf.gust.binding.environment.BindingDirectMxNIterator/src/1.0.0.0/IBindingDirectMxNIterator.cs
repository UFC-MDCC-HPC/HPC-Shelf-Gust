using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;

namespace br.ufc.mdcc.hpcshelf.gust.binding.environment.BindingDirectMxNIterator
{
	public interface IBindingDirectMxNIterator<S> : BaseIBindingDirectMxNIterator<S>, IBindingDirect<IPortTypeIterator,S>
		where S:IPortTypeIterator
	{
	}
}