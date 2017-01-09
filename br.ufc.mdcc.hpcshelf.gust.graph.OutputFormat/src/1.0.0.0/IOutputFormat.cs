using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.graph.OutputFormat
{
	public interface IOutputFormat<OKey, OValue> : BaseIOutputFormat<OKey, OValue>, IData
		where OKey:IData
		where OValue:IData
	{
	}
}