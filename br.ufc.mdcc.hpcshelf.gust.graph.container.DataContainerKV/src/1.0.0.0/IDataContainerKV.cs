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
	public interface IDataContainerKVInstance<V, E, RV> : IDataContainerInstance<V, E>, ICloneable 
		where V:IDVertexBasic 
		where E:IDEdgeWeighted<V>  {

		RV Vertex { get; set; }
		IDEdgeWeightedInstance<V, RV> EdgeFactory { set; get; }

		IDictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>> DataSet { get; }
		void newDataSet (int size);
	}
}