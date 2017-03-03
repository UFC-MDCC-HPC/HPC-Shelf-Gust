/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.common.Iterator;
using br.ufc.mdcc.common.KVPair;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.gust.example.pgr.DataPGRANK;
using br.ufc.mdcc.hpcshelf.gust.example.pgr.PGRANK;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;

namespace br.ufc.mdcc.hpcshelf.gust.example.pgr.PGRANKImpl 
{
	public abstract class BaseIPGRANKImpl: Computation, BaseIPGRANK
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

		private IDirectedGraph<IDataContainerKV<IVertex, IEdgeWeighted<IVertex>>, IVertex, IEdgeWeighted<IVertex>> graph = null;
		public IDirectedGraph<IDataContainerKV<IVertex, IEdgeWeighted<IVertex>>, IVertex, IEdgeWeighted<IVertex>> Graph
		{
			get
			{
				if (this.graph == null)
					this.graph = (IDirectedGraph<IDataContainerKV<IVertex, IEdgeWeighted<IVertex>>, IVertex, IEdgeWeighted<IVertex>>) Services.getPort("graph");
				return this.graph;
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
		private IIterator<IKVPair<IInteger, IDataPGRANK>> output = null;

		public IIterator<IKVPair<IInteger, IDataPGRANK>> Output
		{
			get
			{
				if (this.output == null)
					this.output = (IIterator<IKVPair<IInteger, IDataPGRANK>>) Services.getPort("output");
				return this.output;
			}
		}
		private IKVPair<IInteger, IIterator<IDataPGRANK>> input_values = null;

		public IKVPair<IInteger, IIterator<IDataPGRANK>> Input_values
		{
			get
			{
				if (this.input_values == null)
					this.input_values = (IKVPair<IInteger, IIterator<IDataPGRANK>>) Services.getPort("input_values");
				return this.input_values;
			}
		}
	}
}