using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using System;

namespace br.ufc.mdcc.common.Float { 
	public interface IFloat : BaseIFloat, IData{
		IFloatInstance newInstance(float d);
	} // end main interface 
	public interface IFloatInstance : ICloneable {
		float Value { set; get; }
	}
} // end namespace 
