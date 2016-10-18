/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV
{
	public interface BaseIDataContainerV<V, E> : BaseIDataContainer<V, E>, IDataStructureKind 
		where V:IVertexBasic
		where E:IEdgeBasic<V>
	{
	}
}