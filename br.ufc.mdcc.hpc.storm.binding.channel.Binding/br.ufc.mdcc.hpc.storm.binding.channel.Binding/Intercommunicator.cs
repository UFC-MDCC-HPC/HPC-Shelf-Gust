using System;

namespace br.ufc.mdcc.hpc.storm.binding.channel.Binding
{
	public delegate T ReductionOperation<T>(T x, T y);
	
	public interface Intercommunicator
	{
		#region point-to-point operations

		// Value versions ...

		void Send<T> (T value, int dest, int tag);

//		void SendReceive<T> (T inValue, int dest, int tag, out T outValue); /* ok */
//		void SendReceive<T> (T inValue, int dest, int sendTag, int source, int recvTag, out T outValue); /* ok */
//		void SendReceive<T> (T inValue, int dest, int sendTag, int source, int recvTag, out T outValue, out CompletedStatus status);

		T Receive<T> (int source, int tag); /* ok */
		void Receive<T>(int source, int tag, out T value); /* ok */
		void Receive<T> (int source, int tag, out T value, out CompletedStatus status);

		Request ImmediateSend<T> (T value, int dest, int tag);
		ReceiveRequest ImmediateReceive<T> (int source, int tag);

		// Array versions ... 
		void Send<T> (T[] values, int dest, int tag);

//		void SendReceive<T>(T[] inValues, int dest, int tag, ref T[] outValues); /* ok */
//		void SendReceive<T>(T[] inValues, int dest, int sendTag, int source, int recvTag, ref T[] outValues); /* ok */
//		void SendReceive<T> (T[] inValues, int dest, int sendTag, int source, int recvTag, ref T[] outValues, out CompletedStatus status);

		void Receive<T> (int source, int tag, ref T[] values); /* ok */
		void Receive<T> (int source, int tag, ref T[] values, out CompletedStatus status);

		Request ImmediateSend<T> (T[] values, int dest, int tag);
		ReceiveRequest ImmediateReceive<T> (int source, int tag, T[] values);

		// Probe.

		Status Probe (int source, int tag);
		Status ImmediateProbe (int source, int tag);

		#endregion point-to-point operations



		#region collective operations

		#region AllToAll

		T[] Allgather<T> (T value);
		void Allgather<T> (T inValue, ref T[] outValues);

		void AllgatherFlattened<T> (T[] inValues, int count, ref T[] outValues);
		void AllgatherFlattened<T> (T[] inValues, int[] counts, ref T[] outValues);

		T Allreduce<T>(T value, ReductionOperation<T> op);
		T[] Allreduce<T> (T[] values, ReductionOperation<T> op);
		void Allreduce<T> (T[] inValues, ReductionOperation<T> op, ref T[] outValues);

		T[] Alltoall<T> (T[] values);
		void Alltoall<T> (T[] inValues, ref T[] outValues);

		void AlltoallFlattened<T> (T[] inValues, int[] sendCounts, int[] recvCounts, ref T[] outValues);

		T[] ReduceScatter<T> (T[] values, ReductionOperation<T> op, int[] counts);
		void ReduceScatter<T> (T[] inValues, ReductionOperation<T> op, int[] counts, ref T[] outValues);

		#endregion AllToAll


		#region OneToAll

		void Broadcast<T> (ref T value, int root);
		void Broadcast<T> (ref T[] values, int root);

		void Scatter<T> (T[] values);
		T Scatter<T>(int root);
		void Scatter<T>();

		void ScatterFromFlattened<T> (T[] inValues, int count);
		void ScatterFromFlattened<T> (T[] inValues, int[] counts);
		void ScatterFromFlattened<T> (int count, int root, ref T[] outValues);
		void ScatterFromFlattened<T> (int[] counts, int root, ref T[] outValues);
		void ScatterFromFlattened<T> ();
		void ScatterFromFlattened<T> (T[] inValues, int count, int root, ref T[] outValues);
		void ScatterFromFlattened<T> (T[] inValues, int[] counts, int root, ref T[] outValues);

		#endregion OneToAll


		#region AllToOne

		T[] Gather<T> (T value, int root);
		T[] Gather<T> (int root);
		void Gather<T>();
		void Gather<T>(T inValue, int root, ref T[] outValues);

		void GatherFlattened<T>(int count, ref T[] outValues);
		T[] GatherFlattened<T>(int count);
		void GatherFlattened<T> (T[] inValues, int root);
		void GatherFlattened<T>();
		void GatherFlattened<T> (int[] counts, ref T[] outValues);
		T[] GatherFlattened<T>(int[] counts);

		T Reduce<T> (T value, ReductionOperation<T> op, int root);
		T[] Reduce<T>(T[] values, ReductionOperation<T> op, int root);
		void Reduce<T>(T[] inValues, ReductionOperation<T> op, int root, ref T[] outValues);

		#endregion AllToOne


		#endregion collective operations

	}
}

