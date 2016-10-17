/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;//Basic;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE
{
	public interface BaseIDataContainerE<V, E> : BaseIDataContainer<V, E>, IDataStructureKind 
		where V:IDVertexBasic
		where E:IDEdge<V>
	{
	}
}