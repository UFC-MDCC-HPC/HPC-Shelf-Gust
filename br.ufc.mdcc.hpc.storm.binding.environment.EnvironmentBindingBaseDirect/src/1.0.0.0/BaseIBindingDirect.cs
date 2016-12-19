/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect
{
	public interface BaseIBindingDirect<C,S> : BaseIClientBase<C>, BaseIServerBase<S>, ISynchronizerKind 
		where C:IEnvironmentPortTypeSinglePartner
		where S:IEnvironmentPortTypeSinglePartner
	{
	}
}