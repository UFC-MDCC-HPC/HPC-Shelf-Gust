using System;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.common.Float {
	public interface IFloat : BaseIFloat, IData 
	{
		IFloatInstance newInstance(float f);
	} // end main interface 

	public interface IFloatInstance : IDataInstance, ICloneable
	{
		float Value { set; get; }
	}

} // end namespace 
