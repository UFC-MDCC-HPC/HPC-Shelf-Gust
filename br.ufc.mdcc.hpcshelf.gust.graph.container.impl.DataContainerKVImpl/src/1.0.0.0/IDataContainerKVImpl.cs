using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerKV;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerKVImpl
{
	public class IDataContainerKVImpl<V, E> : BaseIDataContainerKVImpl<V, E>, IDataContainerKV<V, E>
where V:IVertex
where E:IEdge<V> {
		public IDataContainerKVImpl(){ }
		override public void after_initialize () { 
			newInstance (); 
		}
		public object newInstance () {
			IVertexInstance v = (IVertexInstance) this.Vertex.newInstance ();
			IEdgeInstance<V, int> e = (IEdgeInstance<V, int>)this.EdgeFactory.newInstance ();
			instance = new IDataContainerKVInstanceImpl<V, E, int, IEdgeInstance<V, int>> (v.Id, e, Rank);
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
		public IDataContainerKVInstance<V, E, int, IEdgeInstance<V, int>> DataContainerKVInstance {
			get { return (IDataContainerKVInstance<V, E, int, IEdgeInstance<V, int>>)instance; }
			set { 
				IDataContainerKVInstance<V, E, int, IEdgeInstance<V, int>> dc = (IDataContainerKVInstance<V, E, int, IEdgeInstance<V, int>>)value;
				dc.RankPartition = Rank;
				this.instance = dc;
			}
		}
		public IDataContainerKVInstance<V, E, TV, TE> InstanceTFactory<TV, TE> (TE e) where TE:IEdgeInstance<V, TV>{
			TE ei = (TE)this.EdgeFactory.InstanceTFactory<TV>(e.Source,e.Target,e.Weight);
			IDataContainerKVInstance<V, E, TV, TE> instanceT = new IDataContainerKVInstanceImpl<V, E, TV, TE> (ei.Source, ei, Rank); 
			return instanceT;
		}
	}

	[Serializable]
	public class IDataContainerKVInstanceImpl<V, E, TV, TE> : IDataContainerKVInstance<V, E, TV, TE> 
		where V: IVertex 
		where E:IEdge<V> 
		where TE: IEdgeInstance<V, TV> {

		public IDataContainerKVInstanceImpl(){}
		public IDataContainerKVInstanceImpl(TV v, TE e, int part){
			this.vertex = v;
			this.edgeFactory = e;
			rankPartition = part;
		}
		#region ICloneable implementation
		public object Clone () {
			IDataContainerKVInstanceImpl<V, E, TV, TE> clone = new IDataContainerKVInstanceImpl<V, E, TV, TE> ();
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [2]))
				clone.Vertex = (TV)((ICloneable)vertex).Clone ();
			else
				clone.Vertex = vertex;
			clone.EdgeFactory = (TE)edgeFactory.Clone ();
			clone.RankPartition = rankPartition;
			clone.AllowingLoops = _allowingLoops;
			clone.AllowingMultipleEdges = _allowingMultipleEdges;
			clone.DataSet =  new Dictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> (dataSet);
			return (IDataContainerKVInstance<V, E, TV, TE>) clone;
		}
		#endregion

		#region IDataContainerKVInstance implementation
		private TV vertex;
		private TE edgeFactory;
		private int rankPartition = 0;
		private bool _allowingLoops = true;
		private bool _allowingMultipleEdges = false; 
		private IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> dataSet; 

		public TV Vertex { get { return vertex; } set { this.vertex = (TV)value; } }
		public TE EdgeFactory { get { return edgeFactory; } set { this.edgeFactory = (TE)value; } }
		public int RankPartition { get { return rankPartition; } set { this.rankPartition = value; } }
		public bool AllowingLoops{ get { return _allowingLoops; } set{ _allowingLoops = value; } }
		public bool AllowingMultipleEdges{ get { return _allowingMultipleEdges; } set{ _allowingMultipleEdges = value; } }
		public IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> DataSet { 
			get { if (dataSet == null) dataSet = new Dictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>> (); return dataSet; } 
			set { dataSet = (IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>)value; }
		}

		public object ObjValue {
			get { return new Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>(vertex,edgeFactory,rankPartition,_allowingLoops,_allowingMultipleEdges,dataSet); }
			set { 
				this.vertex =                 ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item1;
				this.edgeFactory =            ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item2;
				this.rankPartition =          ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item3;
				this._allowingLoops =         ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item4;
				this._allowingMultipleEdges = ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item5;
				this.dataSet =            ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<KeyValuePair<TV, float>>>>)value).Item6;

			}
		}
		public override bool Equals (object obj) {
			if (typeof(IDataContainerKVInstance<V, E, TV, TE>).IsAssignableFrom (obj.GetType ())) {
				IDataContainerKVInstance<V, E, TV, TE> o = (IDataContainerKVInstance<V, E, TV, TE>)obj;
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
