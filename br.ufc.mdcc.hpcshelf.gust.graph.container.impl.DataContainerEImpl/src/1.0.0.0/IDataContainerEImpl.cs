using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;//Basic;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;

using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerEImpl {
	public class IDataContainerEImpl<V, E> : BaseIDataContainerEImpl<V, E>, IDataContainerE<V, E> 
		where V:IDVertexBasic 
		where E:IDEdge<V> {
		public IDataContainerEImpl(){ }
		override public void after_initialize () { 
			newInstance (); 
		}
		public object newInstance () {
			IDVertexBasicInstance v = (IDVertexBasicInstance) this.Vertex.newInstance ();
			IDEdgeInstance<V, int> e = (IDEdgeInstance<V, int>)this.EdgeFactory.newInstance ();
			instance = new IDataContainerEInstanceImpl<V, E, int> (v.Id, e, Rank);
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
		public IDataContainerEInstance<V, E, int> DataContainerEInstance {
			get { return (IDataContainerEInstance<V, E, int>)instance; }
			set { 
				IDataContainerEInstance<V, E, int> dc = (IDataContainerEInstance<V, E, int>)value;
				dc.RankPartition = Rank;
				this.instance = dc;
			}
		}
		public IDataContainerEInstance<V, E, T> InstanceTFactory<T> (T i, T j, float w) {
			IDEdgeInstance<V, T> e = (IDEdgeInstance<V, T>)this.EdgeFactory.InstanceTFactory<T>(i,j);
			IDataContainerEInstance<V, E, T> instanceT = new IDataContainerEInstanceImpl<V, E, T> (e.Source, e, Rank); 
			return instanceT;
		}
	}

	[Serializable]
	public class IDataContainerEInstanceImpl<V, E, TV> : IDataContainerEInstance<V, E, TV> 
		where V: IDVertexBasic 
		where E:IDEdge<V> {

		public IDataContainerEInstanceImpl(){}
		public IDataContainerEInstanceImpl(TV v, IDEdgeInstance<V, TV> e, int part){
			this.vertex = v;
			this.edgeFactory = e;
			rankPartition = part;
		}
		#region ICloneable implementation
		public object Clone () {
			IDataContainerEInstanceImpl<V, E, TV> clone = new IDataContainerEInstanceImpl<V, E, TV> ();
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [2]))
				clone.Vertex = (TV)((ICloneable)vertex).Clone ();
			else
				clone.Vertex = vertex;
			clone.EdgeFactory = (IDEdgeInstance<V, TV>)edgeFactory.Clone ();
			clone.RankPartition = rankPartition;
			clone.AllowingLoops = _allowingLoops;
			clone.AllowingMultipleEdges = _allowingMultipleEdges;
			clone.DataSet =  new Dictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>> (dataSet);
			return (IDataContainerEInstance<V, E, TV>) clone;
		}
		#endregion

		#region IDataContainerEInstance implementation
		private TV vertex;
		private IDEdgeInstance<V, TV> edgeFactory;
		private int rankPartition = 0;
		private bool _allowingLoops = true;
		private bool _allowingMultipleEdges = false; 
		private IDictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>> dataSet; 

		public TV Vertex { get { return vertex; } set { this.vertex = (TV)value; } }
		public IDEdgeInstance<V, TV> EdgeFactory { get { return edgeFactory; } set { this.edgeFactory = (IDEdgeInstance<V, TV>)value; } }
		public int RankPartition { get { return rankPartition; } set { this.rankPartition = value; } }
		public bool AllowingLoops{ get { return _allowingLoops; } set{ _allowingLoops = value; } }
		public bool AllowingMultipleEdges{ get { return _allowingMultipleEdges; } set{ _allowingMultipleEdges = value; } }
		public IDictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>> DataSet { 
			get { if (dataSet == null) dataSet = new Dictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>> (); return dataSet; } 
			set { dataSet = (IDictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>>)value; }
		}

		public object ObjValue {
			get { return new Tuple<TV,IDEdgeInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>>>(vertex,edgeFactory,rankPartition,_allowingLoops,_allowingMultipleEdges,dataSet); }
			set { 
				this.vertex =                 ((Tuple<TV,IDEdgeInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>>>)value).Item1;
				this.edgeFactory =            ((Tuple<TV,IDEdgeInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>>>)value).Item2;
				this.rankPartition =          ((Tuple<TV,IDEdgeInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>>>)value).Item3;
				this._allowingLoops =         ((Tuple<TV,IDEdgeInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>>>)value).Item4;
				this._allowingMultipleEdges = ((Tuple<TV,IDEdgeInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>>>)value).Item5;
				this.dataSet =            ((Tuple<TV,IDEdgeInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>>>)value).Item6;

			}
		}
		public override bool Equals (object obj) {
			if (typeof(IDataContainerEInstance<V, E, TV>).IsAssignableFrom (obj.GetType ())) {
				IDataContainerEInstance<V, E, TV> o = (IDataContainerEInstance<V, E, TV>)obj;
				if (o.RankPartition == this.RankPartition)
					return true;
			}
			return false;
		}
		public override int GetHashCode () { return rankPartition; }

		public void newDataSet (int size) {
			dataSet = new Dictionary<TV, IEdgeContainer<IDEdgeInstance<V, TV>>> (size);
		}
		#endregion
	}
}