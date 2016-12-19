using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.platform.Platform
{
	public interface IProcessingNode<M> : BaseIProcessingNode<M>
		where M:IMaintainer
	{
	}
}