/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;//Basic;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE
{
	public interface BaseIDataContainerE<V, E> : BaseIDataContainer<V, E>, IDataStructureKind 
		where V:IVertexBasic
		where E:IEdge<V>
	{
	}
}