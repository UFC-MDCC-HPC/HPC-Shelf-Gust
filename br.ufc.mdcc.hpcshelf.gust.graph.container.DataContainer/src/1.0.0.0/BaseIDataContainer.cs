/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer
{
	public interface BaseIDataContainer<V, E> : BaseIData, IDataStructureKind 
		where V:IDVertex
		where E:IDEdge<V>
	{
		E DEdgeFactory {get;}
		V DVertex {get;}
	}
}