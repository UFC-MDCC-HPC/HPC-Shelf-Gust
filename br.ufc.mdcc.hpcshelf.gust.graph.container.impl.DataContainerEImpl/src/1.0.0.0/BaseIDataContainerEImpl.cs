/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
//using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
//using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;//Basic;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerEImpl 
{
	public abstract class BaseIDataContainerEImpl<V, E>: DataStructure, BaseIDataContainerE<V, E>
		where V:IVertexBasic
		where E:IEdge<V>
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