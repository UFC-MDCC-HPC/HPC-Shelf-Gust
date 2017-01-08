using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;

namespace br.ufc.mdcc.hpcshelf.mapreduce.environment.Base.EnvironmentPortTypeWriteDataServer
{
	public interface IEnvironmentPortTypeWriteDataServer : BaseIEnvironmentPortTypeWriteDataServer, IEnvironmentPortTypeSinglePartner
	{
		void write_data_1();
	}
}