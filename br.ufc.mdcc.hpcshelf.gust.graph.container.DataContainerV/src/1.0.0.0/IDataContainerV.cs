using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeBasic;
//using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV
{
	public interface IDataContainerV<V, E> : BaseIDataContainerV<V, E>, IDataContainer<V, E>
		where V:IDVertexBasic
		where E:IDEdgeBasic<V> {
		IDataContainerVInstance<V, E, int> DataContainerVInstance { get; set; }
		IDataContainerVInstance<V, E, T> InstanceTFactory<T> (T i, T j);
	}
	public interface IDataContainerVInstance<V, E, TV> : IDataContainerInstance<V, E>, ICloneable 
		where V:IDVertexBasic 
		where E:IDEdgeBasic<V> {

		TV Vertex { get; set; }
		IDEdgeBasicInstance<V, TV> EdgeFactory { set; get; }
		IDictionary<TV, IEdgeContainer<TV>> DataSet { get; }
		void newDataSet (int size);
	}
}