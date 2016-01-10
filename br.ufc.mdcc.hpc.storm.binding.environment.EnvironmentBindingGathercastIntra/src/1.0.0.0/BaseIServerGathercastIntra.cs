/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingGathercast;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingGathercastIntra
{
	public interface BaseIServerGathercastIntra<S> : BaseIServerGathercast<S>, ISynchronizerKind 
		where S:IEnvironmentPortTypeMultiplePartner
	{
	}
}