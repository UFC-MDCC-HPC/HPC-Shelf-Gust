using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DirectedGraphEImpl
{
	public class IDirectedGraphEImpl<CTN, V, E> : BaseIDirectedGraphEImpl<CTN, V, E>, IDirectedGraph<CTN, V, E>
where CTN:IDataContainerE<V, E>
where V:IVertexBasic
where E:IEdge<V>
	{
		public override void main()
		{
		}
		private object instanceControl = null;
		public object InstanceControl { 
			get{ 
				return this.instanceControl;
			}
		}
		public IInstanceControlDirected<V, E, TV, TE> newInstanceControlT<TV, TE> (TE e, int size)  where TE: IEdgeInstance<V, TV> {
			IDataContainerEInstance<V, E, TV> dc = DataContainer.InstanceTFactory<TV>(e.Source, e.Target, 1.0f);
			dc.newDataSet (size);
			IGraphHelperE<V, E, TV, TE> h = new IGraphHelperEImpl<V, E, TV, TE>(dc);
			this.instanceControl = new InstanceControlImpl<V, E, TV, TE> (h);
			return (IInstanceControlDirected<V, E, TV, TE>) this.instanceControl;
		}
		public IInstanceControlDirected<V, E, int, TE> newInstanceControl<TE> (int size)  where TE: IEdgeInstance<V, int> {
			DataContainer.DataContainerEInstance.newDataSet (size);
			IDataContainerEInstance<V, E, int> dc = DataContainer.DataContainerEInstance;
			IGraphHelperE<V, E, int, TE> h = new IGraphHelperEImpl<V, E, int, TE>(dc);
			this.instanceControl = new InstanceControlImpl<V, E, int, TE> (h);
			return (IInstanceControlDirected<V, E, int, TE>) this.instanceControl;
		}

		public class InstanceControlImpl<V, E, TV, TE>//: IInstanceControlDirected<V, E, TV, TE> 
			where V:IVertexBasic  
			where E:IEdge<V> 
			where TE: IEdgeInstance<V, TV> {

			public IGraphHelperE<V, E, TV, TE> delegator;

			public IDataContainerInstance<V, E> DataContainer{
				get{ return this.delegator.Container; }
				set{ this.delegator.Container = (IDataContainerEInstance<V, E, TV>)value; }
			}

			public InstanceControlImpl(IGraphHelperE<V, E, TV, TE> d){
				delegator = d;
			}
//			public override string ToString () { 
//				IEnumerator<TV> vertexSet = this.vertexSet ().GetEnumerator();
//				IEnumerator<TE> edgeSet = this.edgeSet ();
//				System.Text.StringBuilder sb = new System.Text.StringBuilder();
//				String ret = "";
//				sb.Append ("Vertexs [");
//				for(int i = 0; vertexSet.MoveNext();i++){
//					sb.Append (vertexSet.Current.ToString ()+",");
//				}
//				sb.Remove (sb.Length-1, 1);
//				sb.Append ("] \nedges ");
//				for (int i=0; edgeSet.MoveNext(); i++) {
//					sb.Append("(");
//					sb.Append (edgeSet.Current.ToString());
//					sb.Append(") ");
//				}
//				ret = sb.ToString ();
//				sb.Clear ();
//				return ret;
//			}
		}
		public interface IGraphHelperE<V, E, TV, TE>//: IGraphHelper<V, E, TV, TE> 
			where V:IVertexBasic where E:IEdge<V> where TE: IEdgeInstance<V, TV> {
			IDataContainerEInstance<V, E, TV> Container { get; set; }
		}
		internal class IGraphHelperEImpl<V, E, TV, TE>: IGraphHelperE<V, E, TV, TE> 
			where V:IVertexBasic 
			where E:IEdge<V>
			where TE: IEdgeInstance<V, TV> {

			private int count_edges = 0;
			private IDataContainerEInstance<V, E, TV> container;

			public IGraphHelperEImpl (IDataContainerEInstance<V, E, TV> c) {
				container = c;
			}

			public IDataContainerEInstance<V, E, TV> Container{
				get{ return this.container; }
				set{ this.container = (IDataContainerEInstance<V, E, TV>)value; }
			}
			public IDataContainerInstance<V, E> DataContainer{
				get{ return this.container; }
				set{ this.container = (IDataContainerEInstance<V, E, TV>)value; }
			}
		}
	}
}
