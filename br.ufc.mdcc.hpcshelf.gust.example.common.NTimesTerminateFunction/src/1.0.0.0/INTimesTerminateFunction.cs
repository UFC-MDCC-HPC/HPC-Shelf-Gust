using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;
using br.ufc.mdcc.hpcshelf.gust.example.common.DataSSSP;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.gust.example.common.NTimesTerminateFunction
{
	public interface INTimesTerminateFunction : BaseINTimesTerminateFunction, ITerminateFunction<IInteger, IDataSSSP, IInteger, IDataSSSP>
	{
	}
}