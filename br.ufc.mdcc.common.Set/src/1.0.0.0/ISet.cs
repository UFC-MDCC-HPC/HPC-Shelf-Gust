using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.common.Set
{
	public interface ISet<T> : BaseISet<T>, IData
	where T:IData
	{
	}
}