using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;//Basic;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV {
	public interface IDataContainerKV<V, E> : BaseIDataContainerKV<V, E>, IDataContainer<V, E>
		where V:IVertexBasic
		where E:IEdgeWeighted<V> {
		IDataContainerKVInstance<V, E, int> DataContainerKVInstance { get; set; }
		IDataContainerKVInstance<V, E, T> InstanceTFactory<T> (T i, T j, float w);
	}
	public interface IDataContainerKVInstance<V, E, TV> : IDataContainerInstance<V, E>, ICloneable 
		where V:IVertexBasic 
		where E:IEdgeWeighted<V>  {

		TV Vertex { get; set; }
		IEdgeWeightedInstance<V, TV> EdgeFactory { set; get; }

		IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> DataSet { get; }
		//void newDataSet (int size);
	}
}