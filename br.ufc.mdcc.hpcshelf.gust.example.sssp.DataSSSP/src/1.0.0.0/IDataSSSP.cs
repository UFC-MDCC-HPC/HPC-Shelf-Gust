using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.example.sssp.DataSSSP
{
	public interface IDataSSSP : BaseIDataSSSP, IData {
		IDataSSSPInstance newInstance(int partid);
		IDataSSSPInstance InstanceDataSSSP { get; }
	} // end main interface 

	public interface IDataSSSPInstance : IDataInstance, ICloneable {
		ConcurrentDictionary<int, float> Path_size { get; set; }
		int Partid { get; set; }
		bool Finished { get; set; }
	}
} // end namespace 
