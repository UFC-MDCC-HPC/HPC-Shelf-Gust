using System;
using System.Collections.Generic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.example.sssp.DataSSSP
{
	public interface IDataSSSP : BaseIDataSSSP, IData {
		IDataSSSPInstance InstanceDataSSSP { get; }
	} // end main interface 

	public interface IDataSSSPInstance : IDataInstance, ICloneable {
		IDictionary<int, float> Path_size { get; set; }
		bool Activated { get; set; }
	}
} // end namespace 
