/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.common.Data;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.ContainerData
{
	public interface BaseIContainerData<V, E> : BaseIData, IDataStructureKind 
		where V:IVertex
		where E:IEdge<V>
	{
		E EdgeFactory {get;}
		V Vertex {get;}
	}
}