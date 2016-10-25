using System;
using System.Collections.Generic;
using System.Linq;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.UndirectedGraphEImpl {
	public class IUndirectedGraphEImpl<CTN, V, E> : BaseIUndirectedGraphEImpl<CTN, V, E>, IUndirectedGraph<CTN, V, E>
where CTN:IDataContainerE<V, E>
where V:IVertexBasic
where E:IEdge<V> {
		public override void main(){
		}
		private object instanceControl = null;
		public object InstanceControl { 
			get{ 
				return this.instanceControl;
			}
		}
		public IInstanceControlUndirected<V, E, TV, TE> newInstanceControlT<TV, TE> (TE e, int size)  where TE: IEdgeInstance<V, TV> {
			IDataContainerEInstance<V, E, TV, TE> dc = DataContainer.InstanceTFactory<TV, TE>(e);
			dc.newDataSet (size);
			IGraphHelperE<V, E, TV, TE> h = new IGraphHelperEImpl<V, E, TV, TE>(dc);
			this.instanceControl = new InstanceControlImpl<V, E, TV, TE> (h);
			return (IInstanceControlUndirected<V, E, TV, TE>) this.instanceControl;
		}
		public IInstanceControlUndirected<V, E, int, IEdgeInstance<V, int>> newInstanceControl(int size) {
			IDataContainerEInstance<V, E, int, IEdgeInstance<V, int>> dc = DataContainer.DataContainerEInstance;
			dc.newDataSet (size);
			IGraphHelperE<V, E, int, IEdgeInstance<V, int>> h = new IGraphHelperEImpl<V, E, int, IEdgeInstance<V, int>>(dc);
			this.instanceControl = new InstanceControlImpl<V, E, int, IEdgeInstance<V, int>> (h);
			return (IInstanceControlUndirected<V, E, int, IEdgeInstance<V, int>>) this.instanceControl;
		}
		public class InstanceControlImpl<V, E, TV, TE>//: IInstanceControlUndirected<V, E, TV, TE> 
			where V:IVertexBasic  
			where E:IEdge<V> 
			where TE: IEdgeInstance<V, TV> {

			public IGraphHelperE<V, E, TV, TE> delegator;

			public IDataContainerInstance<V, E> DataContainer{
				get{ return this.delegator.Container; }
				set{ this.delegator.Container = (IDataContainerEInstance<V, E, TV, TE>)value; }
			}

			public InstanceControlImpl(IGraphHelperE<V, E, TV, TE> d){
				delegator = d;
			}
			//************** implementation ***********************

		}
		public interface IGraphHelperE<V, E, TV, TE>//: IGraphHelper<V, E, TV, TE> 
			where V:IVertexBasic where E:IEdge<V> where TE: IEdgeInstance<V, TV> {
			IDataContainerEInstance<V, E, TV, TE> Container { get; set; }
		}
		internal class IGraphHelperEImpl<V, E, TV, TE>: IGraphHelperE<V, E, TV, TE> 
			where V:IVertexBasic 
			where E:IEdge<V>
			where TE: IEdgeInstance<V, TV> {

			private int count_edges = 0;
			private IDataContainerEInstance<V, E, TV, TE> container;

			public IGraphHelperEImpl (IDataContainerEInstance<V, E, TV, TE> c) {
				container = c;
			}

			public IDataContainerEInstance<V, E, TV, TE> Container{
				get{ return this.container; }
				set{ this.container = (IDataContainerEInstance<V, E, TV, TE>)value; }
			}
			public IDataContainerInstance<V, E> DataContainer{
				get{ return this.container; }
				set{ this.container = (IDataContainerEInstance<V, E, TV, TE>)value; }
			}
			// ************************* implements ***********************************

		}
	}
}