/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.app.TesterContext;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.app.Tester;

namespace br.ufc.mdcc.hpcshelf.gust.graph.app.impl.TesterImpl 
{
	public abstract class BaseITesterImpl: Application, BaseITester
	{
		private ITesterContext<IDirectedGraph<IDataContainerE<IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDataContainerE<IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDVertexBasic, IDEdgeBasic<IDVertexBasic>> comp = null;

		protected ITesterContext<IDirectedGraph<IDataContainerE<IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDataContainerE<IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDVertexBasic, IDEdgeBasic<IDVertexBasic>> Comp
		{
			get
			{
				if (this.comp == null)
					this.comp = (ITesterContext<IDirectedGraph<IDataContainerE<IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDataContainerE<IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDVertexBasic, IDEdgeBasic<IDVertexBasic>>) Services.getPort("comp");
				return this.comp;
			}
		}
		private IGraph<IDataContainerE<IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDVertexBasic, IDEdgeBasic<IDVertexBasic>> graph = null;

		protected IGraph<IDataContainerE<IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDVertexBasic, IDEdgeBasic<IDVertexBasic>> Graph
		{
			get
			{
				if (this.graph == null)
					this.graph = (IGraph<IDataContainerE<IDVertexBasic, IDEdgeBasic<IDVertexBasic>>, IDVertexBasic, IDEdgeBasic<IDVertexBasic>>) Services.getPort("graph");
				return this.graph;
			}
		}
	}
}