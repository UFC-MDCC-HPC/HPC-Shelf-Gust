using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;
using br.ufc.mdcc.hpcshelf.gust.example.sssp.DataSSSP;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.gust.example.sssp.TerminateFunctionSSSP
{
	public interface ITerminateFunctionSSSP : BaseITerminateFunctionSSSP, ITerminateFunction<IInteger, IDataSSSP, IInteger, IDataSSSP>
	{
	}
}