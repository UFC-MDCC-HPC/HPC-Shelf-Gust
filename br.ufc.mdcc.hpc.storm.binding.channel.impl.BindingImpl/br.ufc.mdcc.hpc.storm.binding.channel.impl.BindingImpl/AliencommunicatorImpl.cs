using System;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using MPI;

namespace br.ufc.mdcc.hpc.storm.binding.channel.impl.BindingImpl
{
	public class AliencommunicatorImpl : Aliencommunicator
	{
		#region Aliencommunicator implementation

		// Send Singleton

		void Aliencommunicator.Send<T> (T value, Tuple<int, int> dest, int tag)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SEND, 0, TAG_SEND_OPERATION + 0);
			alien_communicator.Send<T> (value, dest, tag);
		}
		Request Aliencommunicator.ImmediateSend<T> (T value, Tuple<int, int> dest, int tag)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SEND, 0, TAG_SEND_OPERATION + 1);
			return alien_communicator.ImmediateSend<T> (value, dest, tag);
		}

		// Receive Singleton

		T Aliencommunicator.Receive<T> (Tuple<int, int> source, int tag)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.RECEIVE, 0, TAG_SEND_OPERATION + 0);
			return alien_communicator.Receive<T> (source, tag);
		}
		void Aliencommunicator.Receive<T> (Tuple<int, int> source, int tag, out T value)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.RECEIVE, 0, TAG_SEND_OPERATION + 1);
			alien_communicator.Receive<T> (source, tag, out value);
		}
		void Aliencommunicator.Receive<T> (Tuple<int, int> source, int tag, out T value, out CompletedStatus status)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.RECEIVE, 0, TAG_SEND_OPERATION + 2);
			alien_communicator.Receive<T> (source, tag, out value, out status);
		}
		ReceiveRequest Aliencommunicator.ImmediateReceive<T> (Tuple<int, int> source, int tag)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.RECEIVE, 0, TAG_SEND_OPERATION + 3);
			return alien_communicator.ImmediateReceive<T> (source, tag);
		}

		// Send Array

		void Aliencommunicator.Send<T> (T[] values, int dest, int tag)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SEND_ARRAY, 0, TAG_SEND_OPERATION + 0);
			alien_communicator.Send<T> (values, dest, tag);
		}
		Request Aliencommunicator.ImmediateSend<T> (T[] values, Tuple<int, int> dest, int tag)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SEND_ARRAY, 0, TAG_SEND_OPERATION + 1);
			return alien_communicator.ImmediateSend<T> (values, dest, tag);
		}

		// Receive Array

		void Aliencommunicator.Receive<T> (Tuple<int, int> source, int tag, ref T[] values, out CompletedStatus status)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.RECEIVE_ARRAY, 0, TAG_SEND_OPERATION + 0);
			alien_communicator.Receive<T> (source, tag, ref values, out status);
		}
		void Aliencommunicator.Receive<T> (Tuple<int, int> source, int tag, ref T[] values)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.RECEIVE_ARRAY, 0, TAG_SEND_OPERATION + 1);
			alien_communicator.Receive<T> (source, tag, ref values);
		}
		ReceiveRequest Aliencommunicator.ImmediateReceive<T> (Tuple<int, int> source, int tag, T[] values)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.RECEIVE_ARRAY, 0, TAG_SEND_OPERATION + 2);
			return alien_communicator.ImmediateReceive<T> (source, tag, values);
		}

		// Probe

		Status Aliencommunicator.Probe (Tuple<int, int> source, int tag)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.PROBE, 0, TAG_SEND_OPERATION + 0);
			return alien_communicator.Probe (source, tag);
		}

		Status Aliencommunicator.ImmediateProbe (Tuple<int, int> source, int tag)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.PROBE, 0, TAG_SEND_OPERATION + 1);
			return alien_communicator.ImmediateProbe (source, tag);
		}

		// All Gather

		T[] Aliencommunicator.Allgather<T> (int facet, T value)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.ALL_GATHER, 0, TAG_SEND_OPERATION + 0);
			return alien_communicator.Allgather<T> (facet, value);
		}
		void Aliencommunicator.Allgather<T> (int facet, T inValue, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.ALL_GATHER, 0, TAG_SEND_OPERATION + 1);
			alien_communicator.Allgather<T> (facet, inValue, ref outValues);
		}

		// All Gather Flattened

		void Aliencommunicator.AllgatherFlattened<T> (int facet, T[] inValues, int count, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.ALL_GATHER_FLATTENED, 0, TAG_SEND_OPERATION + 0);
			alien_communicator.AllgatherFlattened<T> (facet, inValues, count, ref outValues);
		}
		void Aliencommunicator.AllgatherFlattened<T> (int facet, T[] inValues, int[] counts, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.ALL_GATHER_FLATTENED, 0, TAG_SEND_OPERATION + 1);
			alien_communicator.AllgatherFlattened<T> (facet, inValues, counts, ref outValues);
		}

		// All Reduce

		T Aliencommunicator.Allreduce<T> (int facet, T value, ReductionOperation<T> op)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.ALL_REDUCE, 0, TAG_SEND_OPERATION + 0);
			return alien_communicator.Allreduce<T> (facet, value, op);
		}
		T[] Aliencommunicator.Allreduce<T> (int facet, T[] values, ReductionOperation<T> op)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.ALL_REDUCE, 0, TAG_SEND_OPERATION + 1);
			return alien_communicator.Allreduce<T> (facet, values, op);
		}
		void Aliencommunicator.Allreduce<T> (int facet, T[] inValues, ReductionOperation<T> op, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.ALL_REDUCE, 0, TAG_SEND_OPERATION + 2);
			alien_communicator.Allreduce<T> (facet, inValues, op, ref outValues);
		}

		// All to all 

		T[] Aliencommunicator.Alltoall<T> (int facet, T[] values)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.ALL_TO_ALL, 0, TAG_SEND_OPERATION + 0);
			return alien_communicator.Alltoall<T> (facet, values);
		}
		void Aliencommunicator.Alltoall<T> (int facet, T[] inValues, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.ALL_TO_ALL, 0, TAG_SEND_OPERATION + 1);
			alien_communicator.Alltoall<T> (facet, inValues, ref outValues);
		}

		// All to all Flattened

		void Aliencommunicator.AlltoallFlattened<T> (int facet, T[] inValues, int[] sendCounts, int[] recvCounts, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.ALL_TO_ALL_FLATTENED, 0, TAG_SEND_OPERATION);
			alien_communicator.AlltoallFlattened<T> (facet, inValues, sendCounts, recvCounts, ref outValues);
		}

		// Reduce-Scatter

		T[] Aliencommunicator.ReduceScatter<T> (int facet, T[] values, ReductionOperation<T> op, int[] counts)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.REDUCE_SCATTER, 0, TAG_SEND_OPERATION + 0);
			return alien_communicator.ReduceScatter<T> (facet, values, op, counts);
		}
		void Aliencommunicator.ReduceScatter<T> (int facet, T[] inValues, ReductionOperation<T> op, int[] counts, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.REDUCE_SCATTER, 0, TAG_SEND_OPERATION + 1);
			alien_communicator.ReduceScatter<T> (facet, inValues, op, counts, ref outValues);
		}

		// BROACAST

		void Aliencommunicator.Broadcast<T> (int facet, ref T value, int root)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.BROADCAST, 0, TAG_SEND_OPERATION + 0);
			alien_communicator.Broadcast<T> (facet, ref value, root);
		}
		void Aliencommunicator.Broadcast<T> (int facet, ref T[] values, int root)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.BROADCAST, 0, TAG_SEND_OPERATION + 1);
			alien_communicator.Broadcast<T> (facet, ref values, root);
		}

		// Scatter

		void Aliencommunicator.Scatter<T> (int facet, T[] values)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SCATTER, 0, TAG_SEND_OPERATION + 0);
			alien_communicator.Scatter<T> (facet, values);
		}
		T Aliencommunicator.Scatter<T> (int facet, int root)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SCATTER, 0, TAG_SEND_OPERATION + 1);
			return alien_communicator.Scatter<T> (facet, root);
		}
		void Aliencommunicator.Scatter<T> (int facet)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SCATTER, 0, TAG_SEND_OPERATION + 2);
			alien_communicator.Scatter<T> (facet);
		}

		// Scatter from Flattened

		void Aliencommunicator.ScatterFromFlattened<T> (int facet, T[] inValues, int count)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SCATTER_FROM_FLATTENED, 0, TAG_SEND_OPERATION + 0);
			alien_communicator.ScatterFromFlattened<T> (facet, inValues, count);
		}
		void Aliencommunicator.ScatterFromFlattened<T> (int facet, T[] inValues, int[] counts)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SCATTER_FROM_FLATTENED, 0, TAG_SEND_OPERATION + 1);
			alien_communicator.ScatterFromFlattened<T> (facet, inValues, counts);
		}
		void Aliencommunicator.ScatterFromFlattened<T> (int facet, int count, int root, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SCATTER_FROM_FLATTENED, 0, TAG_SEND_OPERATION + 2);
			alien_communicator.ScatterFromFlattened<T> (facet, count, root, ref outValues);
		}
		void Aliencommunicator.ScatterFromFlattened<T> (int facet, int[] counts, int root, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SCATTER_FROM_FLATTENED, 0, TAG_SEND_OPERATION + 3);
			alien_communicator.ScatterFromFlattened<T> (facet, counts, root, ref outValues);
		}
		void Aliencommunicator.ScatterFromFlattened<T> (int facet)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SCATTER_FROM_FLATTENED, 0, TAG_SEND_OPERATION + 4);
			alien_communicator.ScatterFromFlattened<T> (facet);
		}
		void Aliencommunicator.ScatterFromFlattened<T> (int facet, T[] inValues, int count, int root, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SCATTER_FROM_FLATTENED, 0, TAG_SEND_OPERATION + 5);
			alien_communicator.ScatterFromFlattened<T> (facet, inValues, count, root, ref outValues);
		}
		void Aliencommunicator.ScatterFromFlattened<T> (int facet, T[] inValues, int[] counts, int root, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.SCATTER_FROM_FLATTENED, 0, TAG_SEND_OPERATION + 6);
			alien_communicator.ScatterFromFlattened<T> (facet, inValues, counts, root, ref outValues);
		}

		// GATHER

		T[] Aliencommunicator.Gather<T> (int facet, T value, int root)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.GATHER, 0, TAG_SEND_OPERATION + 0);
			return alien_communicator.Gather<T> (facet, value, root);
		}
		T[] Aliencommunicator.Gather<T> (int facet, int root)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.GATHER, 0, TAG_SEND_OPERATION + 1);
			return alien_communicator.Gather<T> (facet, root);
		}
		void Aliencommunicator.Gather<T> (int facet)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.GATHER, 0, TAG_SEND_OPERATION + 2);
			alien_communicator.Gather<T> (facet);
		}
		void Aliencommunicator.Gather<T> (int facet, T inValue, int root, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.GATHER, 0, TAG_SEND_OPERATION + 3);
			alien_communicator.Gather<T> (facet, inValue, root, ref outValues);
		}

		// Gather Flattened

		void Aliencommunicator.GatherFlattened<T> (int facet, int count, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.GATHER_FLATTENED, 0, TAG_SEND_OPERATION + 0);
			alien_communicator.GatherFlattened<T> (facet, count, ref outValues);
		}
		T[] Aliencommunicator.GatherFlattened<T> (int facet, int count)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.GATHER_FLATTENED, 0, TAG_SEND_OPERATION + 1);
			return alien_communicator.GatherFlattened<T> (facet, count);
		}
		void Aliencommunicator.GatherFlattened<T> (int facet, T[] inValues, int root)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.GATHER_FLATTENED, 0, TAG_SEND_OPERATION + 2);
			alien_communicator.GatherFlattened<T> (facet, inValues, root);
		}
		void Aliencommunicator.GatherFlattened<T> (int facet)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.GATHER_FLATTENED, 0, TAG_SEND_OPERATION + 3);
			alien_communicator.GatherFlattened<T> (facet);
		}
		void Aliencommunicator.GatherFlattened<T> (int facet, int[] counts, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.GATHER_FLATTENED, 0, TAG_SEND_OPERATION + 4);
			alien_communicator.GatherFlattened<T> (facet, counts, ref outValues);
		}
		T[] Aliencommunicator.GatherFlattened<T> (int facet, int[] counts)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.GATHER_FLATTENED, 0, TAG_SEND_OPERATION + 5);
			return alien_communicator.GatherFlattened<T> (facet, counts);
		}

		// REDUCE 

		T Aliencommunicator.Reduce<T> (int facet, T value, ReductionOperation<T> op, int root)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.REDUCE, 0, TAG_SEND_OPERATION + 0);
			return alien_communicator.Reduce<T> (facet, value, op, root);
		}
		T[] Aliencommunicator.Reduce<T> (int facet, T[] values, ReductionOperation<T> op, int root)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.REDUCE, 0, TAG_SEND_OPERATION + 1);
			return alien_communicator.Reduce<T> (facet, values, op, root);
		}
		void Aliencommunicator.Reduce<T> (int facet, T[] inValues, ReductionOperation<T> op, int root, ref T[] outValues)
		{
			this.RootCommunicator.Send<AliencommunicatorOperation>(AliencommunicatorOperation.REDUCE, 0, TAG_SEND_OPERATION + 2);
			alien_communicator.Reduce<T> (facet, inValues, op, root, ref outValues);
		}
		#endregion
	}
}

