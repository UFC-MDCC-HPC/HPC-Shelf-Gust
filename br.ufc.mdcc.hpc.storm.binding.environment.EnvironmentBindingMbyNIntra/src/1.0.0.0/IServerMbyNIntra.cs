using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNIntra
{
	public interface IServerMbyNIntra<S> : BaseIServerMbyNIntra<S>, IServerMbyN<S>
		where S:IEnvironmentPortTypeMultiplePartner
	{
	}
}