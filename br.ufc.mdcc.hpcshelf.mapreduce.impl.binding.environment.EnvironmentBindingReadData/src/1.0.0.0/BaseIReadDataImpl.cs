/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingBaseDirect;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeDataSourceInterface;
using br.ufc.mdcc.hpcshelf.mapreduce.binding.environment.EnvironmentBindingReadData;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.binding.environment.EnvironmentBindingReadData 
{
	public abstract class BaseIReadDataImpl<S>: Synchronizer, BaseIReadData<S>
		//where C:IPortTypeIterator
		where S:IPortTypeDataSourceInterface
	{
		private IPortTypeIterator client_port_type = null;
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

		private IIterator<IKVPair<IInteger,IInputFormat>> input_pairs_iterator = null;
		protected IIterator<IKVPair<IInteger,IInputFormat>> Input_pairs_iterator
		{
			get
			{
				if (this.input_pairs_iterator == null)
					this.input_pairs_iterator = (IIterator<IKVPair<IInteger,IInputFormat>>) Services.getPort("input_pairs_iterator");
				return this.input_pairs_iterator;
			}
		}

	}
}