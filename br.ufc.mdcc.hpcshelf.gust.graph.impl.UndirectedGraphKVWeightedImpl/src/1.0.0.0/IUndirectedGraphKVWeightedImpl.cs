using System;
using System.Collections.Generic;
using System.Linq;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.UndirectedGraphKVWeightedImpl {
	public class IUndirectedGraphKVWeightedImpl<CTN, V, E> : BaseIUndirectedGraphKVWeightedImpl<CTN, V, E>, IUndirectedGraph<CTN, V, E>
where CTN:IDataContainerKV<V, E>
where V:IVertexBasic
where E:IEdgeWeighted<V> {
		public override void main() {
		}
		private object instanceControl = null;
		public object InstanceControl { 
			get{ 
				return this.instanceControl;
			}
		}
		public IInstanceControlUndirected<V, E, TV, TE> newInstanceControlT<TV, TE> (TE e, int size)  where TE: IEdgeInstance<V, TV> {
			IDataContainerKVInstance<V, E, TV, TE> dc = DataContainer.InstanceTFactory<TV, TE>(e);
			dc.newDataSet (size);
			IGraphHelperKV<V, E, TV, TE> h = new IGraphHelperKVImpl<V, E, TV, TE>(dc);
			this.instanceControl = new InstanceControlImpl<V, E, TV, TE> (h);
			return (IInstanceControlUndirected<V, E, TV, TE>) this.instanceControl;
		}
		public IInstanceControlUndirected<V, E, int, IEdgeInstance<V, int>> newInstanceControl(int size) {
			IDataContainerKVInstance<V, E, int, IEdgeInstance<V, int>> dc = DataContainer.DataContainerKVInstance;
			dc.newDataSet (size);
			IGraphHelperKV<V, E, int, IEdgeInstance<V, int>> h = new IGraphHelperKVImpl<V, E, int, IEdgeInstance<V, int>>(dc);
			this.instanceControl = new InstanceControlImpl<V, E, int, IEdgeInstance<V, int>> (h);
			return (IInstanceControlUndirected<V, E, int, IEdgeInstance<V, int>>) this.instanceControl;
		}
		public class InstanceControlImpl<V, E, TV, TE>//: IInstanceControlUndirected<V, E, TV, TE> 
			where V:IVertexBasic  
			where E:IEdgeWeighted<V> 
			where TE: IEdgeInstance<V, TV> {

			public IGraphHelperKV<V, E, TV, TE> delegator;

			public IDataContainerInstance<V, E> DataContainer{
				get{ return this.delegator.Container; }
				set{ this.delegator.Container = (IDataContainerKVInstance<V, E, TV, TE>)value; }
			}

			public InstanceControlImpl(IGraphHelperKV<V, E, TV, TE> d){
				delegator = d;
			}
			//************** implementation ***********************

		}
		public interface IGraphHelperKV<V, E, TV, TE>//: IGraphHelper<V, E, TV, TE> 
			where V:IVertexBasic where E:IEdgeWeighted<V> where TE: IEdgeInstance<V, TV> {
			IDataContainerKVInstance<V, E, TV, TE> Container { get; set; }
		}
		internal class IGraphHelperKVImpl<V, E, TV, TE>: IGraphHelperKV<V, E, TV, TE> 
			where V:IVertexBasic 
			where E:IEdgeWeighted<V>
			where TE: IEdgeInstance<V, TV> {

			private int count_edges = 0;
			private IDataContainerKVInstance<V, E, TV, TE> container;

			public IGraphHelperKVImpl (IDataContainerKVInstance<V, E, TV, TE> c) {
				container = c;
			}

			public IDataContainerKVInstance<V, E, TV, TE> Container{
				get{ return this.container; }
				set{ this.container = (IDataContainerKVInstance<V, E, TV, TE>)value; }
			}
			public IDataContainerInstance<V, E> DataContainer{
				get{ return this.container; }
				set{ this.container = (IDataContainerKVInstance<V, E, TV, TE>)value; }
			}
			// ************************* implements ***********************************

		}
	}
}
