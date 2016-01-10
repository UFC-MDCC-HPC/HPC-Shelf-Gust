/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.example.MbyN.EnvironmentPortTypeClient;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNIntra;

namespace br.ufc.mdcc.hpc.storm.binding.environment.impl.BindingMbyNIntraExample 
{
	public abstract class BaseIBindingMyNIntraExampleClient<C>: Synchronizer, BaseIClientMbyNIntra<C>
		where C:IMbyNClientPortTypeExample
	{
		private C client_port_type = default(C);

		protected C Client_port_type
		{
			get
			{
				if (this.client_port_type == null)
					this.client_port_type = (C) Services.getPort("client_port_type");
				return this.client_port_type;
			}
		}
	}
}