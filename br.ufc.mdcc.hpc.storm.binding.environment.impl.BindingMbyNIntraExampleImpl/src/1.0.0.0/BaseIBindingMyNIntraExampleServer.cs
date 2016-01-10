/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.example.MbyN.EnvironmentPortTypeServer;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNIntra;

namespace br.ufc.mdcc.hpc.storm.binding.environment.impl.BindingMbyNIntraExample 
{
	public abstract class BaseIBindingMyNIntraExampleServer<S>: Synchronizer, BaseIServerMbyNIntra<S>
		where S:IMbyNServerPortTypeExample
	{
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