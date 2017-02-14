using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.example.sssp.DataSSSP;

namespace br.ufc.mdcc.hpcshelf.gust.example.sssp.DataSSSPImpl
{
	public class IDataSSSPImpl : BaseIDataSSSPImpl, IDataSSSP {

		public IDataSSSPImpl () { }

		override public void after_initialize () {
			newInstance (); 
		}

		public IDataSSSPInstance newInstance (int partid) {
			IDataSSSPInstance instance = (IDataSSSPInstance)newInstance ();
			instance.Partid = partid;
			return instance;
		}

		public object newInstance () {
			this.instance = new IDataSSSPInstanceImpl ();
			return this.Instance;
		}

		private IDataSSSPInstance instance;

		public object Instance {
			get { return instance; }
			set { this.instance = (IDataSSSPInstance)value; }
		}
		public IDataSSSPInstance InstanceDataSSSP {
			get { return instance; }
		}
	}

	[Serializable]
	public class IDataSSSPInstanceImpl : IDataSSSPInstance {

		#region IDataSSSPInstance implementation

		private ConcurrentDictionary<int, float> path_size = new ConcurrentDictionary<int, float>();
		private int partid = -1;
		private bool finished = false;

		public IDataSSSPInstanceImpl(){ }
		public IDataSSSPInstanceImpl(int partid):this(){ this.partid = partid; }

		public int Partid { get { return partid; } set { partid = (int) value; } }

		public bool Finished { get { return finished; } set { finished = (bool) value; } }

		public ConcurrentDictionary<int, float> Path_size {
			get { return this.path_size; }
			set { this.path_size = (ConcurrentDictionary<int, float>) value; }
		}

		public object ObjValue {
			get { return new Tuple<ConcurrentDictionary<int, float>, int, bool>(this.path_size, this.partid, this.finished); }
			set { 
				this.path_size = ((Tuple<ConcurrentDictionary<int, float>, int, bool>)value).Item1;
				this.partid =    ((Tuple<ConcurrentDictionary<int, float>, int, bool>)value).Item2;
				this.finished =  ((Tuple<ConcurrentDictionary<int, float>, int, bool>)value).Item3;
			}
		}

		public override int GetHashCode () {
			return this.partid;
		}
		public override bool Equals (object obj) {
			if (typeof(IDataSSSPInstance).IsAssignableFrom (obj.GetType ()))
				return this.Partid == ((IDataSSSPInstance)obj).Partid;
			return false;
		}
		#endregion

		#region ICloneable implementation
		public object Clone () {
			IDataSSSPInstance clone = new IDataSSSPInstanceImpl ();
			clone.Partid = this.Partid;
			clone.Finished = this.Finished;
			clone.Path_size = new ConcurrentDictionary<int, float> (this.Path_size);
			return clone;
		}
		#endregion
	}
}


