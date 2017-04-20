using System;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;

namespace br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic 
{
	public interface IVertexBasic : BaseIVertexBasic, IVertex 
	{
	}
	public interface IVertexBasicInstance : IVertexInstance, ICloneable {
		//int Id { get; set; }
	}
}