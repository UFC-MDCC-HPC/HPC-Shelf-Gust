/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;

namespace br.ufc.mdcc.common.impl.IteratorImpl { 

public abstract class BaseIIteratorImpl<T>: DataStructure, BaseIIterator<T>
where T:IData
{

		private T item_factory = default(T);

		protected T Item_factory {
			get {
				if (this.item_factory == null)
					this.item_factory = (T) Services.getPort("item_factory");
				return this.item_factory;
			}
		}

		private IPortTypeIterator port_type = null;

		protected IPortTypeIterator Port_type
		{
			get
			{
				if (this.port_type == null)
					this.port_type = (IPortTypeIterator) Services.getPort("port_type");
				return this.port_type;
			}
		}
}

}
