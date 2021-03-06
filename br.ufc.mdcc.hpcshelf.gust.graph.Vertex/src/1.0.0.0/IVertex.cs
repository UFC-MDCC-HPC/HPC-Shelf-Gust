using System;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.graph.Vertex 
{
	public interface IVertex : BaseIVertex, IData 
	{
		IVertexInstance newInstance(int i);
		IVertexInstance VInstance { get; }
	}
	public interface IVertexInstance : IDataInstance, ICloneable {
		int Id { set; get; }
	}
}
