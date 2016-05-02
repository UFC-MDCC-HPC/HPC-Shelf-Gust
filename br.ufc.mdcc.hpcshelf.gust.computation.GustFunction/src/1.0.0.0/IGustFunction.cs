using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.computation.GustFunction
{
	public interface IGustFunction<OIK, OIV, OOK, OOV> : BaseIGustFunction<OIK, OIV, OOK, OOV>
		where OIK:IData
		where OIV:IData
		where OOK:IData
		where OOV:IData
	{
	   IGustFunctionInstance[] getInstances();
	}
	public interface IGustFunctionInstance : ICloneable{

	}
}
