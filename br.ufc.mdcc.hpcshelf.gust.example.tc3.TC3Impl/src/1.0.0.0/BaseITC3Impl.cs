/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.example.tc.DataTriangle;
using br.ufc.mdcc.hpcshelf.gust.example.tc3.TC3;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic;

namespace br.ufc.mdcc.hpcshelf.gust.example.tc3.TC3Impl 
{
	public abstract class BaseITC3Impl: Computation, BaseITC3
	{
		private IIterator<IKVPair<IInteger, IInputFormat>> output_gif = null;

		public IIterator<IKVPair<IInteger, IInputFormat>> Output_gif
		{
			get
			{
				if (this.output_gif == null)
					this.output_gif = (IIterator<IKVPair<IInteger, IInputFormat>>) Services.getPort("output_gif");
				return this.output_gif;
			}
		}
		
		private IUndirectedGraph<IDataContainerV<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>> graph = null;
		public IUndirectedGraph<IDataContainerV<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>> Graph
		{
			get
			{
				if (this.graph == null)
					this.graph = (IUndirectedGraph<IDataContainerV<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>>) Services.getPort("graph");
				return this.graph;
			}
		}
		
//		private IData continuation_value = null;		
//		protected IData Continuation_value
//		{
//			get
//			{
//				if (this.continuation_value == null)
//					this.continuation_value = (IData) Services.getPort("continuation_value");
//				return this.continuation_value;
//			}
//		}
		
		private IKVPair<IVertex, IIterator<IDataTriangle>> input_values = null;

		public IKVPair<IVertex, IIterator<IDataTriangle>> Input_values
		{
			get
			{
				if (this.input_values == null)
					this.input_values = (IKVPair<IVertex, IIterator<IDataTriangle>>) Services.getPort("input_values");
				return this.input_values;
			}
		}
		private IKVPair<IInteger, IIterator<IInputFormat>> graph_values = null;

		public IKVPair<IInteger, IIterator<IInputFormat>> Graph_values
		{
			get
			{
				if (this.graph_values == null)
					this.graph_values = (IKVPair<IInteger, IIterator<IInputFormat>>) Services.getPort("graph_values");
				return this.graph_values;
			}
		}
		private IIterator<IKVPair<IVertex, IDataTriangle>> output = null;

		public IIterator<IKVPair<IVertex, IDataTriangle>> Output
		{
			get
			{
				if (this.output == null)
					this.output = (IIterator<IKVPair<IVertex, IDataTriangle>>) Services.getPort("output");
				return this.output;
			}
		}
	}
}
