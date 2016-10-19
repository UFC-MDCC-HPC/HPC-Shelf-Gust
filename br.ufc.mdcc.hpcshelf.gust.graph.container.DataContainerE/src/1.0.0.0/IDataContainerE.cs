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
		IDataContainerEInstance<V, E, int> DataContainerEInstance { get; set; }
		IDataContainerEInstance<V, E, T> InstanceTFactory<T> (T i, T j, float w);
	}
	public interface IDataContainerEInstance<V, E, TV> : IDataContainerInstance<V, E>, ICloneable 
		where V:IVertexBasic
		where E:IEdge<V>  {

		TV Vertex { get; set; }
		IEdgeInstance<V, TV> EdgeFactory { set; get; }

		IDictionary<TV, IEdgeContainer<IEdgeInstance<V, TV>>> DataSet { get; }
		//void newDataSet (int size);
	}
}