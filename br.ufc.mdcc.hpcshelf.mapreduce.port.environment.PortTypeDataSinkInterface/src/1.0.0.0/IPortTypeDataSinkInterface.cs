using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSinkInterface
{
	public interface IPortTypeDataSinkInterface : BaseIPortTypeDataSinkInterface, IEnvironmentPortTypeSinglePartner
	{
		void writeLines (IEnumerable<string> pairs); 
		void resetOutput ();
		IEnumerable<string> readOutput ();
		object IteratorProvider{ get; }
		string formatRepresentation (object kv_pair);
	}
}