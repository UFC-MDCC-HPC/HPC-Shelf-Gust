using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingGathercast
{
	public interface IServerGathercast<S> : BaseIServerGathercast<S>, IServerBase<S>
		where S:IEnvironmentPortTypeMultiplePartner
	{
	}

}