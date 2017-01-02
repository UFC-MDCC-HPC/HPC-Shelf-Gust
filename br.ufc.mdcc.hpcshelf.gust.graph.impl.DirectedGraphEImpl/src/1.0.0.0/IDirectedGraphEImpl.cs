using System;
using System.Collections.Generic;
using System.Linq;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DirectedGraphEImpl
{
	public class IDirectedGraphEImpl<CTN, V, E> : BaseIDirectedGraphEImpl<CTN, V, E>, IDirectedGraph<CTN, V, E>
where CTN:IDataContainerE<V, E>
where V:IVertex
where E:IEdge<V>
	{
		override public void after_initialize()
		{
			newInstance(); 
		}

		public object newInstance () {
			return newInstance (0);
		}

		public object Instance {
			get { return graphInstanceT; }
			set { this.graphInstanceT = value; }
		}

		private object graphInstanceT = null;
		public object GraphInstanceT { 
			get{ 
				return this.graphInstanceT;
			}
		}
		public IDirectedGraphInstance<V, E, TV, TE> newInstanceT<TV, TE> (TE e, int size)  where TE: IEdgeInstance<V, TV> {
			IDataContainerEInstance<V, E, TV, TE> dc = DataContainer.InstanceTFactory<TV, TE> (e);//e.Source, e.Target, 1.0f);
			dc.newDataSet (size);
			IGraphHelperE<V, E, TV, TE> h = new IGraphHelperEImpl<V, E, TV, TE>(dc);
			this.graphInstanceT = new IDirectedGraphEInstanceImpl<V, E, TV, TE> (h);
			return (IDirectedGraphInstance<V, E, TV, TE>) this.graphInstanceT;
		}
		public IDirectedGraphInstance<V, E, int, IEdgeInstance<V, int>> newInstance(int size) {
			IDataContainerEInstance<V, E, int, IEdgeInstance<V, int>> dc = DataContainer.DataContainerEInstance;//e.Source, e.Target, 1.0f);
			dc.newDataSet (size);
			IGraphHelperE<V, E, int, IEdgeInstance<V, int>> h = new IGraphHelperEImpl<V, E, int, IEdgeInstance<V, int>>(dc);
			this.graphInstanceT = new IDirectedGraphEInstanceImpl<V, E, int, IEdgeInstance<V, int>> (h);
			return (IDirectedGraphInstance<V, E, int, IEdgeInstance<V, int>>) this.graphInstanceT;
		}

		public class IDirectedGraphEInstanceImpl<V, E, TV, TE>: IDirectedGraphInstance<V, E, TV, TE> 
			where V:IVertex  
			where E:IEdge<V> 
			where TE: IEdgeInstance<V, TV> {

			public IGraphHelperE<V, E, TV, TE> delegator;

			public IDataContainerInstance<V, E> DataContainer{
				get{ return this.delegator.Container; }
				set{ this.delegator.Container = (IDataContainerEInstance<V, E, TV, TE>)value; }
			}

			public IDirectedGraphEInstanceImpl(IGraphHelperE<V, E, TV, TE> d){
				delegator = d;
			}

			public object ObjValue {
				get { return new Tuple<IGraphHelperE<V, E, TV, TE>>(delegator); }
				set { 
					this.delegator = ((Tuple<IGraphHelperE<V, E, TV, TE>>)value).Item1;
				}
			}

			public ICollection<TE> getAllEdges (TV sourceVertex, TV targetVertex) {
				ICollection<TE> edges = null;

				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {
					edges = new List<TE> ();

					IEnumerator<TE> iter = delegator.outgoing<TE> (sourceVertex).GetEnumerator();
					while (iter.MoveNext ()) {

						if (iter.Current.Target.Equals (targetVertex)) {
							TE e = iter.Current;
							edges.Add (e);
						}
					}
				}

				return edges;
			}
			public TE getEdge (TV sourceVertex, TV targetVertex) {
				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {

					IEnumerator<TE> iter = delegator.outgoing<TE> (sourceVertex).GetEnumerator();
					while (iter.MoveNext ()) {

						if (iter.Current.Target.Equals (targetVertex)) {
							TE e = iter.Current;
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
				return getEdge(sourceVertex, targetVertex) != null;
			}
			public bool containsEdge(TE e){
				if (e != null) {
					if (containsVertex (e.Source) && containsVertex (e.Target))
						return getEdge(e.Source, e.Target) != null;//container.outgoing (e.source).Contains (e.target);
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
				ICollection<TE> i = delegator.incoming<TE> (vertex);
				ICollection<TV> edges = new HashSet<TV> ();
				foreach (TE e in i)
					edges.Add (e.Source);
				ICollection<TE> o = delegator.outgoing<TE> (vertex);
				foreach (TE e in o)
					edges.Add (e.Target);
				return edges;
			}
			public IEnumerator<TV> iteratorNeighborsOf (TV vertex){
				ICollection<TE> o = delegator.outgoing<TE> (vertex);
				ICollection<TE> i = delegator.incoming<TE> (vertex);
				foreach(TE e in o)
					yield return e.Target;
				foreach(TE e in i)
					yield return e.Source;
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
				return getEdgeWeight (e.Source, e.Target);
			}
			public void setAllEdgeWeight (TE e, float weight) {
				setAllEdgeWeight (e.Source, e.Target, weight);
			}
			public void setAllEdgeWeight (TV sourceVertex, TV targetVertex, float weight){
				if (delegator.Container.AllowingMultipleEdges) {
					ICollection<TE> list = this.getAllEdges (sourceVertex, targetVertex);
					foreach (TE ei in list)
						ei.Weight = weight;
				} else {
					TE ei = this.getEdge (sourceVertex, targetVertex);
					if(ei!=null)
						ei.Weight = weight;
				}
			}
			public float getEdgeWeight (TV sourceVertex, TV targetVertex){
				TE e = this.getEdge (sourceVertex, targetVertex);
				if(e!=null) 
					return e.Weight;
				return 0f;
			}
			// end interface implements

			#region ICloneable implementation
			public object Clone () {
				IGraphHelperE<V, E, TV, TE> d = (IGraphHelperE<V, E, TV, TE>) this.delegator.Clone ();
				IDirectedGraphEInstanceImpl<V, E, TV, TE> clone = new IDirectedGraphEInstanceImpl<V, E, TV, TE>(d);
				return clone;
			}
			#endregion

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
				return delegator.incoming<TE> (vertex).Count;
			}
			public int outDegreeOf (TV vertex) {
				return delegator.outgoing<TE> (vertex).Count;
			}
			public ICollection<TE> incomingEdgesOf (TV vertex) {
				return delegator.incoming<TE> (vertex);
			}
			public ICollection<TE> outgoingEdgesOf (TV vertex) {
				return delegator.outgoing<TE> (vertex);
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
		public interface IGraphHelperE<V, E, TV, TE>: IGraphHelper<V, E, TV, TE> 
			where V:IVertex where E:IEdge<V> where TE: IEdgeInstance<V, TV> {
			IDataContainerEInstance<V, E, TV, TE> Container { get; set; }
		}
		internal class IGraphHelperEImpl<V, E, TV, TE>: IGraphHelperE<V, E, TV, TE> 
			where V:IVertex 
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

			#region ICloneable implementation
			public object Clone () {
				IDataContainerEInstance<V, E, TV, TE> c = (IDataContainerEInstance<V, E, TV, TE>) this.Container.Clone ();
				IGraphHelperEImpl<V, E, TV, TE> clone = new IGraphHelperEImpl<V, E, TV, TE> (c);
				clone.count_edges = this.count_edges;
				return clone;
			}
			#endregion

			public IEnumerator<TE> edgeSet () {
				ICollection<TV> collection = container.DataSet.Keys;
				foreach (TV v in collection) {
					IEdgeContainer<TE> ec = container.DataSet[v];
					IEnumerator<TE> iterator = ec.incoming.GetEnumerator();
					while (iterator.MoveNext ()) {
						yield return iterator.Current;
					}
				}
			}
			public ICollection<TE> edgesOf (TV vertex) {
				IEdgeContainer<TE> ec;
				ICollection<TE> edges;// = new HashSet<E> ();
				if (!container.AllowingMultipleEdges)
					edges = new HashSet<TE> ();
				else
					edges = new List<TE> ();
				if (container.DataSet.TryGetValue (vertex, out ec)) {
					foreach (TE e in ec.incoming)
						edges.Add (e);
					foreach (TE e in ec.outgoing) {
						if (!vertex.Equals (e.Target))
							edges.Add (e);
					}
				}
				return edges;
			}
			public IEnumerator<TE> iteratorEdgesOf (TV vertex) {
				IEdgeContainer<TE> ec;
				if (container.DataSet.TryGetValue (vertex, out ec)) {
					foreach (TE e in ec.incoming)
						yield return (e);
					foreach (TE e in ec.outgoing) {
						if (!vertex.Equals (e.Target))
							yield return (e);
					}
				}
			}
			public ICollection<TV> vertexSet () {
				return container.DataSet.Keys;
			}
			public void addIncomingEdge (TE e) {//if (container.DataSet.ContainsKey (e.target))
				container.DataSet [e.Target].incoming.Add (e); 
				count_edges++;
			}
			public void addOutgoingEdge (TE e) { //if (container.DataSet.ContainsKey (e.source))
				container.DataSet [e.Source].outgoing.Add (e);
			}
			public void removeIncomingEdge (TE e) { //if (container.DataSet.ContainsKey (e.target))
				container.DataSet [e.Target].incoming.Remove (e);
				count_edges--;
			}
			public void removeOutgoingEdge (TE e) { //if (container.DataSet.ContainsKey (e.source))
				container.DataSet [e.Source].outgoing.Remove (e); 
			}
			public void noSafeAdd(TE e){
				addOutgoingEdge (e);
				addIncomingEdge (e);
			}
			public void noSafeAdd(TV source, TV target){
				TE e = (TE) container.EdgeFactory.newInstance (source, target);
				container.DataSet [source].outgoing.Add (e);
				container.DataSet [target].incoming.Add (e); 
				count_edges++;
			}
			public void noSafeAdd(TV source, TV target, float weight){
				TE e = (TE) container.EdgeFactory.newInstance (source, target, weight);
				container.DataSet [source].outgoing.Add (e);
				container.DataSet [target].incoming.Add (e); 
				count_edges++;
			}
			public ICollection<T> incoming<T>(TV vertex){
				IEdgeContainer<TE> ec;
				if (container.DataSet.TryGetValue (vertex, out ec))
					return ((List<T>)ec.incoming);//.AsReadOnly();
				return new List<T> ();//.AsReadOnly();
			}
			public ICollection<T> outgoing<T>(TV vertex){
				IEdgeContainer<TE> ec;
				if (container.DataSet.TryGetValue (vertex, out ec))
					return ((List<T>)ec.outgoing);//.AsReadOnly();
				return new List<T>();//.AsReadOnly();
			}
			public bool addVertex (TV vertex) {
				if (container.DataSet.ContainsKey (vertex)) return false;
				else { 
					IEdgeContainer<TE> ec = new EdgeContainer<TE>(); ec.outgoing = new List<TE> (); ec.incoming = new List<TE> ();
					container.DataSet[vertex] = ec;
					return true; 
				}
			}
			public bool containsEdge(TE e){
				return container.DataSet [e.Source].outgoing.Contains (e);
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
				IEdgeContainer<TE> ec; 
				if (container.DataSet.TryGetValue (v, out ec)) 
					return ec.outgoing.Count + ec.incoming.Count; 
				return 0; 
			}
		}
	}
}
