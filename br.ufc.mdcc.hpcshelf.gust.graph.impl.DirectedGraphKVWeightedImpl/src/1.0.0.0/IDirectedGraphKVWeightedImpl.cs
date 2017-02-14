using System;
using System.Collections.Generic;
using System.Linq;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.Graph;
using br.ufc.mdcc.hpcshelf.gust.graph.DirectedGraph;

namespace br.ufc.mdcc.hpcshelf.gust.graph.impl.DirectedGraphKVWeightedImpl
{
	public class IDirectedGraphKVWeightedImpl<CTN, V, E> : BaseIDirectedGraphKVWeightedImpl<CTN, V, E>, IDirectedGraph<CTN, V, E>
where CTN:IDataContainerKV<V, E>
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
			IDataContainerKVInstance<V, E, TV, TE> dc = DataContainer.InstanceTFactory<TV, TE> (e);
			dc.newDataSet (size);
			IGraphHelperKV<V, E, TV, TE> h = new IGraphHelperKVImpl<V, E, TV, TE>(dc);
			this.graphInstanceT = new IDirectedGraphKVWeightedInstanceImpl<V, E, TV, TE> (h);
			return (IDirectedGraphInstance<V, E, TV, TE>) this.graphInstanceT;
		}
		public IDirectedGraphInstance<V, E, int, IEdgeInstance<V, int>> newInstance(int size) {
			IDataContainerKVInstance<V, E, int, IEdgeInstance<V, int>> dc = DataContainer.DataContainerKVInstance;
			dc.newDataSet (size);
			IGraphHelperKV<V, E, int, IEdgeInstance<V, int>> h = new IGraphHelperKVImpl<V, E, int, IEdgeInstance<V, int>>(dc);
			this.graphInstanceT = new IDirectedGraphKVWeightedInstanceImpl<V, E, int, IEdgeInstance<V, int>> (h);
			return (IDirectedGraphInstance<V, E, int, IEdgeInstance<V, int>>) this.graphInstanceT;
		}

		public class IDirectedGraphKVWeightedInstanceImpl<V, E, TV, TE>: IDirectedGraphInstance<V, E, TV, TE> 
			where V:IVertex  
			where E:IEdge<V> 
			where TE: IEdgeInstance<V, TV> {

			public IGraphHelperKV<V, E, TV, TE> delegator;

			public IDataContainerInstance<V, E> DataContainer{
				get{ return this.delegator.Container; }
				set{ this.delegator.Container = (IDataContainerKVInstance<V, E, TV, TE>)value; }
			}

			public IDirectedGraphKVWeightedInstanceImpl(IGraphHelperKV<V, E, TV, TE> d){
				delegator = d;
			}

			public object ObjValue {
				get { return new Tuple<IGraphHelperKV<V, E, TV, TE>>(delegator); }
				set { 
					this.delegator = ((Tuple<IGraphHelperKV<V, E, TV, TE>>)value).Item1;
				}
			}

			public ICollection<TE> getAllEdges (TV sourceVertex, TV targetVertex) {
				ICollection<TE> edges = null;

				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {
					edges = new List<TE> ();

					IEnumerator<KeyValuePair<TV, float>> iter = delegator.outgoing<KeyValuePair<TV, float>> (sourceVertex).GetEnumerator();
					for(;iter.MoveNext();) {

						if (iter.Current.Key.Equals (targetVertex)) {
							TE e = (TE) delegator.Container.EdgeFactory.newInstance (sourceVertex, iter.Current.Key, iter.Current.Value);
							edges.Add (e);
						}
					}
				}

				return edges;
			}
			public TE getEdge (TV sourceVertex, TV targetVertex) {
				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {

					IEnumerator<KeyValuePair<TV, float>> iter = delegator.outgoing<KeyValuePair<TV, float>> (sourceVertex).GetEnumerator();
					for(;iter.MoveNext();) {
						if (iter.Current.Key.Equals (targetVertex)) {
							TE e = (TE) delegator.Container.EdgeFactory.newInstance (sourceVertex, iter.Current.Key, iter.Current.Value);
							return e;
						}
					}
				}

				return default(TE);
			}
			public TE getEdge (TE edge) {
				if (containsVertex (edge.Source) && containsVertex (edge.Target)) {

					IEnumerator<KeyValuePair<TV, float>> iter = delegator.outgoing<KeyValuePair<TV, float>> (edge.Source).GetEnumerator();
					for(;iter.MoveNext();) {
						if (iter.Current.Key.Equals (edge.Target)) {
							TE e = (TE) delegator.Container.EdgeFactory.newInstance (edge.Source, iter.Current.Key, iter.Current.Value);
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
						return getEdge(e) != null; //container.outgoing (e.source).ContainsKey (e.target);
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
				ICollection<KeyValuePair<TV, float>> i = delegator.incoming<KeyValuePair<TV, float>> (vertex);
				ICollection<TV> edges = new HashSet<TV> ();
				foreach (KeyValuePair<TV, float> kv in i)
					edges.Add (kv.Key);
				ICollection<KeyValuePair<TV, float>> o = delegator.outgoing<KeyValuePair<TV, float>> (vertex);
				foreach (KeyValuePair<TV, float> kv in o)
					edges.Add (kv.Key);
				return edges;
			}
			public IEnumerator<TV> iteratorNeighborsOf (TV vertex){
				ICollection<KeyValuePair<TV, float>> o = delegator.outgoing<KeyValuePair<TV, float>> (vertex);
				ICollection<KeyValuePair<TV, float>> i = delegator.incoming<KeyValuePair<TV, float>> (vertex);
				foreach (KeyValuePair<TV, float> kv in o)
					yield return kv.Key;
				foreach (KeyValuePair<TV, float> kv in i)
					yield return kv.Key;
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
					removeAllEdges (list);
					foreach (TE ei in list) {
						addEdgeToContainer ( (TE) delegator.Container.EdgeFactory.newInstance(sourceVertex, targetVertex, weight));
					}
				} else {
					bool b = removeEdge (sourceVertex, targetVertex)!=null;
					if (b) {
						addEdgeToContainer ( (TE) delegator.Container.EdgeFactory.newInstance(sourceVertex, targetVertex, weight));
					}
				}
			}
			public float getEdgeWeight (TV sourceVertex, TV targetVertex){
				TE e = this.getEdge (sourceVertex, targetVertex);
				if (e != null) {
					return e.Weight;
				}
				return 0f;
			}
			// end interface implements

			#region ICloneable implementation
			public object Clone () {
				IGraphHelperKV<V, E, TV, TE> d = (IGraphHelperKV<V, E, TV, TE>) this.delegator.Clone ();
				IDirectedGraphKVWeightedInstanceImpl<V, E, TV, TE> clone = new IDirectedGraphKVWeightedInstanceImpl<V, E, TV, TE>(d);
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
				return delegator.incoming<KeyValuePair<TV, float>> (vertex).Count;
			}
			public int outDegreeOf (TV vertex) {
				return delegator.outgoing<KeyValuePair<TV, float>> (vertex).Count;
			}
			public ICollection<TE> incomingEdgesOf (TV vertex) {
				ICollection<KeyValuePair<TV, float>> incoming_list = delegator.incoming<KeyValuePair<TV, float>> (vertex);
				ICollection<TE> edges;
				if (!delegator.Container.AllowingMultipleEdges)
					edges = new HashSet<TE> ();
				else
					edges = new List<TE> ();

				foreach (KeyValuePair<TV, float> kv in incoming_list)
					edges.Add ((TE) delegator.Container.EdgeFactory.newInstance(kv.Key, vertex, kv.Value));
				return edges;
			}
			public ICollection<TE> outgoingEdgesOf (TV vertex) {
				ICollection<KeyValuePair<TV, float>> outgoing_list = delegator.outgoing<KeyValuePair<TV, float>> (vertex);
				ICollection<TE> edges;
				if (!delegator.Container.AllowingMultipleEdges)
					edges = new HashSet<TE> ();
				else
					edges = new List<TE> ();

				foreach (KeyValuePair<TV, float> kv in outgoing_list)
					edges.Add ((TE) delegator.Container.EdgeFactory.newInstance(vertex, kv.Key, kv.Value));
				return edges;
			}
			//
			public ICollection<TV> outgoingVertexOf (TV vertex) {
				ICollection<KeyValuePair<TV, float>> outgoing_list = delegator.outgoing<KeyValuePair<TV, float>> (vertex);
				ICollection<TV> edges;
				if (!delegator.Container.AllowingMultipleEdges)
					edges = new HashSet<TV> ();
				else
					edges = new List<TV> ();
				foreach (KeyValuePair<TV, float> kv in outgoing_list)
					edges.Add (kv.Key);
				return edges;
			}
			public IEnumerator<TV> iteratorOutgoingVertexOf(TV vertex){
				ICollection<KeyValuePair<TV, float>> outgoing_list = delegator.outgoing<KeyValuePair<TV, float>> (vertex);
				ICollection<TV> edges;
				if (!delegator.Container.AllowingMultipleEdges) {
					edges = new HashSet<TV> ();
					foreach (KeyValuePair<TV, float> kv in outgoing_list) {
						bool contain = edges.Contains (kv.Key);
						if (!contain) {
							edges.Add (kv.Key);
							yield return kv.Key;
						}
					}
				}
				else
					foreach (KeyValuePair<TV, float> kv in outgoing_list)
						yield return kv.Key;
			}
			//
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
		public interface IGraphHelperKV<V, E, TV, TE>: IGraphHelper<V, E, TV, TE> 
			where V:IVertex where E:IEdge<V> where TE: IEdgeInstance<V, TV> {
			IDataContainerKVInstance<V, E, TV, TE> Container { get; set; }
		}
		internal class IGraphHelperKVImpl<V, E, TV, TE>: IGraphHelperKV<V, E, TV, TE> 
			where V:IVertex 
			where E:IEdge<V>
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

			#region ICloneable implementation
			public object Clone () {
				IDataContainerKVInstance<V, E, TV, TE> c = (IDataContainerKVInstance<V, E, TV, TE>) this.Container.Clone ();
				IGraphHelperKVImpl<V, E, TV, TE> clone = new IGraphHelperKVImpl<V, E, TV, TE> (c);
				clone.count_edges = this.count_edges;
				return clone;
			}
			#endregion

			public IEnumerator<TE> edgeSet () {
				ICollection<TV> collection = container.DataSet.Keys;
				foreach (TV v in collection) {
					IEdgeContainer<KeyValuePair<TV, float>> ec = container.DataSet[v];
					IEnumerator<KeyValuePair<TV, float>> iterator = ec.incoming.GetEnumerator();
					while (iterator.MoveNext ()) {
						TE edge = (TE)container.EdgeFactory.newInstance (iterator.Current.Key, v, iterator.Current.Value);
						yield return edge;
					}
				}
			}
			public ICollection<TE> edgesOf (TV vertex) {
				IEdgeContainer<KeyValuePair<TV, float>> ec;
				ICollection<TE> edges;
				if (!container.AllowingMultipleEdges)
					edges = new HashSet<TE> ();
				else
					edges = new List<TE> ();
				if (container.DataSet.TryGetValue (vertex, out ec)) {
					foreach (KeyValuePair<TV, float> kv in ec.incoming)
						edges.Add ((TE)container.EdgeFactory.newInstance(kv.Key, vertex, kv.Value));
					foreach (KeyValuePair<TV, float> kv in ec.outgoing) {
						if(!vertex.Equals(kv.Key)) edges.Add ((TE)container.EdgeFactory.newInstance (vertex, kv.Key, kv.Value));
					}
				}
				return edges;
			}
			public IEnumerator<TE> iteratorEdgesOf (TV vertex) {
				IEdgeContainer<KeyValuePair<TV, float>> ec;
				if (container.DataSet.TryGetValue (vertex, out ec)) {
					foreach (KeyValuePair<TV, float> kv in ec.incoming)
						yield return ((TE)container.EdgeFactory.newInstance(kv.Key, vertex, kv.Value));
					foreach (KeyValuePair<TV, float> kv in ec.outgoing) {
						if(!vertex.Equals(kv.Key)) yield return ((TE)container.EdgeFactory.newInstance (vertex, kv.Key, kv.Value));
					}
				}
			}
			public ICollection<TV> vertexSet () {
				return container.DataSet.Keys;
			}
			public void addIncomingEdge (TE e) {
				KeyValuePair<TV, float> kv = new KeyValuePair<TV, float>(e.Source, e.Weight);
				container.DataSet [e.Target].incoming.Add (kv);
				count_edges++;
			}
			public void addOutgoingEdge (TE e) {
				KeyValuePair<TV, float> kv = new KeyValuePair<TV, float>(e.Target, e.Weight);
				container.DataSet [e.Source].outgoing.Add (kv);
			}
			public void removeIncomingEdge (TE e) { 
				KeyValuePair<TV, float> kv = new KeyValuePair<TV, float>(e.Source, e.Weight);
				container.DataSet [e.Target].incoming.Remove (kv);
				count_edges--;
			}
			public void removeOutgoingEdge (TE e) {
				KeyValuePair<TV, float> kv = new KeyValuePair<TV, float>(e.Target, e.Weight);
				container.DataSet [e.Source].outgoing.Remove (kv); 
			}
			public void noSafeAdd(TE e){
				addOutgoingEdge (e);
				addIncomingEdge (e);
			}
			public void noSafeAdd(TV source, TV target){
				KeyValuePair<TV, float> kv1 = new KeyValuePair<TV, float>(target, 1.0f);
				container.DataSet [source].outgoing.Add (kv1);

				KeyValuePair<TV, float> kv2 = new KeyValuePair<TV, float>(source, 1.0f);
				container.DataSet [target].incoming.Add (kv2);
				count_edges++;
			}
			public void noSafeAdd(TV source, TV target, float weight){
				KeyValuePair<TV, float> kv1 = new KeyValuePair<TV, float>(target, weight);
				container.DataSet [source].outgoing.Add (kv1);

				KeyValuePair<TV, float> kv2 = new KeyValuePair<TV, float>(source, weight);
				container.DataSet [target].incoming.Add (kv2);
				count_edges++;
			}
			public ICollection<T> incoming<T>(TV vertex){
				IEdgeContainer<KeyValuePair<TV, float>> ec;
				if (container.DataSet.TryGetValue (vertex, out ec))
					return ((List<T>)ec.incoming).AsReadOnly();
				return new List<T>();
			}
			public ICollection<T> outgoing<T>(TV vertex){
				IEdgeContainer<KeyValuePair<TV, float>> ec;
				if (container.DataSet.TryGetValue (vertex, out ec))
					return ((List<T>)ec.outgoing).AsReadOnly();
				return new List<T> ();
			}
			public bool addVertex (TV vertex) {
				if (container.DataSet.ContainsKey (vertex)) return false;
				else { 
					IEdgeContainer<KeyValuePair<TV, float>> ec = new EdgeContainer<KeyValuePair<TV, float>>(); ec.outgoing = new List<KeyValuePair<TV, float>> (); ec.incoming = new List<KeyValuePair<TV, float>> ();
					container.DataSet[vertex] = ec;
					return true; 
				}
			}
			public bool containsEdge(TE e){
				KeyValuePair<TV, float> kv = new KeyValuePair<TV, float> (e.Target, e.Weight);
				return container.DataSet [e.Source].outgoing.Contains (kv);
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
				IEdgeContainer<KeyValuePair<TV, float>> ec; 
				if (container.DataSet.TryGetValue (v, out ec)) 
					return ec.outgoing.Count + ec.incoming.Count; 
				return 0; 
			}
		}
	}
}
