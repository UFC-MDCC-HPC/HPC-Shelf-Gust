using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV
{
	public interface IDataContainerV<V, E> : BaseIDataContainerV<V, E>, IDataContainer<V, E>
		where V:IVertex
		where E:IEdge<V> {
		IDataContainerVInstance<V, E, int, IEdgeInstance<V, int>> DataContainerVInstance { get; set; }
		IDataContainerVInstance<V, E, TV, TE> InstanceTFactory<TV, TE> (TE e) where TE: IEdgeInstance<V, TV>;
	}
	public interface IDataContainerVInstance<V, E, TV, TE> : IDataContainerInstance<V, E>, ICloneable 
		where V:IVertex 
		where E:IEdge<V> 
		where TE: IEdgeInstance<V, TV> {

		TV Vertex { get; set; }
		TE EdgeFactory { set; get; }
		IDictionary<TV, IEdgeContainer<TV>> DataSet { get; }
		//void newDataSet (int size);
	}
}
