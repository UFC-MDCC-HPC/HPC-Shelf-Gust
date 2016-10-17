using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;//Basic;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE
{
	public interface IDataContainerE<V, E> : BaseIDataContainerE<V, E>, IDataContainer<V, E>
		where V:IDVertexBasic
		where E:IDEdge<V> {
		IDataContainerEInstance<V, E, int> DataContainerEInstance { get; set; }
		IDataContainerEInstance<V, E, T> InstanceTFactory<T> (T i, T j, float w);
	}
	public interface IDataContainerEInstance<V, E, RV> : IDataContainerInstance<V, E>, ICloneable 
		where V:IDVertexBasic
		where E:IDEdge<V>  {

		RV Vertex { get; set; }
		IDEdgeInstance<V, RV> EdgeFactory { set; get; }

		IDictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>> DataSet { get; }
		void newDataSet (int size);
	}
}