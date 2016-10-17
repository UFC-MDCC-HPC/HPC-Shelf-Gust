using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic {
	public interface IDVertexBasic : BaseIDVertexBasic, IDVertex {
	}
	public interface IDVertexBasicInstance : IDVertexInstance, ICloneable {
		//int Id { get; set; }
	}
}