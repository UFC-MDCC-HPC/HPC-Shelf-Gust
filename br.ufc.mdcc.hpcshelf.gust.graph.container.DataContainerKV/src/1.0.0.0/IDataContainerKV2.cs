using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV {
	public interface IDataContainerKV<V, E> : BaseIDataContainerKV<V, E>, IDataContainer<V, E>
		where V:IVertexBasic
		where E:IEdgeWeighted<V> {
		IDataContainerKVInstance<V, E, int, IEdgeInstance<V, int>> DataContainerKVInstance { get; set; }
		IDataContainerKVInstance<V, E, TV, TE> InstanceTFactory<TV, TE> (TE e) where TE: IEdgeInstance<V, TV>;
	}
	public interface IDataContainerKVInstance<V, E, TV, TE> : IDataContainerInstance<V, E>, ICloneable 
		where V:IVertexBasic 
		where E:IEdgeWeighted<V>  
		where TE:IEdgeInstance<V, TV> {

		TV Vertex { get; set; }
		TE EdgeFactory { set; get; }

		IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> DataSet { get; }
		//void newDataSet (int size);
	}
}