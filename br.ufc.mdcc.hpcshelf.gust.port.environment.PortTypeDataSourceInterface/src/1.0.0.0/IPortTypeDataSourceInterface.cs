using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSourceInterface
{
	public interface IPortTypeDataSourceInterface : BaseIPortTypeDataSourceInterface, IEnvironmentPortTypeSinglePartner
	{
		//string[] fetchFileNames ();
		//IEnumerable<string> fetchFileContents(string fileName);
		object fetchFileContentObject();
	}
}