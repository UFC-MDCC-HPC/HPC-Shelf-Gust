/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
//using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
//using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;//Basic;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerKVImpl 
{
	public abstract class BaseIDataContainerKVImpl<V, E>: DataStructure, BaseIDataContainerKV<V, E>
		where V:IVertexBasic
		where E:IEdgeWeighted<V>
	{
		private E edgeFactory = default(E);

		public E EdgeFactory
		{
			get
			{
				if (this.edgeFactory == null)
					this.edgeFactory = (E) Services.getPort("edgeFactory");
				return this.edgeFactory;
			}
		}
		private V vertex = default(V);

		public V Vertex
		{
			get
			{
				if (this.vertex == null)
					this.vertex = (V) Services.getPort("vertex");
				return this.vertex;
			}
		}
	}
}