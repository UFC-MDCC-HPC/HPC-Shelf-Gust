using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.port.environment.PortTypeIterator;
using System.Threading;

namespace br.ufc.mdcc.hpcshelf.mapreduce.impl.binding.environment.BindingDirectMxNIterator
{
	public class IBindingDirectMxNIterator<S> : BaseIBindingDirectMxNIterator<S>, br.ufc.mdcc.hpcshelf.mapreduce.binding.environment.BindingDirectMxNIterator.IBindingDirectMxNIterator<S>
		//where C: IPortTypeIterator
		where S: IPortTypeIterator
	{
		
		public override void main()
		{
		}

		private ManualResetEvent sync = new ManualResetEvent(false);

		public IPortTypeIterator Client 
		{ 
			get 
			{ 
				sync.WaitOne ();
				return server;
			} 
		}

		private S server = default(S);

		public S Server 
		{ 
			set 
			{ 
				this.server = value; 
				sync.Set ();
			} 

		}
	}
}
