using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeWeighted;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV;

using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerKVImpl {
	public class IDataContainerKVImpl<V, E> : BaseIDataContainerKVImpl<V, E>, IDataContainerKV<V, E> 
		where V:IDVertexBasic 
		where E:IDEdgeWeighted<V> {
		public IDataContainerKVImpl(){ }
		override public void after_initialize () { 
			newInstance (); 
		}
		public object newInstance () {
			IDVertexBasicInstance v = (IDVertexBasicInstance) this.Vertex.newInstance ();
			IDEdgeWeightedInstance<V, int> e = (IDEdgeWeightedInstance<V, int>)this.EdgeFactory.newInstance ();
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
			IDEdgeWeightedInstance<V, T> e = (IDEdgeWeightedInstance<V, T>)this.EdgeFactory.InstanceTFactory<T>(i,j,w);
			IDataContainerKVInstance<V, E, T> instanceT = new IDataContainerKVInstanceImpl<V, E, T> (e.Source, e, Rank); 
			return instanceT;
		}
	}

	[Serializable]
	public class IDataContainerKVInstanceImpl<V, E, RV> : IDataContainerKVInstance<V, E, RV> 
		where V: IDVertexBasic 
		where E:IDEdgeWeighted<V> {

		public IDataContainerKVInstanceImpl(){}
		public IDataContainerKVInstanceImpl(RV v, IDEdgeWeightedInstance<V, RV> e, int part){
			this.vertex = v;
			this.edgeFactory = e;
			rankPartition = part;
		}
		#region ICloneable implementation
		public object Clone () {
			IDataContainerKVInstanceImpl<V, E, RV> clone = new IDataContainerKVInstanceImpl<V, E, RV> ();
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [2]))
				clone.Vertex = (RV)((ICloneable)vertex).Clone ();
			else
				clone.Vertex = vertex;
			clone.EdgeFactory = (IDEdgeWeightedInstance<V, RV>)edgeFactory.Clone ();
			clone.RankPartition = rankPartition;
			clone.AllowingLoops = _allowingLoops;
			clone.AllowingMultipleEdges = _allowingMultipleEdges;
			clone.DataSet =  new Dictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>> (dataSet);
			return (IDataContainerKVInstance<V, E, RV>) clone;
		}
		#endregion

		#region IDataContainerKVInstance implementation
		private RV vertex;
		private IDEdgeWeightedInstance<V, RV> edgeFactory;
		private int rankPartition = 0;
		private bool _allowingLoops = true;
		private bool _allowingMultipleEdges = false; 
		private IDictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>> dataSet; 

		public RV Vertex { get { return vertex; } set { this.vertex = (RV)value; } }
		public IDEdgeWeightedInstance<V, RV> EdgeFactory { get { return edgeFactory; } set { this.edgeFactory = (IDEdgeWeightedInstance<V, RV>)value; } }
		public int RankPartition { get { return rankPartition; } set { this.rankPartition = value; } }
		public bool AllowingLoops{ get { return _allowingLoops; } set{ _allowingLoops = value; } }
		public bool AllowingMultipleEdges{ get { return _allowingMultipleEdges; } set{ _allowingMultipleEdges = value; } }
		public IDictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>> DataSet { 
			get { if (dataSet == null) dataSet = new Dictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>> (); return dataSet; } 
			set { dataSet = (IDictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>>)value; }
		}

		public object ObjValue {
			get { return new Tuple<RV,IDEdgeWeightedInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>>>(vertex,edgeFactory,rankPartition,_allowingLoops,_allowingMultipleEdges,dataSet); }
			set { 
				this.vertex =                 ((Tuple<RV,IDEdgeWeightedInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>>>)value).Item1;
				this.edgeFactory =            ((Tuple<RV,IDEdgeWeightedInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>>>)value).Item2;
				this.rankPartition =          ((Tuple<RV,IDEdgeWeightedInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>>>)value).Item3;
				this._allowingLoops =         ((Tuple<RV,IDEdgeWeightedInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>>>)value).Item4;
				this._allowingMultipleEdges = ((Tuple<RV,IDEdgeWeightedInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>>>)value).Item5;
				this.dataSet =            ((Tuple<RV,IDEdgeWeightedInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>>>)value).Item6;

			}
		}
		public override bool Equals (object obj) {
			if (typeof(IDataContainerKVInstance<V, E, RV>).IsAssignableFrom (obj.GetType ())) {
				IDataContainerKVInstance<V, E, RV> o = (IDataContainerKVInstance<V, E, RV>)obj;
				if (o.RankPartition == this.RankPartition)
					return true;
			}
			return false;
		}
		public override int GetHashCode () { return rankPartition; }

		public void newDataSet (int size) {
			dataSet = new Dictionary<RV, IEdgeContainer<KeyValuePair<RV, float>>> (size);
		}
		#endregion
	}
}
