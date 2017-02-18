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
		private int halt = 0;

		public IDataSSSPInstanceImpl(){ }

		public int Halt { get { return halt; } set { halt = (int) value; } }

		public IDictionary<int, float> Path_size {
			get { return this.path_size; }
			set { this.path_size = (IDictionary<int, float>) value; }
		}

		public object ObjValue {
			get { return new Tuple<IDictionary<int, float>, int>(this.path_size, this.halt); }
			set { 
				this.path_size = ((Tuple<IDictionary<int, float>, int>)value).Item1;
				this.halt =  ((Tuple<IDictionary<int, float>, int>)value).Item2;
			}
		}

		public override string ToString () { 
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			IEnumerator<KeyValuePair<int, float>> iterator = this.Path_size.GetEnumerator ();
			if (iterator.MoveNext ()) {
				sb.Append (iterator.Current.Key + ":" + iterator.Current.Value);
				while (iterator.MoveNext ())
					sb.Append (System.Environment.NewLine+iterator.Current.Key + ":" + iterator.Current.Value);
			}
			string r = sb.ToString ();
			sb.Clear ();
			return r;
		}

		#endregion

		#region ICloneable implementation
		public object Clone () {
			IDataSSSPInstance clone = new IDataSSSPInstanceImpl ();
			clone.Halt = this.Halt;
			clone.Path_size = new Dictionary<int, float> (this.Path_size);
			return clone;
		}
		#endregion
	}
}


