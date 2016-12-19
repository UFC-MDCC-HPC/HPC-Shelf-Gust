using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator
{
	public interface IPortTypeIterator : BaseIPortTypeIterator, IEnvironmentPortTypeMultiplePartner
	{
		ICloneable createItem ();

		void put(object item);

		// - raises exception if has finished and not restarted
		void finish();

		// tests whether there is an item to be read in the chunk
		bool has_next(); 

		// In result, it returns the next item. 
		// After dequeing the next chunk item, the return is true if has_next() is true or false, otherwise.
		// If the chunk is empty, an exception is thrown.
		bool fetch_next (out object result);
	}
}