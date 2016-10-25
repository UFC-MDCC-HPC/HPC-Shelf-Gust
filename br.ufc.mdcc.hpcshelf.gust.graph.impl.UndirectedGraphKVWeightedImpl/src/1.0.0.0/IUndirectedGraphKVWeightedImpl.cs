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
		public class InstanceControlImpl<V, E, TV, TE>: IInstanceControlUndirected<V, E, TV, TE> 
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
			public ICollection<TE> getAllEdges (TV sourceVertex, TV targetVertex) {
				ICollection<TE> edges = null;

				if (containsVertex (sourceVertex) && containsVertex (targetVertex)) {
					edges = new List<TE>();

					IEnumerator<TE> iter = iteratorEdgesOf (sourceVertex);

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
					IEnumerator<TE> iter = iteratorEdgesOf (sourceVertex);

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
						return default(TE);
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
						return false;
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
				return getEdge(sourceVertex, targetVertex) != null;
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
			// End interface implements

			public bool isAllowingLoops () { return delegator.Container.AllowingLoops; }
			public bool isAllowingMultipleEdges () { return delegator.Container.AllowingMultipleEdges; }

			public object Clone () {
				throw new NotSupportedException ("Not Supported Exception");
			}
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
		public interface IGraphHelperKV<V, E, TV, TE>: IGraphHelper<V, E, TV, TE> 
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
			public IEnumerator<TE> edgeSet () {
				ICollection<TV> collection = container.DataSet.Keys;
				foreach (TV v in collection) {
					IEdgeContainer<KeyValuePair<TV, float>> ec = container.DataSet[v];
					IEnumerator<KeyValuePair<TV, float>> iterator = ec.outgoing.GetEnumerator();
					while (iterator.MoveNext ()) {
						yield return (TE) container.EdgeFactory.newInstance (v, iterator.Current.Key, iterator.Current.Value);
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
					foreach (KeyValuePair<TV, float> kv in ec.outgoing) {
						edges.Add ((TE) container.EdgeFactory.newInstance (vertex, kv.Key, kv.Value));
					}
					foreach (KeyValuePair<TV, float> kv in ec.incoming)
						if(!vertex.Equals(kv.Key)) edges.Add ((TE) container.EdgeFactory.newInstance (kv.Key, vertex, kv.Value));
				}
				return edges;
			}
			public IEnumerator<TE> iteratorEdgesOf (TV vertex) {
				IEdgeContainer<KeyValuePair<TV, float>> ec;
				if (container.DataSet.TryGetValue (vertex, out ec)) {
					foreach (KeyValuePair<TV, float> kv in ec.outgoing) {
						yield return ((TE) container.EdgeFactory.newInstance (vertex, kv.Key, kv.Value));
					}
					foreach (KeyValuePair<TV, float> kv in ec.incoming)
						if(!vertex.Equals(kv.Key)) yield return ((TE) container.EdgeFactory.newInstance (kv.Key, vertex, kv.Value));
				}
			}
			public ICollection<TV> vertexSet () {
				return container.DataSet.Keys;
			}

			public void addIncomingEdge (TE e) {//if (container.DataSet.ContainsKey (e.target))
				if (!e.Source.Equals (e.Target)) { 
					KeyValuePair<TV, float> kv = new KeyValuePair<TV, float>(e.Source, e.Weight);
					container.DataSet [e.Target].incoming.Add (kv);
				}
			}
			public void addOutgoingEdge (TE e) { //if (container.DataSet.ContainsKey (e.source))
				KeyValuePair<TV, float> kv = new KeyValuePair<TV, float>(e.Target, e.Weight);
				container.DataSet [e.Source].outgoing.Add (kv);
				count_edges++;
			}
			public void removeIncomingEdge (TE e) { //if (container.DataSet.ContainsKey (e.target))
				KeyValuePair<TV, float> kv = new KeyValuePair<TV, float>(e.Source, e.Weight);
				container.DataSet [e.Target].incoming.Remove (kv);
				count_edges--;
			}
			public void removeOutgoingEdge (TE e) { //if (container.DataSet.ContainsKey (e.source))
				KeyValuePair<TV, float> kv = new KeyValuePair<TV, float>(e.Target, e.Weight);
				container.DataSet [e.Source].outgoing.Remove (kv);
			}
			public void noSafeAdd(TE e){
				if (!containsEdge(e)) {
					KeyValuePair<TV, float> kv_ou = new KeyValuePair<TV, float>(e.Target, e.Weight);
					container.DataSet [e.Source].outgoing.Add (kv_ou);
					if (!e.Source.Equals (e.Target)) { 
						KeyValuePair<TV, float> kv_in = new KeyValuePair<TV, float>(e.Source, e.Weight);
						container.DataSet [e.Target].incoming.Add (kv_in);
					}
					count_edges++;
				}
			}
			public void noSafeAdd(TV source, TV target){
				KeyValuePair<TV, float> kv_ou = new KeyValuePair<TV, float>(target, 1.0f);
				KeyValuePair<TV, float> kv_in = new KeyValuePair<TV, float>(source, 1.0f);
				bool b = container.DataSet [source].outgoing.Contains (kv_ou) || container.DataSet [target].outgoing.Contains (kv_in);
				if (!b) {
					container.DataSet [source].outgoing.Add (kv_ou);
					if (!source.Equals (target)) { 
						container.DataSet [target].incoming.Add (kv_in);
					}
					count_edges++;
				}
			}
			public void noSafeAdd(TV source, TV target, float weight){
				KeyValuePair<TV, float> kv_ou = new KeyValuePair<TV, float>(target, weight);
				KeyValuePair<TV, float> kv_in = new KeyValuePair<TV, float>(source, weight);
				bool b = container.DataSet [source].outgoing.Contains (kv_ou) || container.DataSet [target].outgoing.Contains (kv_in);
				if (!b) {
					container.DataSet [source].outgoing.Add (kv_ou);
					if (!source.Equals (target)) { 
						container.DataSet [target].incoming.Add (kv_in);
					}
					count_edges++;
				}
			}
			public ICollection<T> incoming<T>(TV vertex){
				IEdgeContainer<KeyValuePair<TV, float>> ec;
				if (container.DataSet.TryGetValue (vertex, out ec))
					return ((List<T>)ec.incoming).AsReadOnly();
				return new List<T>().AsReadOnly();
			}
			public ICollection<T> outgoing<T>(TV vertex){
				IEdgeContainer<KeyValuePair<TV, float>> ec;
				if (container.DataSet.TryGetValue (vertex, out ec))
					return ((List<T>)ec.outgoing).AsReadOnly();
				return new List<T>().AsReadOnly();
			}
			public bool addVertex (TV vertex) {
				if (container.DataSet.ContainsKey (vertex)) return false;
				else { 
					IEdgeContainer<KeyValuePair<TV, float>> ec = new EdgeContainer<KeyValuePair<TV, float>>(); 
					ec.outgoing = new List<KeyValuePair<TV, float>> ();
					ec.incoming = new List<KeyValuePair<TV, float>> ();
					container.DataSet[vertex] = ec;
					return true; 
				}
			}
			public bool containsEdge(TE e){
				KeyValuePair<TV, float> kv_ou = new KeyValuePair<TV, float>(e.Target, e.Weight);
				KeyValuePair<TV, float> kv_in = new KeyValuePair<TV, float>(e.Source, e.Weight);
				return container.DataSet [e.Source].outgoing.Contains (kv_ou) || container.DataSet [e.Target].outgoing.Contains (kv_in);
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
