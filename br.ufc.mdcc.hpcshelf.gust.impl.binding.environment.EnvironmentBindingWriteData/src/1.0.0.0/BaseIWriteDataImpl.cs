/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpcshelf.gust.port.environment.PortTypeDataSinkInterface;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect;
using br.ufc.mdcc.hpcshelf.gust.binding.environment.EnvironmentBindingWriteData;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.String;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.gust.impl.binding.environment.EnvironmentBindingWriteData 
{
	public abstract class BaseIWriteDataImpl<S>: Synchronizer, BaseIWriteData<S>
		//where C:IPortTypeIterator
		where S:IPortTypeDataSinkInterface
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

		private IIterator<IKVPair<IString,IInteger>> output_pairs_iterator = null;
		protected IIterator<IKVPair<IString,IInteger>> Output_pairs_iterator
		{
			get
			{
				if (this.output_pairs_iterator == null)
					this.output_pairs_iterator = (IIterator<IKVPair<IString,IInteger>>) Services.getPort("output_pairs_iterator");
				return this.output_pairs_iterator;
			}
		}
	}
}