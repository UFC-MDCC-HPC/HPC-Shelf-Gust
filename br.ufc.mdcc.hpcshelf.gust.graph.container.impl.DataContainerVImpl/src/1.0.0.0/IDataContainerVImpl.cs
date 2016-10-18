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
	public class IDataContainerVInstanceImpl<V, E, TV> : IDataContainerVInstance<V, E, TV> 
		where V: IDVertexBasic 
		where E:IDEdgeBasic<V> {

		public IDataContainerVInstanceImpl(){}
		public IDataContainerVInstanceImpl(TV v, IDEdgeBasicInstance<V, TV> e, int part){
			this.vertex = v;
			this.edgeFactory = e;
			rankPartition = part;
		}
		#region ICloneable implementation
		public object Clone () {
			IDataContainerVInstanceImpl<V, E, TV> clone = new IDataContainerVInstanceImpl<V, E, TV> ();
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [2]))
				clone.Vertex = (TV)((ICloneable)vertex).Clone ();
			else
				clone.Vertex = vertex;
			clone.EdgeFactory = (IDEdgeBasicInstance<V, TV>)edgeFactory.Clone ();
			clone.RankPartition = rankPartition;
			clone.AllowingLoops = _allowingLoops;
			clone.AllowingMultipleEdges = _allowingMultipleEdges;
			clone.DataSet =  new Dictionary<TV, IEdgeContainer<TV>> (dataSet);
			return (IDataContainerVInstance<V, E, TV>) clone;
		}
		#endregion

		#region IDataContainerVInstance implementation
		private TV vertex;
		private IDEdgeBasicInstance<V, TV> edgeFactory;
		private int rankPartition = 0;
		private bool _allowingLoops = true;
		private bool _allowingMultipleEdges = false; 
		private IDictionary<TV, IEdgeContainer<TV>> dataSet; 

		public TV Vertex { get { return vertex; } set { this.vertex = (TV)value; } }
		public IDEdgeBasicInstance<V, TV> EdgeFactory { get { return edgeFactory; } set { this.edgeFactory = (IDEdgeBasicInstance<V, TV>)value; } }
		public int RankPartition { get { return rankPartition; } set { this.rankPartition = value; } }
		public bool AllowingLoops{ get { return _allowingLoops; } set{ _allowingLoops = value; } }
		public bool AllowingMultipleEdges{ get { return _allowingMultipleEdges; } set{ _allowingMultipleEdges = value; } }
		public IDictionary<TV, IEdgeContainer<TV>> DataSet { 
			get { if (dataSet == null) dataSet = new Dictionary<TV, IEdgeContainer<TV>> (); return dataSet; } 
			set { dataSet = (IDictionary<TV, IEdgeContainer<TV>>)value; }
		}

		public object ObjValue {
			get { return new Tuple<TV,IDEdgeBasicInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>(vertex,edgeFactory,rankPartition,_allowingLoops,_allowingMultipleEdges,dataSet); }
			set { 
				this.vertex =                 ((Tuple<TV,IDEdgeBasicInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item1;
				this.edgeFactory =            ((Tuple<TV,IDEdgeBasicInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item2;
				this.rankPartition =          ((Tuple<TV,IDEdgeBasicInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item3;
				this._allowingLoops =         ((Tuple<TV,IDEdgeBasicInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item4;
				this._allowingMultipleEdges = ((Tuple<TV,IDEdgeBasicInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item5;
				this.dataSet =            ((Tuple<TV,IDEdgeBasicInstance<V, TV>, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item6;

			}
		}
		public override bool Equals (object obj) {
			if (typeof(IDataContainerVInstance<V, E, TV>).IsAssignableFrom (obj.GetType ())) {
				IDataContainerVInstance<V, E, TV> o = (IDataContainerVInstance<V, E, TV>)obj;
				if (o.RankPartition == this.RankPartition)
					return true;
			}
			return false;
		}
		public override int GetHashCode () { return rankPartition; }

		public void newDataSet (int size) {
			dataSet = new Dictionary<TV, IEdgeContainer<TV>> (size);
		}
		#endregion
	}
}
