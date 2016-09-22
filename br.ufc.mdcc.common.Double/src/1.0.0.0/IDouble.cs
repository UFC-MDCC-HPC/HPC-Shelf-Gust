using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using System;

namespace br.ufc.mdcc.common.Double { 
	public interface IDouble : BaseIDouble, IData{
		IDoubleInstance newInstance(double d);
	} // end main interface 
	public interface IDoubleInstance : ICloneable {
		double Value { set; get; }
	}
} // end namespace 
