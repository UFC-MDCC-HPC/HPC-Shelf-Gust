using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.example.common.NTimesTerminateFunction
{
	public interface INTimesTerminateFunction : BaseINTimesTerminateFunction, ITerminateFunction<IData, IData, IData, IData>
	{
	}
}