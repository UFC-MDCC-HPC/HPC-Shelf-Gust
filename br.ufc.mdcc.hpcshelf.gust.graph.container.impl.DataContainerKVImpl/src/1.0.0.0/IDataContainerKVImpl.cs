using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.EdgeWeighted;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV;

using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerKVImpl {
	public class IDataContainerKVImpl<V, E> : BaseIDataContainerKVImpl<V, E>, IDataContainerKV<V, E> 
		where V:IVertexBasic 
		where E:IEdgeWeighted<V> {
		public IDataContainerKVImpl(){ }
		override public void after_initialize () { 
			newInstance (); 
		}
		public object newInstance () {
			IVertexBasicInstance v = (IVertexBasicInstance) this.Vertex.newInstance ();
			IEdgeWeightedInstance<V, int> e = (IEdgeWeightedInstance<V, int>)this.EdgeFactory.newInstance ();
			instance = new IDataContainerKVInstanceImpl<V, E, int> (v.Id, e, Rank);
			return instance;
		}
		private object instance;
		public object Instance {
			get { return instance; }
			set { 
				IDataContainerInstance<V, E> dc = (IDataContainerInstance<V, E>)value;
				dc.RankPartition = Rank;
				this.instance = dc;
			}
		}
		public IDataContainerKVInstance<V, E, int> DataContainerKVInstance {
			get { return (IDataContainerKVInstance<V, E, int>)instance; }
			set { 
				IDataContainerKVInstance<V, E, int> dc = (IDataContainerKVInstance<V, E, int>)value;
				dc.RankPartition = Rank;
				this.instance = dc;
			}
		}
		public IDataContainerKVInstance<V, E, T> InstanceTFactory<T> (T i, T j, float w) {
			IEdgeWeightedInstance<V, T> e = (IEdgeWeightedInstance<V, T>)this.EdgeFactory.InstanceTFactory<T>(i,j,w);
			IDataContainerKVInstance<V, E, T> instanceT = new IDataContainerKVInstanceImpl<V, E, T> (e.Source, e, Rank); 
			return instanceT;
		}
	}

	[Serializable]
	public class IDataContainerKVInstanceImpl<V, E, TV> : IDataContainerKVInstance<V, E, TV> 
		where V: IVertexBasic 
		where E:IEdgeWeighted<V> {

		public IDataContainerKVInstanceImpl(){}
		public IDataContainerKVInstanceImpl(TV v, IEdgeWeightedInstance<V, TV> e, int part){
			this.vertex = v;
			this.edgeFactory = e;
			rankPartition = part;
		}
		#region ICloneable implementation
		public object Clone () {
			IDataContainerKVInstanceImpl<V, E, TV> clone = new IDataContainerKVInstanceImpl<V, E, TV> ();
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [2]))
				clone.Vertex = (TV)((ICloneable)vertex).Clone ();
			else
				clone.Vertex = vertex;
			clone.EdgeFactory = (IEdgeWeightedInstance<V, TV>)edgeFactory.Clone ();
			clone.RankPartition = rankPartition;
			clone.AllowingLoops = _allowingLoops;
			clone.AllowingMultipleEdges = _allowingMultipleEdges;
			clone.DataSet =  new Dictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> (dataSet);
			return (IDataContainerKVInstance<V, E, TV>) clone;
		}
		#endregion

		#region IDataContainerKVInstance implementation
		private TV vertex;
		private IEdgeWeightedInstance<V, TV> edgeFactory;
		private int rankPartition = 0;
		private bool _allowingLoops = true;
		private bool _allowingMultipleEdges = false; 
		private IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> dataSet; 

		public TV Vertex { get { return vertex; } set { this.vertex = (TV)value; } }
		public IEdgeWeightedInstance<V, TV> EdgeFactory { get { return edgeFactory; } set { this.edgeFactory = (IEdgeWeightedInstance<V, TV>)value; } }
		public int RankPartition { get { return rankPartition; } set { this.rankPartition = value; } }
		public bool AllowingLoops{ get { return _allowingLoops; } set{ _allowingLoops = value; } }
		public bool AllowingMultipleEdges{ get { return _allowingMultipleEdges; } set{ _allowingMultipleEdges = value; } }
		public IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> DataSet { 
			get { if (dataSet == null) dataSet = new Dictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> (); return dataSet; } 
			set { dataSet = (IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>)value; }
		}

		public object ObjValue {
			get { return new Tuple<TV,IEdgeWeightedInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>(vertex,edgeFactory,rankPartition,_allowingLoops,_allowingMultipleEdges,dataSet); }
			set { 
				this.vertex =                 ((Tuple<TV,IEdgeWeightedInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item1;
				this.edgeFactory =            ((Tuple<TV,IEdgeWeightedInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item2;
				this.rankPartition =          ((Tuple<TV,IEdgeWeightedInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item3;
				this._allowingLoops =         ((Tuple<TV,IEdgeWeightedInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item4;
				this._allowingMultipleEdges = ((Tuple<TV,IEdgeWeightedInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item5;
				this.dataSet =            ((Tuple<TV,IEdgeWeightedInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item6;

			}
		}
		public override bool Equals (object obj) {
			if (typeof(IDataContainerKVInstance<V, E, TV>).IsAssignableFrom (obj.GetType ())) {
				IDataContainerKVInstance<V, E, TV> o = (IDataContainerKVInstance<V, E, TV>)obj;
				if (o.RankPartition == this.RankPartition)
					return true;
			}
			return false;
		}
		public override int GetHashCode () { return rankPartition; }

		public void newDataSet (int size) {
			dataSet = new Dictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> (size);
		}
		#endregion
	}
}
