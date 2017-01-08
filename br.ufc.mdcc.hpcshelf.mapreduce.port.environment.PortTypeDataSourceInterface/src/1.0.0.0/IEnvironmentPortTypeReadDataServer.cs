using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;

namespace br.ufc.mdcc.hpcshelf.mapreduce.environment.Base.EnvironmentPortTypeReadDataServer
{
	public interface IEnvironmentPortTypeReadDataServer : BaseIEnvironmentPortTypeReadDataServer, IEnvironmentPortTypeSinglePartner
	{
		void read_data_1();
	}
}