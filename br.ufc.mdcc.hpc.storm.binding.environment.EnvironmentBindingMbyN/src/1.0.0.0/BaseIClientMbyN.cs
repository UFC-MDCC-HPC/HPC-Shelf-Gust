/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN
{
	public interface BaseIClientMbyN<C> : BaseIClientBase<C>, ISynchronizerKind 
		where C:IEnvironmentPortTypeMultiplePartner
	{
	}
}