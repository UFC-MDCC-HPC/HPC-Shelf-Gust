using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNIntra
{
	public interface IClientMbyNIntra<C> : BaseIClientMbyNIntra<C>, IClientMbyN<C>
		where C:IEnvironmentPortTypeMultiplePartner
	{
	}
}