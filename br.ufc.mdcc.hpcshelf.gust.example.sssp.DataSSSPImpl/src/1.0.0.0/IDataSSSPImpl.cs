using System;
using System.Collections.Generic;
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

		private IDictionary<int, float> path_size = new Dictionary<int, float>();
		private bool activated = true;

		public IDataSSSPInstanceImpl(){ }

		public bool Activated { get { return activated; } set { activated = (bool) value; } }

		public IDictionary<int, float> Path_size {
			get { return this.path_size; }
			set { this.path_size = (IDictionary<int, float>) value; }
		}

		public object ObjValue {
			get { return new Tuple<IDictionary<int, float>, bool>(this.path_size, this.activated); }
			set { 
				this.path_size = ((Tuple<IDictionary<int, float>, bool>)value).Item1;
				this.activated =  ((Tuple<IDictionary<int, float>, bool>)value).Item2;
			}
		}

		#endregion

		#region ICloneable implementation
		public object Clone () {
			IDataSSSPInstance clone = new IDataSSSPInstanceImpl ();
			clone.Activated = this.Activated;
			clone.Path_size = new Dictionary<int, float> (this.Path_size);
			return clone;
		}
		#endregion
	}
}


