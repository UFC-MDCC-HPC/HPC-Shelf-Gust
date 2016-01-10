using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMulticast
{
	public interface IServerMulticast<S> : BaseIServerMulticast<S>, IServerBase<S>
		where S:IEnvironmentPortTypeSinglePartner
	{
	}
}