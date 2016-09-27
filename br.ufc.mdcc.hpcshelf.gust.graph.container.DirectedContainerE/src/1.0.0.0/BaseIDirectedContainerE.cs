/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DirectedContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DirectedContainerE
{
	public interface BaseIDirectedContainerE<V, E> : BaseIDirectedContainer<V, E>, IDataStructureKind 
		where V:IDVertex
		where E:IDEdge<V>
	{
	}
}