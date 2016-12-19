/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.GustyFunction;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic;
using br.ufc.mdcc.common.String;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.KVPair;

namespace br.ufc.mdcc.hpcshelf.gust.example.cw.Tallier
{
	public interface BaseITallier : BaseIGustyFunction<IString,IInteger,IString,IInteger,IString,IDirectedGraph<IDataContainerV<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>>>, IComputationKind 
	{
	}
}