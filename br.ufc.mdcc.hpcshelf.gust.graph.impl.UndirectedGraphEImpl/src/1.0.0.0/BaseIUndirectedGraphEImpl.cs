/* Automatically Generated Code */

using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.UndirectedGraphEImpl 
{
	public abstract class BaseIUndirectedGraphEImpl<CTN, V, E>: DataStructure, BaseIUndirectedGraph<CTN, V, E>
		where CTN:IDataContainerE<V, E>
		where V:IVertex
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
		private CTN dataContainer = default(CTN);

		public CTN DataContainer
		{
			get
			{
				if (this.dataContainer == null)
					this.dataContainer = (CTN) Services.getPort("dataContainer");
				return this.dataContainer;
			}
		}
	}
}