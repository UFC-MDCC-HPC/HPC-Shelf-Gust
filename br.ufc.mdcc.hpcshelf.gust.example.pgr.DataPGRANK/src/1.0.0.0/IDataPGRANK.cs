using System;
using System.Collections.Generic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.example.pgr.DataPGRANK
{
	public interface IDataPGRANK : BaseIDataPGRANK, IData {
		IDataPGRANKInstance InstanceDataPGRANK { get; }
	} // end main interface 

	public interface IDataPGRANKInstance : IDataInstance, ICloneable {
		IDictionary<int, float> Ranks { get; set; }
		float Slice { get; set; }
	}
} // end namespace 
