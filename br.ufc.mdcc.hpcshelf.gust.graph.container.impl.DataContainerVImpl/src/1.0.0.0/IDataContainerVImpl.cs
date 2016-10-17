using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.DVertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdgeBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV;

using br.ufc.mdcc.hpcshelf.gust.graph.DVertex;
using br.ufc.mdcc.hpcshelf.gust.graph.DEdge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerVImpl {
	public class IDataContainerVImpl<V, E> : BaseIDataContainerVImpl<V, E>, IDataContainerV<V, E> 
		where V:IDVertexBasic 
		where E:IDEdgeBasic<V> {
		public IDataContainerVImpl(){ }
		override public void after_initialize () { 
			newInstance (); 
		}
		public object newInstance () {
			IDVertexBasicInstance v = (IDVertexBasicInstance) this.Vertex.newInstance ();
			IDEdgeBasicInstance<V, int> e = (IDEdgeBasicInstance<V, int>)this.EdgeFactory.newInstance ();
			instance = new IDataContainerVInstanceImpl<V, E, int> (v.Id, e, Rank);
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
		public IDataContainerVInstance<V, E, int> DataContainerVInstance {
			get { return (IDataContainerVInstance<V, E, int>)instance; }
			set { 
				IDataContainerVInstance<V, E, int> dc = (IDataContainerVInstance<V, E, int>)value;
				dc.RankPartition = Rank; 
				this.instance = dc;
			}
		}
		public IDataContainerVInstance<V, E, T> InstanceTFactory<T> (T i, T j) {
			IDEdgeBasicInstance<V, T> e = (IDEdgeBasicInstance<V, T>)this.EdgeFactory.InstanceTFactory<T>(i,j,1.0f);
			IDataContainerVInstance<V, E, T> instanceT = new IDataContainerVInstanceImpl<V, E, T> (e.Source, e, Rank); 
			return instanceT;
		}
	}

	[Serializable]
	public class IDataContainerVInstanceImpl<V, E, RV> : IDataContainerVInstance<V, E, RV> 
		where V: IDVertexBasic 
		where E:IDEdgeBasic<V> {

		public IDataContainerVInstanceImpl(){}
		public IDataContainerVInstanceImpl(RV v, IDEdgeBasicInstance<V, RV> e, int part){
			this.vertex = v;
			this.edgeFactory = e;
			rankPartition = part;
		}
		#region ICloneable implementation
		public object Clone () {
			IDataContainerVInstanceImpl<V, E, RV> clone = new IDataContainerVInstanceImpl<V, E, RV> ();
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [2]))
				clone.Vertex = (RV)((ICloneable)vertex).Clone ();
			else
				clone.Vertex = vertex;
			clone.EdgeFactory = (IDEdgeBasicInstance<V, RV>)edgeFactory.Clone ();
			clone.RankPartition = rankPartition;
			clone.AllowingLoops = _allowingLoops;
			clone.AllowingMultipleEdges = _allowingMultipleEdges;
			clone.DataSet =  new Dictionary<RV, IEdgeContainer<RV>> (dataSet);
			return (IDataContainerVInstance<V, E, RV>) clone;
		}
		#endregion

		#region IDataContainerVInstance implementation
		private RV vertex;
		private IDEdgeBasicInstance<V, RV> edgeFactory;
		private int rankPartition = 0;
		private bool _allowingLoops = true;
		private bool _allowingMultipleEdges = false; 
		private IDictionary<RV, IEdgeContainer<RV>> dataSet; 

		public RV Vertex { get { return vertex; } set { this.vertex = (RV)value; } }
		public IDEdgeBasicInstance<V, RV> EdgeFactory { get { return edgeFactory; } set { this.edgeFactory = (IDEdgeBasicInstance<V, RV>)value; } }
		public int RankPartition { get { return rankPartition; } set { this.rankPartition = value; } }
		public bool AllowingLoops{ get { return _allowingLoops; } set{ _allowingLoops = value; } }
		public bool AllowingMultipleEdges{ get { return _allowingMultipleEdges; } set{ _allowingMultipleEdges = value; } }
		public IDictionary<RV, IEdgeContainer<RV>> DataSet { 
			get { if (dataSet == null) dataSet = new Dictionary<RV, IEdgeContainer<RV>> (); return dataSet; } 
			set { dataSet = (IDictionary<RV, IEdgeContainer<RV>>)value; }
		}

		public object ObjValue {
			get { return new Tuple<RV,IDEdgeBasicInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<RV>>>(vertex,edgeFactory,rankPartition,_allowingLoops,_allowingMultipleEdges,dataSet); }
			set { 
				this.vertex =                 ((Tuple<RV,IDEdgeBasicInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<RV>>>)value).Item1;
				this.edgeFactory =            ((Tuple<RV,IDEdgeBasicInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<RV>>>)value).Item2;
				this.rankPartition =          ((Tuple<RV,IDEdgeBasicInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<RV>>>)value).Item3;
				this._allowingLoops =         ((Tuple<RV,IDEdgeBasicInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<RV>>>)value).Item4;
				this._allowingMultipleEdges = ((Tuple<RV,IDEdgeBasicInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<RV>>>)value).Item5;
				this.dataSet =            ((Tuple<RV,IDEdgeBasicInstance<V, RV>, int, bool, bool, IDictionary<RV, IEdgeContainer<RV>>>)value).Item6;

			}
		}
		public override bool Equals (object obj) {
			if (typeof(IDataContainerVInstance<V, E, RV>).IsAssignableFrom (obj.GetType ())) {
				IDataContainerVInstance<V, E, RV> o = (IDataContainerVInstance<V, E, RV>)obj;
				if (o.RankPartition == this.RankPartition)
					return true;
			}
			return false;
		}
		public override int GetHashCode () { return rankPartition; }

		public void newDataSet (int size) {
			dataSet = new Dictionary<RV, IEdgeContainer<RV>> (size);
		}
		#endregion
	}
}
