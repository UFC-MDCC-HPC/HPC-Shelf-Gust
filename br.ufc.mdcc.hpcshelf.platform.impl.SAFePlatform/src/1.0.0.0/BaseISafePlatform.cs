/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.platform.maintainer.SAFeHost;
using br.ufc.mdcc.hpcshelf.platform.Platform;

namespace br.ufc.mdcc.hpcshelf.platform.impl.SAFePlatform 
{
	public abstract class BaseISafePlatform<M>: br.ufc.pargo.hpe.kinds.Environment, BaseIProcessingNode<M>
		where M:ISAFeHost
	{
		private M maintainer = default(M);

		protected M Maintainer
		{
			get
			{
				if (this.maintainer == null)
					this.maintainer = (M) Services.getPort("maintainer");
				return this.maintainer;
			}
		}
	}
}