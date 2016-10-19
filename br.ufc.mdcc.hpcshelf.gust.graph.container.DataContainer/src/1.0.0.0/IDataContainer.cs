using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.graph.Edge;
using br.ufc.mdcc.common.Data;
using System.Collections.Generic;
using System;

namespace br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer {
	public interface IDataContainer<V, E> : BaseIDataContainer<V, E>, IData where V:IVertex where E:IEdge<V> {
		
	}

	public interface IDataContainerInstance<V, E> : IDataInstance, ICloneable 
		where V:IVertex 
		where E:IEdge<V> {
		int RankPartition { get; set; }
		bool AllowingMultipleEdges { get; }
		bool AllowingLoops { get; }
		void newDataSet (int size);
	}
	public class EdgeContainer<T>: IEdgeContainer<T> { 
		public ICollection<T> _outgoing;
		public ICollection<T> outgoing{
			get{ 
				return _outgoing;
			}
			set{ _outgoing = (ICollection<T>)value;	}
		}
		public ICollection<T> _incoming;
		public ICollection<T> incoming{
			get{ 
				return _incoming;
			}
			set{ _incoming = (ICollection<T>)value;	}
		}
	}
	public interface IEdgeContainer<T> { 
		ICollection<T> incoming { get; set; } 
		ICollection<T> outgoing { get; set; } 
	}
}