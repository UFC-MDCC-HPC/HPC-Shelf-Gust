using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.maintainer.DataHost;
using br.ufc.mdcc.hpcshelf.platform.Platform;

namespace br.ufc.mdcc.hpcshelf.platform.impl.DataPlatform
{
	public class IDataPlatform<M>: BaseIDataPlatform<M>, IProcessingNode<M>
where M:IDataHost
	{
	}
}
