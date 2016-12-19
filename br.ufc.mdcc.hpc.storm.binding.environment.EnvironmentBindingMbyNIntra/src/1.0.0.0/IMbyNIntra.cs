using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNIntra
{
	public interface IMbyNIntra<C,S> : BaseIMbyNIntra<C,S>, IClientMbyN<C>, IServerMbyN<S>
		where C:IEnvironmentPortTypeMultiplePartner
		where S:IEnvironmentPortTypeMultiplePartner
	{
	}
}