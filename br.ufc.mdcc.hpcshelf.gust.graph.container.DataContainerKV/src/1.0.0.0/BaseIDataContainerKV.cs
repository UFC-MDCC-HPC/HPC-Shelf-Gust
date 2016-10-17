/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeWeighted;//Basic;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV
{
	public interface BaseIDataContainerKV<V, E> : BaseIDataContainer<V, E>, IDataStructureKind 
		where V:IDVertexBasic
		where E:IDEdgeWeighted<V>
	{
	}
}