using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;//Basic;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE
{
	public interface IDataContainerE<V, E> : BaseIDataContainerE<V, E>, IDataContainer<V, E>
		where V:IVertexBasic
		where E:IEdge<V> {
		IDataContainerEInstance<V, E, int, IEdgeInstance<V, int>> DataContainerEInstance { get; set; }
		IDataContainerEInstance<V, E, TV, TE> InstanceTFactory<TV, TE> (TE e) where TE: IEdgeInstance<V, TV>;
	}
	public interface IDataContainerEInstance<V, E, TV, TE> : IDataContainerInstance<V, E>, ICloneable 
		where V:IVertexBasic
		where E:IEdge<V>  
		where TE: IEdgeInstance<V, TV> {

		TV Vertex { get; set; }
		TE EdgeFactory { set; get; }

		IDictionary<TV, IEdgeContainer<TE>> DataSet { get; }
		//void newDataSet (int size);
	}
}