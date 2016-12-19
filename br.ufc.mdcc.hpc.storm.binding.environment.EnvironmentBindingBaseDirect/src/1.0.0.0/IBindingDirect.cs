using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect
{
	public interface IBindingDirect<C,S> : BaseIBindingDirect<C,S>, IClientBase<C>, IServerBase<S>
		where C:IEnvironmentPortTypeSinglePartner
		where S:IEnvironmentPortTypeSinglePartner
	{
	}
}