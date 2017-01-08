using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase
{
	public interface IServerBase<S> : BaseIServerBase<S>
		where S:IEnvironmentPortType
	{
		S Server { set; }
	}
}