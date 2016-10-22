using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.VertexBasic;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;//Basic;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerE;

using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerEImpl {
	public class IDataContainerEImpl<V, E> : BaseIDataContainerEImpl<V, E>, IDataContainerE<V, E> 
		where V:IVertexBasic 
		where E:IEdge<V> {
		public IDataContainerEImpl(){ }
		override public void after_initialize () { 
			newInstance (); 
		}
		public object newInstance () {
			IVertexBasicInstance v = (IVertexBasicInstance) this.Vertex.newInstance ();
			IEdgeInstance<V, int> e = (IEdgeInstance<V, int>)this.EdgeFactory.newInstance ();
			instance = new IDataContainerEInstanceImpl<V, E, int, IEdgeInstance<V, int>> (v.Id, e, Rank);
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
		public IDataContainerEInstance<V, E, int, IEdgeInstance<V, int>> DataContainerEInstance {
			get { return (IDataContainerEInstance<V, E, int, IEdgeInstance<V, int>>) this.instance; }
			set { 
				this.instance = (IDataContainerEInstance<V, E, int, IEdgeInstance<V, int>>)value;
			}
		}
		public IDataContainerEInstance<V, E, TV, TE> InstanceTFactory<TV, TE> (TE e) where TE:IEdgeInstance<V, TV> {
			TE ei = (TE)this.EdgeFactory.InstanceTFactory<TV> (e.Source, e.Target);//,e.Weight);
			IDataContainerEInstance<V, E, TV, TE> instanceT = new IDataContainerEInstanceImpl<V, E, TV, TE> (ei.Source, ei, Rank); 
			return instanceT;
		}
	}

	[Serializable]
	public class IDataContainerEInstanceImpl<V, E, TV, TE> : IDataContainerEInstance<V, E, TV, TE> 
		where V: IVertexBasic 
		where E:IEdge<V> 
		where TE:IEdgeInstance<V, TV> {

		public IDataContainerEInstanceImpl(){}
		public IDataContainerEInstanceImpl(TV v, TE e, int part){
			this.vertex = v;
			this.edgeFactory = e;
			rankPartition = part;
		}
		#region ICloneable implementation
		public object Clone () {
			IDataContainerEInstanceImpl<V, E, TV, TE> clone = new IDataContainerEInstanceImpl<V, E, TV, TE> ();
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [2]))
				clone.Vertex = (TV)((ICloneable)vertex).Clone ();
			else
				clone.Vertex = vertex;
			clone.EdgeFactory = (TE)edgeFactory.Clone ();
			clone.RankPartition = rankPartition;
			clone.AllowingLoops = _allowingLoops;
			clone.AllowingMultipleEdges = _allowingMultipleEdges;
			clone.DataSet =  new Dictionary<TV, IEdgeContainer<TE>> (dataSet);
			return (IDataContainerEInstance<V, E, TV, TE>) clone;
		}
		#endregion

		#region IDataContainerEInstance implementation
		private TV vertex;
		private TE edgeFactory;
		private int rankPartition = 0;
		private bool _allowingLoops = true;
		private bool _allowingMultipleEdges = false; 
		private IDictionary<TV, IEdgeContainer<TE>> dataSet; 

		public TV Vertex { get { return vertex; } set { this.vertex = (TV)value; } }
		public TE EdgeFactory { get { return edgeFactory; } set { this.edgeFactory = (TE)value; } }
		public int RankPartition { get { return rankPartition; } set { this.rankPartition = value; } }
		public bool AllowingLoops{ get { return _allowingLoops; } set{ _allowingLoops = value; } }
		public bool AllowingMultipleEdges{ get { return _allowingMultipleEdges; } set{ _allowingMultipleEdges = value; } }
		public IDictionary<TV, IEdgeContainer<TE>> DataSet { 
			get { if (dataSet == null) dataSet = new Dictionary<TV, IEdgeContainer<TE>> (); return dataSet; } 
			set { dataSet = (IDictionary<TV, IEdgeContainer<TE>>)value; }
		}

		public object ObjValue {
			get { return new Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TE>>>(vertex,edgeFactory,rankPartition,_allowingLoops,_allowingMultipleEdges,dataSet); }
			set { 
				this.vertex =                 ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TE>>>)value).Item1;
				this.edgeFactory =            ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TE>>>)value).Item2;
				this.rankPartition =          ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TE>>>)value).Item3;
				this._allowingLoops =         ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TE>>>)value).Item4;
				this._allowingMultipleEdges = ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TE>>>)value).Item5;
				this.dataSet =            ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TE>>>)value).Item6;

			}
		}
		public override bool Equals (object obj) {
			if (typeof(IDataContainerEInstance<V, E, TV, TE>).IsAssignableFrom (obj.GetType ())) {
				IDataContainerEInstance<V, E, TV, TE> o = (IDataContainerEInstance<V, E, TV, TE>)obj;
				if (o.RankPartition == this.RankPartition)
					return true;
			}
			return false;
		}
		public override int GetHashCode () { return rankPartition; }

		public void newDataSet (int size) {
			dataSet = new Dictionary<TV, IEdgeContainer<TE>> (size);
		}
		#endregion
	}
}
