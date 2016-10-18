using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DirectedGraphVImpl {
	public class IDirectedGraphVImpl<CTN, V, E> : BaseIDirectedGraphVImpl<CTN, V, E>, IDirectedGraph<CTN, V, E>
		where CTN:IDataContainerV<V, E>
		where V:IDVertexBasic
		where E:IDEdgeBasic<V> {

		public override void main() {
		}
		public IGraphHelper<V, E, T, IDEdgeBasicInstance<V, T>> InstanceTFactoryHelper<T>(T i, T j){
			return new IGraphHelperVImpl<V, E, T, IDEdgeBasicInstance<V, T>> (DataContainer.InstanceTFactory<T>(i, j));		
		}
		public IGraphHelper<V, E, int, IDEdgeBasicInstance<V, int>> createDefaultHelper(){
			return new IGraphHelperVImpl<V, E, int, IDEdgeBasicInstance<V, int>> (DataContainer.DataContainerVInstance);	
		}

		public class DirectedGraphV<V, E, TV, TE> where V:IDVertexBasic  where E:IDEdgeBasic<V> where TE: IDEdgeBasicInstance<V, TV> {

			public IGraphHelperV<V, E, TV, TE> delegator;

			public DirectedGraphV(IGraphHelperV<V, E, TV, TE> d){
				delegator = d;
			}
			public ICollection<TE> getAllEdges (TV sourceVertex, TV targetVertex) {
				ICollection<TE> edges = null; 

				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {
					edges = new List<TE> ();

					IEnumerator<TV> iter = delegator.outgoing<TV> (sourceVertex).GetEnumerator();
					while (iter.MoveNext ()) {

						if (iter.Current.Equals (targetVertex)) {
							TE e = (TE) delegator.Container.EdgeFactory.newInstance (sourceVertex, iter.Current);
							edges.Add (e);
						}
					}
				}

				return edges;
			}
			public TE getEdge (TV sourceVertex, TV targetVertex) {
				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {

					IEnumerator<TV> iter = delegator.outgoing<TV> (sourceVertex).GetEnumerator();
					while (iter.MoveNext ()) {

						if (iter.Current.Equals (targetVertex)) {
							TE e = (TE) delegator.Container.EdgeFactory.newInstance (sourceVertex, iter.Current);
							return e;
						}
					}
				}

				return default(TE);
			}
			public TE addEdge (TV sourceVertex, TV targetVertex) {
				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {
					if (!delegator.Container.AllowingMultipleEdges && containsEdge (sourceVertex, targetVertex)) {
						return default(TE);
					}

					if (!delegator.Container.AllowingLoops && sourceVertex.Equals (targetVertex)) { throw new ArgumentException ("loops not allowed"); }

					TE e = (TE) delegator.Container.EdgeFactory.newInstance (sourceVertex, targetVertex);
					addEdgeToContainer (e);
					return e;
				}
				return default(TE);
			}
			public bool addEdge (TE e) {
				if (e == null || e.Source == null || e.Target == null) {
					throw new EntryPointNotFoundException ();
				} 
				if (containsVertex (e.Source) && containsVertex (e.Target)) {
					if (!delegator.Container.AllowingMultipleEdges && containsEdge (e.Source, e.Target)) {
						return false;
					}

					if (!delegator.Container.AllowingLoops && e.Source.Equals (e.Target)) {
						throw new ArgumentException ("loops not allowed");
					}
					addEdgeToContainer (e);
					return true;
				} else {
					return false;
				}
			}
			public bool addVertex (TV v) {
				return delegator.addVertex (v);
			}
			public bool containsEdge(TV sourceVertex, TV targetVertex){
				return delegator.outgoing<TV> (sourceVertex).Contains (targetVertex);
			}
			public bool containsEdge(TE e){
				if (e != null) {
					if (containsVertex (e.Source) && containsVertex (e.Target))
						return delegator.outgoing<TV> (e.Source).Contains (e.Target);
					else
						return false;
				}
				return false;
			}
			public bool containsVertex (TV v){
				if(v==null)
					throw new ArgumentNullException ("WARNING: Method containsVertex receive parameter null! ");
				return delegator.containsVertex (v);
			}
			public IEnumerator<TE> edgeSet () {
				return delegator.edgeSet ();
			}
			public ICollection<TE> edgesOf (TV vertex) {
				return delegator.edgesOf (vertex);
			}
			public IEnumerator<TE> iteratorEdgesOf (TV vertex) {
				return delegator.iteratorEdgesOf (vertex);
			}
			public ICollection<TV> neighborsOf (TV vertex){
				ICollection<TV> o = delegator.outgoing<TV> (vertex);
				ICollection<TV> i = delegator.incoming<TV> (vertex);
				return new HashSet<TV> (o.Union (i));
			}
			public IEnumerator<TV> iteratorNeighborsOf (TV vertex){
				ICollection<TV> o = delegator.outgoing<TV> (vertex);
				ICollection<TV> i = delegator.incoming<TV> (vertex);
				foreach(TV v in o)
					yield return v;
				foreach(TV v in i)
					yield return v;
			}
			public bool removeAllEdges(ICollection<TE> edges){
				bool modified = false;

				foreach (TE e in edges) {
					modified |= removeEdge(e);
				}

				return modified;
			}
			public ICollection<TE> removeAllEdges(TV sourceVertex, TV targetVertex){
				ICollection<TE> removed = getAllEdges(sourceVertex, targetVertex);
				if (removed == null) {
					return null;
				}
				removeAllEdges(removed);

				return removed;
			}
			public bool removeAllVertices(ICollection<TV> vertices){
				bool modified = false;

				foreach (TV v in vertices) {
					modified |= removeVertex(v);
				}

				return modified;
			}

			public TE removeEdge (TV sourceVertex, TV targetVertex) {
				TE e = getEdge (sourceVertex, targetVertex);

				if (e != null) {
					removeEdgeFromContainer (e);
				}
				return e;
			}
			public bool removeEdge (TE e) {
				if (containsEdge (e)) {
					removeEdgeFromContainer (e);
					return true;
				} 
				else { return false; }
			}
			public bool removeVertex (TV v) {
				if (containsVertex (v)) {
					ICollection<TE> edges = edgesOf (v);
					foreach (TE e in edges) {
						removeEdgeFromContainer (e);
					}
					return delegator.removeVertex (v);
				} else {
					return false;
				}
			}
			public ICollection<TV> vertexSet () {
				return delegator.vertexSet ();
			}
			public TV getEdgeSource (TE e) {
				return e.Source;
			}

			public TV getEdgeTarget (TE e) {
				return e.Target; 
			}
			public float getEdgeWeight (TE e) {
				return 1.0f;//Default e.Weight;
			}
			public void setEdgeWeight (TE e, float weight) {
				throw new NotSupportedException ("SetEdgeWeight not Supported");
			}
			public void setEdgeWeight (TV sourceVertex, TV targetVertex, float weight){
				throw new NotSupportedException ("SetEdgeWeight not Supported");
			}
			public float getEdgeWeight (TV sourceVertex, TV targetVertex){
				return 1.0f;//Default e.Weight;
			}
			// end interface implements
			public object Clone () {
				throw new NotSupportedException ("Clone: Not Supported Exception");
			}
			public bool isAllowingLoops () { return delegator.Container.AllowingLoops; }

			public bool isAllowingMultipleEdges () { return delegator.Container.AllowingMultipleEdges; }

			public override string ToString () { 
				IEnumerator<TV> vertexSet = this.vertexSet ().GetEnumerator();
				IEnumerator<TE> edgeSet = this.edgeSet ();
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				String ret = "";
				sb.Append ("Vertexs [");
				for(int i = 0; vertexSet.MoveNext();i++){
					sb.Append (vertexSet.Current.ToString ()+",");
				}
				sb.Remove (sb.Length-1, 1);
				sb.Append ("] \nedges ");
				for (int i=0; edgeSet.MoveNext(); i++) {
					sb.Append("(");
					sb.Append (edgeSet.Current.ToString());
					sb.Append(") ");
				}
				ret = sb.ToString ();
				sb.Clear ();
				return ret;
			}

			public void noSafeAdd(TE e){
				delegator.noSafeAdd (e);
			}
			public void noSafeAdd(TV source, TV target){
				delegator.noSafeAdd (source, target);
			}
			public void noSafeAdd(TV source, TV target, float weight){
				delegator.noSafeAdd (source, target, weight);
			}
			public void addEdgeToContainer (TE e) {
				delegator.addOutgoingEdge (e);
				delegator.addIncomingEdge (e);
			}
			public int inDegreeOf (TV vertex) {
				return delegator.incoming<TV> (vertex).Count;
			}
			public int outDegreeOf (TV vertex) {
				return delegator.outgoing<TV> (vertex).Count;
			}
			public ICollection<TE> incomingEdgesOf (TV vertex) {
				ICollection<TV> incoming_list = delegator.incoming<TV> (vertex);
				ICollection<TE> edges;
				if (!delegator.Container.AllowingMultipleEdges)
					edges = new HashSet<TE> ();
				else
					edges = new List<TE> ();
				foreach (TV v in incoming_list)
					edges.Add ((TE) delegator.Container.EdgeFactory.newInstance(v, vertex));
				return edges;
			}
			public ICollection<TE> outgoingEdgesOf (TV vertex) {
				ICollection<TV> outgoing_list = delegator.outgoing<TV> (vertex);
				ICollection<TE> edges;
				if (!delegator.Container.AllowingMultipleEdges)
					edges = new HashSet<TE> ();
				else
					edges = new List<TE> ();
				foreach (TV v in outgoing_list)
					edges.Add ((TE) delegator.Container.EdgeFactory.newInstance(vertex, v));
				return edges;
			}
			public void removeEdgeFromContainer (TE e) {
				delegator.removeOutgoingEdge (e);
				delegator.removeIncomingEdge (e);
			}
			public int countE() { return delegator.countE(); }
			public int countV() { return delegator.countV(); }
			public int degreeOf(TV vertex) { 
				return delegator.degreeOf (vertex);
			}
		}
		public interface IGraphHelperV<V, E, TV, TE>: IGraphHelper<V, E, TV, TE> 
			where V:IDVertexBasic where E:IDEdgeBasic<V> where TE: IDEdgeBasicInstance<V, TV> {
			IDataContainerVInstance<V, E, TV> Container { get; set; }
		}
		internal class IGraphHelperVImpl<V, E, TV, TE>: IGraphHelperV<V, E, TV, TE> 
			where V:IDVertexBasic 
			where E:IDEdgeBasic<V>
			where TE: IDEdgeBasicInstance<V, TV> {

			private int count_edges = 0;
			private IDataContainerVInstance<V, E, TV> container;

			public IGraphHelperVImpl (IDataContainerVInstance<V, E, TV> c) {
				container = c;
			}

			public IDataContainerVInstance<V, E, TV> Container{
				get{ return this.container; }
				set{ this.container = (IDataContainerVInstance<V, E, TV>)value; }
			}
			public IEnumerator<TE> edgeSet () {
				ICollection<TV> collection = container.DataSet.Keys;
				foreach (TV v in collection) {
					IEdgeContainer<TV> ec = container.DataSet[v];
					IEnumerator<TV> iterator = ec.incoming.GetEnumerator();
					while (iterator.MoveNext ()) {
						yield return (TE) container.EdgeFactory.newInstance (iterator.Current, v);
					}
				}
			}
			public ICollection<TE> edgesOf (TV vertex) {
				IEdgeContainer<TV> ec;
				ICollection<TE> edges;
				if (!container.AllowingMultipleEdges)
					edges = new HashSet<TE> ();
				else
					edges = new List<TE> ();
				if (container.DataSet.TryGetValue (vertex, out ec)) {
					foreach (TV v in ec.incoming)
						edges.Add ((TE)container.EdgeFactory.newInstance (v, vertex));
					foreach (TV v in ec.outgoing) {
						if (!vertex.Equals (v))
							edges.Add ((TE)container.EdgeFactory.newInstance (vertex, v));
					}
				}
				return edges;
			}
			public IEnumerator<TE> iteratorEdgesOf (TV vertex) {
				IEdgeContainer<TV> ec;
				if (container.DataSet.TryGetValue (vertex, out ec)) {
					foreach (TV v in ec.incoming)
						yield return ((TE)container.EdgeFactory.newInstance (v, vertex));
					foreach (TV v in ec.outgoing) {
						if (!vertex.Equals (v))
							yield return ((TE)container.EdgeFactory.newInstance (vertex, v));
					}
				}
			}
			public ICollection<TV> vertexSet () {
				return container.DataSet.Keys;
			}
			public void addIncomingEdge (TE e) {
				container.DataSet [e.Target].incoming.Add (e.Source); 
				count_edges++;
			}
			public void addOutgoingEdge (TE e) { 
				container.DataSet [e.Source].outgoing.Add (e.Target);
			}
			public void removeIncomingEdge (TE e) { 
				container.DataSet [e.Target].incoming.Remove (e.Source);
				count_edges--;
			}
			public void removeOutgoingEdge (TE e) { 
				container.DataSet [e.Source].outgoing.Remove (e.Target); 
			}
			public void noSafeAdd(TE e){
				addOutgoingEdge (e);
				addIncomingEdge (e);
			}
			public void noSafeAdd(TV source, TV target){
				container.DataSet [source].outgoing.Add (target);
				container.DataSet [target].incoming.Add (source); 
				count_edges++;
			}
			public void noSafeAdd(TV source, TV target, float weight){
				new  NotSupportedException ("Weight not Supported Exception!");
			}
			public ICollection<T> incoming<T>(TV vertex){
				IEdgeContainer<TV> ec;
				if (container.DataSet.TryGetValue (vertex, out ec))
					return ((List<T>)ec.incoming).AsReadOnly();
				return new List<T>().AsReadOnly();
			}
			public ICollection<T> outgoing<T>(TV vertex){
				IEdgeContainer<TV> ec;
				if (container.DataSet.TryGetValue (vertex, out ec))
					return ((List<T>)ec.outgoing).AsReadOnly();
				return new List<T>().AsReadOnly();
			}
			public bool addVertex (TV vertex) {
				if (container.DataSet.ContainsKey (vertex)) return false;
				else { 
					IEdgeContainer<TV> ec = new EdgeContainer<TV>(); ec.outgoing = new List<TV> (); ec.incoming = new List<TV> ();
					container.DataSet[vertex] = ec;
					return true; 
				}
			}
			public bool containsEdge(TE e){
				return container.DataSet [e.Source].outgoing.Contains (e.Target);
			}
			public bool containsVertex (TV v){ 
				return container.DataSet.ContainsKey (v); 
			}
			public bool removeVertex (TV v) { 
				return container.DataSet.Remove (v);
			}
			public int countE() { return count_edges; }
			public int countV() { return container.DataSet.Count; }
			public int degreeOf(TV v) { 
				IEdgeContainer<TV> ec; 
				if (container.DataSet.TryGetValue (v, out ec)) 
					return ec.outgoing.Count + ec.incoming.Count; 
				return 0; 
			}
		}
	}
}
