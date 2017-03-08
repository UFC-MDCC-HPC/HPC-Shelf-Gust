using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using System;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;

namespace br.ufc.mdcc.common.Iterator { 
	
	public interface IIterator<T> : IData, BaseIIterator<T>
		where T:IData
	{
		IIteratorInstance<T> newIteratorInstance();
		object createItem ();

	} // end main interface 

	public interface IIteratorInstance<T> : IDataInstance, ICloneable, IPortTypeIterator
		where T:IData
	{
		// consumer:
		ICloneable createItem ();

		// - raises exception if has finished
		void put(object item);

		void putAll(IIteratorInstance<T> items);

		// - raises exception if has finished and not restarted
		void finish();

		// producer:

		// - raises exception if has finished
		bool fetch_next (out object result);

		bool has_next(); 
	}



} // end namespace 
