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
		public IGraphDelegate<V, E, T, IDEdgeBasicInstance<V, T>> InstanceTFactoryDelegate<T>(T i, T j){
			return new IGraphDelegateVImpl<V, E, T, IDEdgeBasicInstance<V, T>> (DataContainer.InstanceTFactory<T>(i, j));		
		}
		public IGraphDelegate<V, E, int, IDEdgeBasicInstance<V, int>> createDefaultDelegate(){
			return new IGraphDelegateVImpl<V, E, int, IDEdgeBasicInstance<V, int>> (DataContainer.DataContainerVInstance);	
		}

		public class DirectedGraphV<V, E, RV, RE> where V:IDVertexBasic  where E:IDEdgeBasic<V> where RE: IDEdgeBasicInstance<V, RV> {

			public IGraphDelegateV<V, E, RV, RE> delegator;

			public DirectedGraphV(IGraphDelegateV<V, E, RV, RE> d){
				delegator = d;
			}
			public ICollection<RE> getAllEdges (RV sourceVertex, RV targetVertex) {
				ICollection<RE> edges = null; 

				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {
					edges = new List<RE> ();

					IEnumerator<RV> iter = delegator.outgoing<RV> (sourceVertex).GetEnumerator();
					while (iter.MoveNext ()) {

						if (iter.Current.Equals (targetVertex)) {
							RE e = (RE) delegator.Container.EdgeFactory.newInstance (sourceVertex, iter.Current);
							edges.Add (e);
						}
					}
				}

				return edges;
			}
			public RE getEdge (RV sourceVertex, RV targetVertex) {
				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {

					IEnumerator<RV> iter = delegator.outgoing<RV> (sourceVertex).GetEnumerator();
					while (iter.MoveNext ()) {

						if (iter.Current.Equals (targetVertex)) {
							RE e = (RE) delegator.Container.EdgeFactory.newInstance (sourceVertex, iter.Current);
							return e;
						}
					}
				}

				return default(RE);
			}
			public RE addEdge (RV sourceVertex, RV targetVertex) {
				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {
					if (!delegator.Container.AllowingMultipleEdges && containsEdge (sourceVertex, targetVertex)) {
						return default(RE);
					}

					if (!delegator.Container.AllowingLoops && sourceVertex.Equals (targetVertex)) { throw new ArgumentException ("loops not allowed"); }

					RE e = (RE) delegator.Container.EdgeFactory.newInstance (sourceVertex, targetVertex);
					addEdgeToContainer (e);
					return e;
				}
				return default(RE);
			}
			public bool addEdge (RE e) {
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
			public bool addVertex (RV v) {
				return delegator.addVertex (v);
			}
			public bool containsEdge(RV sourceVertex, RV targetVertex){
				return delegator.outgoing<RV> (sourceVertex).Contains (targetVertex);
			}
			public bool containsEdge(RE e){
				if (e != null) {
					if (containsVertex (e.Source) && containsVertex (e.Target))
						return delegator.outgoing<RV> (e.Source).Contains (e.Target);
					else
						return false;
				}
				return false;
			}
			public bool containsVertex (RV v){
				if(v==null)
					throw new ArgumentNullException ("WARNING: Method containsVertex receive parameter null! ");
				return delegator.containsVertex (v);
			}
			public IEnumerator<RE> edgeSet () {
				return delegator.edgeSet ();
			}
			public ICollection<RE> edgesOf (RV vertex) {
				return delegator.edgesOf (vertex);
			}
			public IEnumerator<RE> iteratorEdgesOf (RV vertex) {
				return delegator.iteratorEdgesOf (vertex);
			}
			public ICollection<RV> neighborsOf (RV vertex){
				ICollection<RV> o = delegator.outgoing<RV> (vertex);
				ICollection<RV> i = delegator.incoming<RV> (vertex);
				return new HashSet<RV> (o.Union (i));
			}
			public IEnumerator<RV> iteratorNeighborsOf (RV vertex){
				ICollection<RV> o = delegator.outgoing<RV> (vertex);
				ICollection<RV> i = delegator.incoming<RV> (vertex);
				foreach(RV v in o)
					yield return v;
				foreach(RV v in i)
					yield return v;
			}
			public bool removeAllEdges(ICollection<RE> edges){
				bool modified = false;

				foreach (RE e in edges) {
					modified |= removeEdge(e);
				}

				return modified;
			}
			public ICollection<RE> removeAllEdges(RV sourceVertex, RV targetVertex){
				ICollection<RE> removed = getAllEdges(sourceVertex, targetVertex);
				if (removed == null) {
					return null;
				}
				removeAllEdges(removed);

				return removed;
			}
			public bool removeAllVertices(ICollection<RV> vertices){
				bool modified = false;

				foreach (RV v in vertices) {
					modified |= removeVertex(v);
				}

				return modified;
			}

			public RE removeEdge (RV sourceVertex, RV targetVertex) {
				RE e = getEdge (sourceVertex, targetVertex);

				if (e != null) {
					removeEdgeFromContainer (e);
				}
				return e;
			}
			public bool removeEdge (RE e) {
				if (containsEdge (e)) {
					removeEdgeFromContainer (e);
					return true;
				} 
				else { return false; }
			}
			public bool removeVertex (RV v) {
				if (containsVertex (v)) {
					ICollection<RE> edges = edgesOf (v);
					foreach (RE e in edges) {
						removeEdgeFromContainer (e);
					}
					return delegator.removeVertex (v);
				} else {
					return false;
				}
			}
			public ICollection<RV> vertexSet () {
				return delegator.vertexSet ();
			}
			public RV getEdgeSource (RE e) {
				return e.Source;
			}

			public RV getEdgeTarget (RE e) {
				return e.Target; 
			}
			public float getEdgeWeight (RE e) {
				return 1.0f;//Default e.Weight;
			}
			public void setEdgeWeight (RE e, float weight) {
				throw new NotSupportedException ("SetEdgeWeight not Supported");
			}
			public void setEdgeWeight (RV sourceVertex, RV targetVertex, float weight){
				throw new NotSupportedException ("SetEdgeWeight not Supported");
			}
			public float getEdgeWeight (RV sourceVertex, RV targetVertex){
				return 1.0f;//Default e.Weight;
			}
			// end interface implements
			public object Clone () {
				throw new NotSupportedException ("Clone: Not Supported Exception");
			}
			public bool isAllowingLoops () { return delegator.Container.AllowingLoops; }

			public bool isAllowingMultipleEdges () { return delegator.Container.AllowingMultipleEdges; }

			public override string ToString () { 
				IEnumerator<RV> vertexSet = this.vertexSet ().GetEnumerator();
				IEnumerator<RE> edgeSet = this.edgeSet ();
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

			public void noSafeAdd(RE e){
				delegator.noSafeAdd (e);
			}
			public void noSafeAdd(RV source, RV target){
				delegator.noSafeAdd (source, target);
			}
			public void noSafeAdd(RV source, RV target, float weight){
				delegator.noSafeAdd (source, target, weight);
			}
			public void addEdgeToContainer (RE e) {
				delegator.addOutgoingEdge (e);
				delegator.addIncomingEdge (e);
			}
			public int inDegreeOf (RV vertex) {
				return delegator.incoming<RV> (vertex).Count;
			}
			public int outDegreeOf (RV vertex) {
				return delegator.outgoing<RV> (vertex).Count;
			}
			public ICollection<RE> incomingEdgesOf (RV vertex) {
				ICollection<RV> incoming_list = delegator.incoming<RV> (vertex);
				ICollection<RE> edges;
				if (!delegator.Container.AllowingMultipleEdges)
					edges = new HashSet<RE> ();
				else
					edges = new List<RE> ();
				foreach (RV v in incoming_list)
					edges.Add ((RE) delegator.Container.EdgeFactory.newInstance(v, vertex));
				return edges;
			}
			public ICollection<RE> outgoingEdgesOf (RV vertex) {
				ICollection<RV> outgoing_list = delegator.outgoing<RV> (vertex);
				ICollection<RE> edges;
				if (!delegator.Container.AllowingMultipleEdges)
					edges = new HashSet<RE> ();
				else
					edges = new List<RE> ();
				foreach (RV v in outgoing_list)
					edges.Add ((RE) delegator.Container.EdgeFactory.newInstance(vertex, v));
				return edges;
			}
			public void removeEdgeFromContainer (RE e) {
				delegator.removeOutgoingEdge (e);
				delegator.removeIncomingEdge (e);
			}
			public int countE() { return delegator.countE(); }
			public int countV() { return delegator.countV(); }
			public int degreeOf(RV vertex) { 
				return delegator.degreeOf (vertex);
			}
		}
		public interface IGraphDelegateV<V, E, RV, RE>: IGraphDelegate<V, E, RV, RE> 
			where V:IDVertexBasic where E:IDEdgeBasic<V> where RE: IDEdgeBasicInstance<V, RV> {
			IDataContainerVInstance<V, E, RV> Container { get; set; }
		}
		internal class IGraphDelegateVImpl<V, E, RV, RE>: IGraphDelegateV<V, E, RV, RE> 
			where V:IDVertexBasic 
			where E:IDEdgeBasic<V>
			where RE: IDEdgeBasicInstance<V, RV> {

			private int count_edges = 0;
			private IDataContainerVInstance<V, E, RV> container;

			public IGraphDelegateVImpl (IDataContainerVInstance<V, E, RV> c) {
				container = c;
			}

			public IDataContainerVInstance<V, E, RV> Container{
				get{ return this.container; }
				set{ this.container = (IDataContainerVInstance<V, E, RV>)value; }
			}
			public IEnumerator<RE> edgeSet () {
				ICollection<RV> collection = container.DataSet.Keys;
				foreach (RV v in collection) {
					IEdgeContainer<RV> ec = container.DataSet[v];
					IEnumerator<RV> iterator = ec.incoming.GetEnumerator();
					while (iterator.MoveNext ()) {
						yield return (RE) container.EdgeFactory.newInstance (iterator.Current, v);
					}
				}
			}
			public ICollection<RE> edgesOf (RV vertex) {
				IEdgeContainer<RV> ec;
				ICollection<RE> edges;
				if (!container.AllowingMultipleEdges)
					edges = new HashSet<RE> ();
				else
					edges = new List<RE> ();
				if (container.DataSet.TryGetValue (vertex, out ec)) {
					foreach (RV v in ec.incoming)
						edges.Add ((RE)container.EdgeFactory.newInstance (v, vertex));
					foreach (RV v in ec.outgoing) {
						if (!vertex.Equals (v))
							edges.Add ((RE)container.EdgeFactory.newInstance (vertex, v));
					}
				}
				return edges;
			}
			public IEnumerator<RE> iteratorEdgesOf (RV vertex) {
				IEdgeContainer<RV> ec;
				if (container.DataSet.TryGetValue (vertex, out ec)) {
					foreach (RV v in ec.incoming)
						yield return ((RE)container.EdgeFactory.newInstance (v, vertex));
					foreach (RV v in ec.outgoing) {
						if (!vertex.Equals (v))
							yield return ((RE)container.EdgeFactory.newInstance (vertex, v));
					}
				}
			}
			public ICollection<RV> vertexSet () {
				return container.DataSet.Keys;
			}
			public void addIncomingEdge (RE e) {
				container.DataSet [e.Target].incoming.Add (e.Source); 
				count_edges++;
			}
			public void addOutgoingEdge (RE e) { 
				container.DataSet [e.Source].outgoing.Add (e.Target);
			}
			public void removeIncomingEdge (RE e) { 
				container.DataSet [e.Target].incoming.Remove (e.Source);
				count_edges--;
			}
			public void removeOutgoingEdge (RE e) { 
				container.DataSet [e.Source].outgoing.Remove (e.Target); 
			}
			public void noSafeAdd(RE e){
				addOutgoingEdge (e);
				addIncomingEdge (e);
			}
			public void noSafeAdd(RV source, RV target){
				container.DataSet [source].outgoing.Add (target);
				container.DataSet [target].incoming.Add (source); 
				count_edges++;
			}
			public void noSafeAdd(RV source, RV target, float weight){
				new  NotSupportedException ("Weight not Supported Exception!");
			}
			public ICollection<T> incoming<T>(RV vertex){
				IEdgeContainer<RV> ec;
				if (container.DataSet.TryGetValue (vertex, out ec))
					return ((List<T>)ec.incoming).AsReadOnly();
				return new List<T>().AsReadOnly();
			}
			public ICollection<T> outgoing<T>(RV vertex){
				IEdgeContainer<RV> ec;
				if (container.DataSet.TryGetValue (vertex, out ec))
					return ((List<T>)ec.outgoing).AsReadOnly();
				return new List<T>().AsReadOnly();
			}
			public bool addVertex (RV vertex) {
				if (container.DataSet.ContainsKey (vertex)) return false;
				else { 
					IEdgeContainer<RV> ec = new EdgeContainer<RV>(); ec.outgoing = new List<RV> (); ec.incoming = new List<RV> ();
					container.DataSet[vertex] = ec;
					return true; 
				}
			}
			public bool containsEdge(RE e){
				return container.DataSet [e.Source].outgoing.Contains (e.Target);
			}
			public bool containsVertex (RV v){ 
				return container.DataSet.ContainsKey (v); 
			}
			public bool removeVertex (RV v) { 
				return container.DataSet.Remove (v);
			}
			public int countE() { return count_edges; }
			public int countV() { return container.DataSet.Count; }
			public int degreeOf(RV v) { 
				IEdgeContainer<RV> ec; 
				if (container.DataSet.TryGetValue (v, out ec)) 
					return ec.outgoing.Count + ec.incoming.Count; 
				return 0; 
			}
		}
	}
}
