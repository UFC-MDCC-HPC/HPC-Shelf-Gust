using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost;
using br.ufc.mdcc.hpcshelf.platform.Platform;

namespace br.ufc.mdcc.hpcshelf.platform.impl.ComputePlatform
{
	public class IComputePlatform<M> : BaseIComputePlatform<M>, IProcessingNode<M>
where M:IComputeHost
	{
	}
}
