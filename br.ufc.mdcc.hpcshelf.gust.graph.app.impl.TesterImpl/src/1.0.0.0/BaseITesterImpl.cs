/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.app.TesterContext;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.app.Tester;

namespace br.ufc.mdcc.hpcshelf.gust.graph.app.impl.TesterImpl 
{
	public abstract class BaseITesterImpl: Application, BaseITester
	{
		private ITesterContext<IDirectedGraph<IDataContainerE<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>>, IDataContainerE<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>> comp = null;

		protected ITesterContext<IDirectedGraph<IDataContainerE<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>>, IDataContainerE<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>> Comp
		{
			get
			{
				if (this.comp == null)
					this.comp = (ITesterContext<IDirectedGraph<IDataContainerE<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>>, IDataContainerE<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>>) Services.getPort("comp");
				return this.comp;
			}
		}
		private IGraph<IDataContainerE<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>> graph = null;

		protected IGraph<IDataContainerE<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>> Graph
		{
			get
			{
				if (this.graph == null)
					this.graph = (IGraph<IDataContainerE<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>>) Services.getPort("graph");
				return this.graph;
			}
		}
	}
}