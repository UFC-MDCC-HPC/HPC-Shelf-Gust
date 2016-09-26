/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.ContainerData
{
	public interface BaseIContainerData<V, E> : BaseIData, IDataStructureKind 
		where V:IVertex
		where E:IDEdge<V>
	{
		E DEdgeFactory {get;}
		V Vertex {get;}
	}
}