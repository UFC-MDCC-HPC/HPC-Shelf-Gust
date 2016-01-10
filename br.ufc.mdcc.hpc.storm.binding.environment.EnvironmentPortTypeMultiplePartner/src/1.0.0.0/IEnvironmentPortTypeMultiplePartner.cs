using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using System.Collections.Generic;
using MPI;
using System;
using System.Runtime.Serialization;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner
{
	public interface IEnvironmentPortTypeMultiplePartner : BaseIEnvironmentPortTypeMultiplePartner, IEnvironmentPortType
	{

	}

	public interface IScatter<T>
	{
		T[] Value { get; set;} 
	}

	public interface IGather<T>
	{
		T[] Value { get; set;}
	}

	public interface IReduce<T>
	{
		T Value { get; set;} 
	}

	public interface IScan<T>
	{
		T Value { get; set;} 
	}

	[DataContract]
	internal class Scatter_<T> : IScatter<T>
	{
		private T[] values;

		#region IScatter implementation
		[DataMember]
		public T[] Value {
			get { return this.values; }
			set { this.values = value; }
		}
		#endregion

		public void check_size(int remote_size)
		{
			if (Value.Length != remote_size)
				throw new Exception ("Invalid attempt to build a scatter object: the size of the array must be equal to the size of the communicator's remote group");
		}
	}

	public class Scatter<U>
	{
		// This function must be called from within the environemnt binding.
		public static IScatter<U> create (Intercommunicator comm, U[] values)
		{
			if (comm.RemoteSize != values.Length)
				throw new Exception ("Invalid attempt to build a scatter object: the size of the array must be equal to the size of the communicator's remote group");

			IScatter<U> res = new Scatter_<U> ();
			res.Value = values;
			return res;
		}
	}
	
	[DataContract]
	internal class Gather_<T> : IGather<T>
	{
		private T[] values;

		#region IGather implementation

		[DataMember]
		public T[] Value {
			get { return this.values; }
			set { this.values = value; }
		}

		#endregion
	}

	public class Gather<U>
	{
		// This function must be called from within the environemnt binding.
		public static IGather<U> create (Intercommunicator comm, U[] values)
		{
			if (comm.RemoteSize != values.Length)
				throw new Exception ("Invalid attempt to build a gather object: the size of the array must be equal to the size of the communicator's remote group");

			IGather<U> res = new Gather_<U> ();
			res.Value = values;
			return res;
		}
	}


	public delegate T Operator<T> (T v1, T v2);


	[DataContract]
	internal class Reduce_<T> : IReduce<T>
	{
		private T value;

		#region IReduce implementation

		[DataMember]
		public T Value {
			get { return this.value; }
			set { this.value = value; }
		}

		#endregion

		/* The operator is defined in subclasses of IEnvironmentPortTypeMultipleClass, in such a way they may be informed by the user. 
		 * So, the binding implementation, which know the proper subclass, may get the corret operator.
		 */

	}

	public class Reduce<U>
	{
		// This function must be called from within the environemnt binding.
		public static IReduce<U> create (Intercommunicator comm, U[] values, Operator<U> op, U v0)
		{
			if (comm.RemoteSize != values.Length)
				throw new Exception ("Invalid attempt to build a reduce object: the size of the array must be equal to the size of the communicator's remote group");

			U value = v0;

			// apply the operator
			foreach (U v in values)
				value = op(value, v);

			IReduce<U> res = new Reduce_<U>();
			res.Value = value;
			return res;
		}
	}


	[DataContract]
	internal class Scan_<T> : IScan<T>
	{
		private T value;

		#region IScan implementation

		[DataMember]
		public T Value {
			get { return this.value; }
			set { this.value = value; }
		}

		#endregion

		/* The operator is defined in subclasses of IEnvironmentPortTypeMultipleClass, in such a way they may be informed by the user. 
		 * So, the binding implementation, which know the proper subclass, may get the corret operator.
		 */
	}

	public class Scan<U>
	{
		// This function must be called from within the environemnt binding.
		public static IScan<U> create (Intercommunicator comm, U[] values, Operator<U> op, U v0)
		{
			int rank = comm.Rank;

			if (comm.RemoteSize != values.Length)
				throw new Exception ("Invalid attempt to build a scan object: the size of the array must be equal to the size of the communicator's remote group");

			U value = v0;

			// apply the operator
			for (int i=0; i<rank; i++)
				value = op(value, values[i]);

			IScan<U> res = new Scan_<U>();
			res.Value = value;
			return res;
		}
	}

}