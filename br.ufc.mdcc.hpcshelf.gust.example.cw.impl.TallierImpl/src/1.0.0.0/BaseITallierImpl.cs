/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.String;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.example.cw.Tallier;

using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeBasic;

namespace br.ufc.mdcc.hpcshelf.gust.example.cw.impl.TallierImpl 
{
	public abstract class BaseITallierImpl: Computation, BaseITallier
	{
		private IKVPair<IString, IIterator<IInteger>> input_values = null;

		public IKVPair<IString, IIterator<IInteger>> Input_values
		{
			get
			{
				if (this.input_values == null)
					this.input_values = (IKVPair<IString, IIterator<IInteger>>) Services.getPort("input_values");
				return this.input_values;
			}
		}
		private IKVPair<IInteger, IString> partition_graph = null;

		public IKVPair<IInteger, IString> Partition_graph
		{
			get
			{
				if (this.partition_graph == null)
					this.partition_graph = (IKVPair<IInteger, IString>) Services.getPort("partition_graph");
				return this.partition_graph;
			}
		}
		private IIterator<IKVPair<IString, IInteger>> output = null;

		public IIterator<IKVPair<IString, IInteger>> Output
		{
			get
			{
				if (this.output == null)
					this.output = (IIterator<IKVPair<IString, IInteger>>) Services.getPort("output");
				return this.output;
			}
		}
		private IInteger continuation_value = null;

		public IInteger Continuation_value
		{
			get
			{
				if (this.continuation_value == null)
					this.continuation_value = (IInteger) Services.getPort("continuation_value");
				return this.continuation_value;
			}
		}
		private IDirectedGraph<IDataContainerV<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>> graph = null;

		public IDirectedGraph<IDataContainerV<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>> Graph
		{
			get
			{
				if (this.graph == null)
					this.graph = (IDirectedGraph<IDataContainerV<IVertexBasic, IEdgeBasic<IVertexBasic>>, IVertexBasic, IEdgeBasic<IVertexBasic>>) Services.getPort("graph");
				return this.graph;
			}
		}
	}
}