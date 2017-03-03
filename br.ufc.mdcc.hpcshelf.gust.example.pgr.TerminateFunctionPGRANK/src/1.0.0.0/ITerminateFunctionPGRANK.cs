using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.TerminateFunction;
using br.ufc.mdcc.hpcshelf.gust.example.pgr.DataPGRANK;
using br.ufc.mdcc.common.Integer;

namespace br.ufc.mdcc.hpcshelf.gust.example.pgr.TerminateFunctionPGRANK
{
	public interface ITerminateFunctionPGRANK : BaseITerminateFunctionPGRANK, ITerminateFunction<IInteger, IDataPGRANK, IInteger, IDataPGRANK>
	{
	}
}