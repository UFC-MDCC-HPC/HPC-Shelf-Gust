using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.DVertex {
	public interface IDVertex : BaseIDVertex, IData {
		IDVertexInstance newInstance(int i);
		IDVertexInstance VInstance { get; }
	}
	public interface IDVertexInstance : IDataInstance, ICloneable {
		int Id { set; get; }
	}
}
