using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerV;
using br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerVImpl
{
	public class IDataContainerVImpl<V, E> : BaseIDataContainerVImpl<V, E>, IDataContainerV<V, E>
where V:IVertex
where E:IEdge<V> {
		public IDataContainerVImpl(){ }
		override public void after_initialize () { 
			newInstance (); 
		}
		public object newInstance () {
			IVertexInstance v = (IVertexInstance)this.Vertex.newInstance ();
			IEdgeInstance<V, int> e = (IEdgeInstance<V, int>)this.EdgeFactory.newInstance ();
			instance = new IDataContainerVInstanceImpl<V, E, int, IEdgeInstance<V, int>> (v.Id, e, Rank);
			return this.instance;
		}
		private IDataContainerInstance<V, E> instance;
		public object Instance {
			get { return instance; }
			set { 
				this.instance = (IDataContainerInstance<V, E>)value;
				this.instance.RankPartition = Rank; 
			}
		}
		public IDataContainerVInstance<V, E, int, IEdgeInstance<V, int>> DataContainerVInstance {
			get { return (IDataContainerVInstance<V, E, int, IEdgeInstance<V, int>>)instance; }
			set { 
				IDataContainerVInstance<V, E, int, IEdgeInstance<V, int>> dc = (IDataContainerVInstance<V, E, int, IEdgeInstance<V, int>>)value;
				dc.RankPartition = Rank; 
				this.instance = dc;
			}
		}
		public IDataContainerVInstance<V, E, TV, TE> InstanceTFactory<TV, TE> (TE e) where TE:IEdgeInstance<V, TV>{
			TE ei = (TE)this.EdgeFactory.InstanceTFactory<TV>(e.Source,e.Target,e.Weight);
			IDataContainerVInstance<V, E, TV, TE> instanceT = new IDataContainerVInstanceImpl<V, E, TV, TE> (ei.Source, ei, Rank); 
			return instanceT;
		}
	}

	[Serializable]
	public class IDataContainerVInstanceImpl<V, E, TV, TE> : IDataContainerVInstance<V, E, TV, TE> 
		where V: IVertex 
		where E:IEdge<V> 
		where TE:IEdgeInstance<V, TV> {

		public IDataContainerVInstanceImpl(){}
		public IDataContainerVInstanceImpl(TV v, TE e, int part){
			this.vertex = v;
			this.edgeFactory = e;
			rankPartition = part;
		}
		#region ICloneable implementation
		public object Clone () {
			IDataContainerVInstanceImpl<V, E, TV, TE> clone = new IDataContainerVInstanceImpl<V, E, TV, TE> ();
			Type[] types = this.GetType ().GenericTypeArguments;
			if (typeof(ICloneable).IsAssignableFrom (types [2]))
				clone.Vertex = (TV)((ICloneable)vertex).Clone ();
			else
				clone.Vertex = vertex;
			clone.EdgeFactory = (TE)edgeFactory.Clone ();
			clone.RankPartition = rankPartition;
			clone.AllowingLoops = _allowingLoops;
			clone.AllowingMultipleEdges = _allowingMultipleEdges;
			clone.DataSet =  new Dictionary<TV, IEdgeContainer<TV>> (dataSet);
			return (IDataContainerVInstance<V, E, TV, TE>) clone;
		}
		#endregion

		#region IDataContainerVInstance implementation
		private TV vertex;
		private TE edgeFactory;
		private int rankPartition = 0;
		private bool _allowingLoops = true;
		private bool _allowingMultipleEdges = false; 
		private IDictionary<TV, IEdgeContainer<TV>> dataSet; 

		public TV Vertex { get { return vertex; } set { this.vertex = (TV)value; } }
		public TE EdgeFactory { get { return edgeFactory; } set { this.edgeFactory = (TE)value; } }
		public int RankPartition { get { return rankPartition; } set { this.rankPartition = value; } }
		public bool AllowingLoops{ get { return _allowingLoops; } set{ _allowingLoops = value; } }
		public bool AllowingMultipleEdges{ get { return _allowingMultipleEdges; } set{ _allowingMultipleEdges = value; } }
		public IDictionary<TV, IEdgeContainer<TV>> DataSet { 
			get { if (dataSet == null) dataSet = new Dictionary<TV, IEdgeContainer<TV>> (); return dataSet; } 
			set { dataSet = (IDictionary<TV, IEdgeContainer<TV>>)value; }
		}

		public object ObjValue {
			get { return new Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>(vertex,edgeFactory,rankPartition,_allowingLoops,_allowingMultipleEdges,dataSet); }
			set { 
				this.vertex =                 ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item1;
				this.edgeFactory =            ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item2;
				this.rankPartition =          ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item3;
				this._allowingLoops =         ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item4;
				this._allowingMultipleEdges = ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item5;
				this.dataSet =            ((Tuple<TV,TE, int, bool, bool, IDictionary<TV, IEdgeContainer<TV>>>)value).Item6;

			}
		}
		public override bool Equals (object obj) {
			if (typeof(IDataContainerVInstance<V, E, TV, TE>).IsAssignableFrom (obj.GetType ())) {
				IDataContainerVInstance<V, E, TV, TE> o = (IDataContainerVInstance<V, E, TV, TE>)obj;
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
