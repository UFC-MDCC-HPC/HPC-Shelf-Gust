using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.maintainer.SAFeHost;
using br.ufc.mdcc.hpcshelf.platform.Platform;

namespace br.ufc.mdcc.hpcshelf.platform.impl.SAFePlatform
{
	public class ISafePlatform<M> : BaseISafePlatform<M>, IProcessingNode<M>
where M:ISAFeHost
	{
	}
}
