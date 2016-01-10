/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultipleClient;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNIntra
{
	public interface BaseIServerMbyNIntra<S> : BaseIServerMbyN<S>, ISynchronizerKind 
		where S:IEnvironmentPortTypeMultipleClient
	{
	}
}