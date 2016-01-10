/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMulticast;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMulticastIntra
{
	public interface BaseIServerMulticastIntra<S> : BaseIServerMulticast<S>, ISynchronizerKind 
		where S:IEnvironmentPortTypeSinglePartner
	{
	}
}