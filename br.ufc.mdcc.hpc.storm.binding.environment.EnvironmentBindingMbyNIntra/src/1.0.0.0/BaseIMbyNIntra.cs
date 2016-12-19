/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNIntra
{
	public interface BaseIMbyNIntra<C,S> : BaseIClientMbyN<C>, BaseIServerMbyN<S>, ISynchronizerKind 
		where C:IEnvironmentPortTypeMultiplePartner
		where S:IEnvironmentPortTypeMultiplePartner
	{
	}
}