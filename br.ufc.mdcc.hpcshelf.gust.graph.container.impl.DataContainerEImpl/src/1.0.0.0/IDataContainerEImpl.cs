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
	public class IDataContainerEInstanceImpl<V, E, RV> : IDataContainerEInstance<V, E, RV> 
		where V: IDVertexBasic 
		where E:IDEdge<V> {

		public IDataContainerEInstanceImpl(){}
		public IDataContainerEInstanceImpl(RV v, IDEdgeInstance<V, RV> e, int part){
			this.vertex = v;
			this.edgeFactory = e;
			rankPartition = part;
		}
		#region ICloneable implementation
		public object Clone () {
			IDataContainerEInstanceImpl<V, E, RV> clone = new IDataContainerEInstanceImpl<V, E, RV> ();
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [2]))
				clone.Vertex = (RV)((ICloneable)vertex).Clone ();
			else
				clone.Vertex = vertex;
			clone.EdgeFactory = (IDEdgeInstance<V, RV>)edgeFactory.Clone ();
			clone.RankPartition = rankPartition;
			clone.AllowingLoops = _allowingLoops;
			clone.AllowingMultipleEdges = _allowingMultipleEdges;
			clone.DataSet =  new Dictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>> (dataSet);
			return (IDataContainerEInstance<V, E, RV>) clone;
		}
		#endregion

		#region IDataContainerEInstance implementation
		private RV vertex;
		private IDEdgeInstance<V, RV> edgeFactory;
		private int rankPartition = 0;
		private bool _allowingLoops = true;
		private bool _allowingMultipleEdges = false; 
		private IDictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>> dataSet; 

		public RV Vertex { get { return vertex; } set { this.vertex = (RV)value; } }
		public IDEdgeInstance<V, RV> EdgeFactory { get { return edgeFactory; } set { this.edgeFactory = (IDEdgeInstance<V, RV>)value; } }
		public int RankPartition { get { return rankPartition; } set { this.rankPartition = value; } }
		public bool AllowingLoops{ get { return _allowingLoops; } set{ _allowingLoops = value; } }
		public bool AllowingMultipleEdges{ get { return _allowingMultipleEdges; } set{ _allowingMultipleEdges = value; } }
		public IDictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>> DataSet { 
			get { if (dataSet == null) dataSet = new Dictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>> (); return dataSet; } 
			set { dataSet = (IDictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>>)value; }
		}

		public object ObjValue {
			get { return new Tuple<RV,IDEdgeInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>>>(vertex,edgeFactory,rankPartition,_allowingLoops,_allowingMultipleEdges,dataSet); }
			set { 
				this.vertex =                 ((Tuple<RV,IDEdgeInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>>>)value).Item1;
				this.edgeFactory =            ((Tuple<RV,IDEdgeInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>>>)value).Item2;
				this.rankPartition =          ((Tuple<RV,IDEdgeInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>>>)value).Item3;
				this._allowingLoops =         ((Tuple<RV,IDEdgeInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>>>)value).Item4;
				this._allowingMultipleEdges = ((Tuple<RV,IDEdgeInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>>>)value).Item5;
				this.dataSet =            ((Tuple<RV,IDEdgeInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>>>)value).Item6;

			}
		}
		public override bool Equals (object obj) {
			if (typeof(IDataContainerEInstance<V, E, RV>).IsAssignableFrom (obj.GetType ())) {
				IDataContainerEInstance<V, E, RV> o = (IDataContainerEInstance<V, E, RV>)obj;
				if (o.RankPartition == this.RankPartition)
					return true;
			}
			return false;
		}
		public override int GetHashCode () { return rankPartition; }

		public void newDataSet (int size) {
			dataSet = new Dictionary<RV, IEdgeContainer<IDEdgeInstance<V, RV>>> (size);
		}
		#endregion
	}
}
