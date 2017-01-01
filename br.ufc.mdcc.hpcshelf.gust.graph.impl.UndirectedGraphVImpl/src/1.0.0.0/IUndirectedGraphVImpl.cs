using System;
using System.Collections.Generic;
using System.Linq;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.UndirectedGraph;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.UndirectedGraphVImpl
{
	public class IUndirectedGraphVImpl<CTN, V, E> : BaseIUndirectedGraphVImpl<CTN, V, E>, IUndirectedGraph<CTN, V, E>
where CTN:IDataContainerV<V, E>
where V:IVertex
where E:IEdge<V> {

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
		public IUndirectedGraphInstance<V, E, TV, TE> newInstanceT<TV, TE> (TE e, int size)  where TE: IEdgeInstance<V, TV> {
			IDataContainerVInstance<V, E, TV, TE> dc = DataContainer.InstanceTFactory<TV, TE>(e);
			dc.newDataSet (size);
			IGraphHelperV<V, E, TV, TE> h = new IGraphHelperVImpl<V, E, TV, TE>(dc);
			this.graphInstanceT = new GraphInstanceImpl<V, E, TV, TE> (h);
			return (IUndirectedGraphInstance<V, E, TV, TE>) this.graphInstanceT;
		}
		public IUndirectedGraphInstance<V, E, int, IEdgeInstance<V, int>> newInstance(int size) {
			IDataContainerVInstance<V, E, int, IEdgeInstance<V, int>> dc = DataContainer.DataContainerVInstance;
			dc.newDataSet (size);
			IGraphHelperV<V, E, int, IEdgeInstance<V, int>> h = new IGraphHelperVImpl<V, E, int, IEdgeInstance<V, int>>(dc);
			this.graphInstanceT = new GraphInstanceImpl<V, E, int, IEdgeInstance<V, int>> (h);
			return (IUndirectedGraphInstance<V, E, int, IEdgeInstance<V, int>>) this.graphInstanceT;
		}
		public class GraphInstanceImpl<V, E, TV, TE>: IUndirectedGraphInstance<V, E, TV, TE> 
			where V:IVertex  
			where E:IEdge<V> 
			where TE: IEdgeInstance<V, TV> {

			public IGraphHelperV<V, E, TV, TE> delegator;

			public IDataContainerInstance<V, E> DataContainer{
				get{ return this.delegator.Container; }
				set{ this.delegator.Container = (IDataContainerVInstance<V, E, TV, TE>)value; }
			}

			public GraphInstanceImpl(IGraphHelperV<V, E, TV, TE> d){
				delegator = d;
			}

			public object ObjValue {
				get { return new Tuple<IGraphHelperV<V, E, TV, TE>>(delegator); }
				set { 
					this.delegator = ((Tuple<IGraphHelperV<V, E, TV, TE>>)value).Item1;
				}
			}

			//************** implementation ***********************
			public ICollection<TE> getAllEdges (TV sourceVertex, TV targetVertex) {
				ICollection<TE> edges = null;

				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {
					edges = new List<TE>();

					IEnumerator<TE> iter = iteratorEdgesOf (sourceVertex);//.GetEnumerator ();

					while (iter.MoveNext ()) {
						TE e = iter.Current;

						bool equal = isEqualsStraightOrInverted (sourceVertex, targetVertex, e);

						if (equal) {
							edges.Add (e);
						}
					}
				}
				return edges;
			}
			public TE getEdge (TV sourceVertex, TV targetVertex) {
				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {
					IEnumerator<TE> iter = iteratorEdgesOf (sourceVertex);//.GetEnumerator ();

					while (iter.MoveNext ()) {
						TE e = iter.Current;

						bool equal = isEqualsStraightOrInverted (sourceVertex, targetVertex, e);

						if (equal) {
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
					if (!delegator.Container.AllowingLoops && sourceVertex.Equals (targetVertex)) {
						return default(TE);//throw new ArgumentException (LOOPS_NOT_ALLOWED);
					}
					TE e = (TE) delegator.Container.EdgeFactory.newInstance (sourceVertex, targetVertex);
					addEdgeToContainer(e);
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
						return false; //throw new ArgumentException (LOOPS_NOT_ALLOWED);
					}
					addEdgeToContainer(e);
					return true;
				} else {
					return false;
				}
			}
			public bool addVertex (TV v) {
				return delegator.addVertex (v);
			}
			public bool containsEdge(TV sourceVertex, TV targetVertex){
				return delegator.outgoing<TV> (sourceVertex).Contains (targetVertex) || delegator.outgoing<TV> (targetVertex).Contains (sourceVertex); //getEdge(sourceVertex, targetVertex) != null;
			}
			public bool containsEdge(TE e){
				if (e != null) {
					if (containsVertex (e.Source) && containsVertex (e.Target))
						return delegator.containsEdge (e);
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
					removeEdgeFromContainer (e); //container.removeEdge (e);
				}
				return e;
			}
			public bool removeEdge (TE e) {
				if (containsEdge (e)) {
					removeEdgeFromContainer (e); //container.removeEdge (e);
					return true;
				} 
				else { return false; }
			}
			public bool removeVertex (TV v) {
				if (containsVertex (v)) {
					ICollection<TE> edges = edgesOf (v);
					foreach (TE e in edges) {
						removeEdgeFromContainer (e); //container.removeEdge (e);
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
				throw new NotSupportedException ("SetEdgeWeight not Supported");
			}
			public float getEdgeWeight (TV sourceVertex, TV targetVertex){
				return delegator.Container.EdgeFactory.newInstance (sourceVertex, targetVertex).Weight;
			}
			// End interface implements

			public bool isAllowingLoops () { return delegator.Container.AllowingLoops; }
			public bool isAllowingMultipleEdges () { return delegator.Container.AllowingMultipleEdges; }

			#region ICloneable implementation
			public object Clone () {
				IGraphHelperV<V, E, TV, TE> d = (IGraphHelperV<V, E, TV, TE>) this.delegator.Clone ();
				GraphInstanceImpl<V, E, TV, TE> clone = new GraphInstanceImpl<V, E, TV, TE>(d);
				return clone;
			}
			#endregion

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
			public void removeEdgeFromContainer (TE e) {
				delegator.removeOutgoingEdge (e);
				delegator.removeIncomingEdge (e);
			}
			private bool isEqualsStraightOrInverted (Object sourceVertex, Object targetVertex, TE e) {
				bool equalStraight = sourceVertex.Equals (e.Source) && targetVertex.Equals (e.Target);
				bool equalInverted = sourceVertex.Equals (e.Target) && targetVertex.Equals (e.Source);
				return equalStraight || equalInverted;
			}
			public int degreeOf (TV vertex) {
				if (delegator.Container.AllowingLoops) {

					int degree = 0;
					ICollection<TE> edges = edgesOf (vertex);

					foreach (TE e in edges) {
						if (e.Source.Equals (e.Target)) {
							degree += 2;
						} else {
							degree += 1;
						}
					}
					return degree;
				} else {
					return delegator.degreeOf (vertex);
				}
			}
			public int countE() { return delegator.countE(); }
			public int countV() { return delegator.countV(); }
		}
		public interface IGraphHelperV<V, E, TV, TE>: IGraphHelper<V, E, TV, TE> 
			where V:IVertex where E:IEdge<V> where TE: IEdgeInstance<V, TV> {
			IDataContainerVInstance<V, E, TV, TE> Container { get; set; }
		}
		internal class IGraphHelperVImpl<V, E, TV, TE>: IGraphHelperV<V, E, TV, TE> 
			where V:IVertex 
			where E:IEdge<V>
			where TE: IEdgeInstance<V, TV> {

			private int count_edges = 0;
			private IDataContainerVInstance<V, E, TV, TE> container;

			public IGraphHelperVImpl (IDataContainerVInstance<V, E, TV, TE> c) {
				container = c;
			}

			public IDataContainerVInstance<V, E, TV, TE> Container{
				get{ return this.container; }
				set{ this.container = (IDataContainerVInstance<V, E, TV, TE>)value; }
			}
			public IDataContainerInstance<V, E> DataContainer{
				get{ return this.container; }
				set{ this.container = (IDataContainerVInstance<V, E, TV, TE>)value; }
			}

			#region ICloneable implementation
			public object Clone () {
				IDataContainerVInstance<V, E, TV, TE> c = (IDataContainerVInstance<V, E, TV, TE>) this.Container.Clone ();
				IGraphHelperVImpl<V, E, TV, TE> clone = new IGraphHelperVImpl<V, E, TV, TE> (c);
				clone.count_edges = this.count_edges;
				return clone;
			}
			#endregion

			// ************************* implements ***********************************
			public IEnumerator<TE> edgeSet () {
				ICollection<TV> collection = container.DataSet.Keys;
				foreach (TV v in collection) {
					IEdgeContainer<TV> ec = container.DataSet[v];
					IEnumerator<TV> iterator = ec.outgoing.GetEnumerator();
					while (iterator.MoveNext ()) {
						yield return (TE) container.EdgeFactory.newInstance (v, iterator.Current);
					}
				}
			}
			public ICollection<TE> edgesOf (TV vertex) {
				IEdgeContainer<TV> ec;
				ICollection<TE> edges;// = new HashSet<E> ();
				if (!container.AllowingMultipleEdges)
					edges = new HashSet<TE> ();
				else
					edges = new List<TE> ();
				if (container.DataSet.TryGetValue (vertex, out ec)) {
					foreach (TV v in ec.outgoing) {
						edges.Add ((TE)container.EdgeFactory.newInstance (vertex, v));
					}
					foreach (TV v in ec.incoming)
						if(!vertex.Equals(v)) edges.Add ((TE)container.EdgeFactory.newInstance (v, vertex));
				}
				return edges;
			}
			public IEnumerator<TE> iteratorEdgesOf (TV vertex) {
				IEdgeContainer<TV> ec;
				if (container.DataSet.TryGetValue (vertex, out ec)) {
					foreach (TV v in ec.outgoing) {
						yield return ((TE)container.EdgeFactory.newInstance (vertex, v));
					}
					foreach (TV v in ec.incoming)
						if(!vertex.Equals(v)) yield return ((TE)container.EdgeFactory.newInstance (v, vertex));
				}
			}
			public ICollection<TV> vertexSet () {
				return container.DataSet.Keys;
			}

			public void addIncomingEdge (TE e) {//if (container.DataSet.ContainsKey (e.target))
				if (!e.Source.Equals (e.Target)) { 
					container.DataSet [e.Target].incoming.Add (e.Source);
				}
			}
			public void addOutgoingEdge (TE e) { //if (container.DataSet.ContainsKey (e.source))
				container.DataSet [e.Source].outgoing.Add (e.Target);
				count_edges++;
			}
			public void removeIncomingEdge (TE e) { //if (container.DataSet.ContainsKey (e.target))
				container.DataSet [e.Target].incoming.Remove (e.Source);
				count_edges--;
			}
			public void removeOutgoingEdge (TE e) { //if (container.DataSet.ContainsKey (e.source))
				container.DataSet [e.Source].outgoing.Remove (e.Target); 
			}
			public void noSafeAdd(TE e){
				if (!containsEdge(e)) {
					container.DataSet [e.Source].outgoing.Add (e.Target);
					if (!e.Source.Equals (e.Target)) { 
						container.DataSet [e.Target].incoming.Add (e.Source);
					}
					count_edges++;
				}
			}
			public void noSafeAdd(TV source, TV target){
				bool b = container.DataSet [source].outgoing.Contains (target) || container.DataSet [target].outgoing.Contains (source);
				if (!b) {
					container.DataSet [source].outgoing.Add (target);
					if (!source.Equals (target)) { 
						container.DataSet [target].incoming.Add (source);
					}
					count_edges++;
				}
			}
			public void noSafeAdd(TV source, TV target, float weight){
				new NotSupportedException ("Weight not Supported Exception!");
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
					IEdgeContainer<TV> ec = new EdgeContainer<TV>(); 
					ec.outgoing = new List<TV> ();
					ec.incoming = new List<TV> ();
					container.DataSet[vertex] = ec;
					return true; 
				}
			}
			public bool containsEdge(TE e){
				return container.DataSet [e.Source].outgoing.Contains (e.Target) || container.DataSet [e.Target].outgoing.Contains (e.Source);
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
