using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeWeighted;//Basic;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV {
	public interface IDataContainerKV<V, E> : BaseIDataContainerKV<V, E>, IDataContainer<V, E>
		where V:IDVertexBasic
		where E:IDEdgeWeighted<V> {
		IDataContainerKVInstance<V, E, int> DataContainerKVInstance { get; set; }
		IDataContainerKVInstance<V, E, T> InstanceTFactory<T> (T i, T j, float w);
	}
	public interface IDataContainerKVInstance<V, E, TV> : IDataContainerInstance<V, E>, ICloneable 
		where V:IDVertexBasic 
		where E:IDEdgeWeighted<V>  {

		TV Vertex { get; set; }
		IDEdgeWeightedInstance<V, TV> EdgeFactory { set; get; }

		IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> DataSet { get; }
		void newDataSet (int size);
	}
}