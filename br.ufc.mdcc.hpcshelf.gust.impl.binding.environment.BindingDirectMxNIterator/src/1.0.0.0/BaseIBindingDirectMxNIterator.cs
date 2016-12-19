/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;

namespace br.ufc.mdcc.hpcshelf.gust.impl.binding.environment.BindingDirectMxNIterator 
{
	public abstract class BaseIBindingDirectMxNIterator<S>: Synchronizer, br.ufc.mdcc.hpcshelf.gust.binding.environment.BindingDirectMxNIterator.BaseIBindingDirectMxNIterator<S>
		// where C:IPortTypeIterator
		where S:IPortTypeIterator
	{
		private IPortTypeIterator client_port_type = default(IPortTypeIterator);

		protected IPortTypeIterator Client_port_type
		{
			get
			{
				if (this.client_port_type == null)
					this.client_port_type = (IPortTypeIterator) Services.getPort("client_port_type");
				return this.client_port_type;
			}
		}
		private S server_port_type = default(S);

		protected S Server_port_type
		{
			get
			{
				if (this.server_port_type == null)
					this.server_port_type = (S) Services.getPort("server_port_type");
				return this.server_port_type;
			}
		}
	}
}