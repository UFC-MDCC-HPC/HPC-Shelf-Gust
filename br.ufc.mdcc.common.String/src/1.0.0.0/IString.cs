using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using System;

namespace br.ufc.mdcc.common.String { 

	public interface IString : BaseIString, IData
	{
		IStringInstance newInstance(string i);
	} // end main interface 

	public interface IStringInstance : IDataInstance, ICloneable
	{
		string Value { set; get; }
	}

} // end namespace 
