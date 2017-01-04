using System;
using System.Collections;
using System.Collections.Generic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.graph.InputFormat
{
	public interface IInputFormat : BaseIInputFormat, IData
	{
	   IInputFormatInstance newInstanceIF();
	}
	public interface IInputFormatInstance : IDataInstance, ICloneable
	{
	   int[] Source { get; }
	   int[] Target { get; }
	   int[] PartitionTABLE { get; set; }
	   float[] Weight { get; }
	   
	   void Add(int i, int j);
	   void Add(int i, int j, float f);
	   void Clear ();
	   void Trim();
	   int firstVertex (int partID);
	   IDictionary<int, IInputFormatInstance> extractBins(string fileName);
	   
	   int ESIZE { get; }
	   int VSIZE { get; }
		int PARTITION_SIZE { get; set; }
		int PARTID { get; set; }
	}
}