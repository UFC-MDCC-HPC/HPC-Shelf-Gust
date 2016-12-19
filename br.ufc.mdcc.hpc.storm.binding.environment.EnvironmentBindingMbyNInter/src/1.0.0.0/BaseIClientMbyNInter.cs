/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNInter
{
	public interface BaseIClientMbyNInter<C> : BaseIClientMbyN<C>, ISynchronizerKind 
		where C:IEnvironmentPortTypeMultiplePartner
	{
	}
}