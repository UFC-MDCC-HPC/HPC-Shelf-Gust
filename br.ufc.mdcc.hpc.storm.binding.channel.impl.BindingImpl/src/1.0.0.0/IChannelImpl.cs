using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using MPI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace br.ufc.mdcc.hpc.storm.binding.channel.impl.BindingImpl
{
	public class IChannelImpl : BaseIChannelImpl, IChannel
	{
		public const int TAG_SEND_OPERATION = 999;

		private int conversation_tag = 100;

		object sync = new object();

		private int takeNextConversationTag()
		{
			lock (sync) 
			{
				conversation_tag ++;
				switch (conversation_tag) {
				case TAG_SEND_OPERATION:
					conversation_tag ++;
					break;
				case int.MaxValue:
					conversation_tag = 10;
					break;
				}
			}
			return /*100*this.PeerRank +*/ conversation_tag;
		}


		// AlienCommunicator

		public const int Root = default(int);
		public const int Null = default(int);

		public int RemoteSize
		{
			get
			{
				throw new NotImplementedException ();
			}
		}

		#region Point-to-Point implementation

		// Send

		public void Send<T> (T value, Tuple<int,int> dest, int tag)
		{
			//int conversation_tag = takeNextConversationTag();
			Console.WriteLine("CHECKING " + this.RootCommunicator.Rank + "," + dest.Item2 + ", tag=" + tag);

			Console.WriteLine (this.PeerRank + ": 1 - BEGIN SEND TO <" + dest.Item1 + "," + dest.Item2 + "> : " + TAG_SEND_OPERATION);
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.SEND, tag), 0, TAG_SEND_OPERATION);
			Console.WriteLine (this.PeerRank + ": 2 - BEGIN SEND TO <" + dest.Item1 + "," + dest.Item2 + "> : " + tag);

			byte[] value_packet = ObjectToByteArray (value);

			this.RootCommunicator.Send<Tuple<int,int,int,byte[]>> (new Tuple<int,int,int,byte[]>(dest.Item1, dest.Item2, tag, value_packet), 0, tag);
			Console.WriteLine (this.PeerRank + ": 3 - END SEND TO <" + dest.Item1 + "," + dest.Item2);
		}

		public br.ufc.mdcc.hpc.storm.binding.channel.Binding.Request ImmediateSend<T> (T value, Tuple<int,int> dest, int tag)
		{
			//int conversation_tag = takeNextConversationTag();
			Console.WriteLine(this.PeerRank + ": CHECKING " + this.RootCommunicator.Rank + "," + dest.Item2 + ", tag=" + tag);
			Console.WriteLine (this.PeerRank + ": 1 - BEGIN SEND TO <" + dest.Item1 + "," + dest.Item2 + "> : " + TAG_SEND_OPERATION);
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.SEND, tag), 0, TAG_SEND_OPERATION);
			Console.WriteLine (this.PeerRank + ": 2 - BEGIN SEND TO <" + dest.Item1 + "," + dest.Item2 + "> : " + tag);
			byte[] value_packet = ObjectToByteArray (value);
			MPI.Request root_request = this.RootCommunicator.ImmediateSend<Tuple<int,int,int,byte[]>> (new Tuple<int,int,int,byte[]>(dest.Item1, dest.Item2, tag, value_packet), 0, tag);
			Console.WriteLine (this.PeerRank + ": 3 - END SEND TO <" + dest.Item1 + "," + dest.Item2);
			return br.ufc.mdcc.hpc.storm.binding.channel.Binding.Request.createRequest (root_request, dest);
		}

		// Send Array

		public void Send<T> (T[] values, Tuple<int,int> dest, int tag)
		{
			//int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.SEND_ARRAY, tag), 0, TAG_SEND_OPERATION);
			byte[] value_packet = ObjectToByteArray (values);
			this.RootCommunicator.Send<Tuple<int,int,int,byte[]>> (new Tuple<int,int,int,byte[]>(dest.Item1, dest.Item2, tag, value_packet), 0, tag);
		}

		public br.ufc.mdcc.hpc.storm.binding.channel.Binding.Request ImmediateSend<T> (T[] values, Tuple<int,int> dest, int tag)
		{
			//int conversation_tag = takeNextConversationTag();
			Console.WriteLine(this.PeerRank + ": CHECKING " + this.RootCommunicator.Rank + "," + dest.Item2);
			Console.WriteLine (this.PeerRank + ": 1 - BEGIN SEND TO <" + dest.Item1 + "," + dest.Item2 + "> : " + TAG_SEND_OPERATION);
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.SEND_ARRAY, tag), 0, TAG_SEND_OPERATION);
			Console.WriteLine (this.PeerRank + ": 2 - BEGIN SEND TO <" + dest.Item1 + "," + dest.Item2 + "> : " + tag);
			byte[] value_packet = ObjectToByteArray (values);
			MPI.Request root_request = this.RootCommunicator.ImmediateSend<Tuple<int,int,int,byte[]>> (new Tuple<int,int,int,byte[]>(dest.Item1, dest.Item2, tag, value_packet), 0, tag);
			Console.WriteLine (this.PeerRank + ": 3 - END SEND TO <" + dest.Item1 + "," + dest.Item2 + ">");
			return br.ufc.mdcc.hpc.storm.binding.channel.Binding.Request.createRequest (root_request, dest);
		}

		// Receive

		public T Receive<T> (Tuple<int,int> source, int tag)
		{
			T result;
			Receive(source, tag, out result);
			return result;
		}

		public void Receive<T> (Tuple<int,int> source, int tag, out T value)
		{
			br.ufc.mdcc.hpc.storm.binding.channel.Binding.CompletedStatus status;
			Receive(source, tag, out value, out status);
		}

		public void Receive<T> (Tuple<int,int> source, int tag, out T value, out br.ufc.mdcc.hpc.storm.binding.channel.Binding.CompletedStatus status)
		{
			int conversation_tag = takeNextConversationTag();
			Console.WriteLine (this.PeerRank + ": 1 - BEGIN RECV FROM <" + source.Item1 + "," + source.Item2 + "> : " + TAG_SEND_OPERATION);
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.RECEIVE, conversation_tag), 0, TAG_SEND_OPERATION);
			Console.WriteLine (this.PeerRank + ": 2 - BEGIN RECV FROM <" + source.Item1 + "," + source.Item2 + "> : " + conversation_tag);
			this.RootCommunicator.Send<Tuple<int,int,int>> (new Tuple<int,int,int>(source.Item1, source.Item2, tag), 0, conversation_tag);
			Console.WriteLine (this.PeerRank + ": 3 - BEGIN RECV FROM <" + source.Item1 + "," + source.Item2 + "> : " + tag);
			MPI.CompletedStatus status_root;
			byte[] v;
			this.RootCommunicator.Receive<byte[]> (0, tag, out v, out status_root);
			value = (T) ByteArrayToObject(v);
			Console.WriteLine (this.PeerRank + ": 4 - END RECV FROM <" + source.Item1 + "," + source.Item2 + ">");

			status = br.ufc.mdcc.hpc.storm.binding.channel.Binding.CompletedStatus.createStatus(status_root, source);
		}

		public br.ufc.mdcc.hpc.storm.binding.channel.Binding.ReceiveRequest ImmediateReceive<T> (Tuple<int,int> source, int tag)
		{
			int conversation_tag = takeNextConversationTag();
			Console.WriteLine (this.PeerRank + ": 1 - BEGIN RECV FROM <" + source.Item1 + "," + source.Item2 + "> : " + TAG_SEND_OPERATION);
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.RECEIVE, conversation_tag), 0, TAG_SEND_OPERATION);
			Console.WriteLine (this.PeerRank + ": 2 - BEGIN RECV FROM <" + source.Item1 + "," + source.Item2 + "> : " + conversation_tag);
			this.RootCommunicator.Send<Tuple<int,int,int>> (new Tuple<int,int,int>(source.Item1, source.Item2, tag), 0, conversation_tag);
			Console.WriteLine (this.PeerRank + ": 3 - BEGIN RECV FROM <" + source.Item1 + "," + source.Item2 + "> : " + tag);
			MPI.ReceiveRequest root_request = this.RootCommunicator.ImmediateReceive<byte[]>(0, tag);
			Console.WriteLine (this.PeerRank + ": 4 - END RECV FROM <" + source.Item1 + "," + source.Item2 + ">");
			return br.ufc.mdcc.hpc.storm.binding.channel.Binding.ValueReceiveRequest<T>.createRequest(root_request, source);
		}

		// Receive Array

		public void Receive<T> (Tuple<int,int> source, int tag, ref T[] values)
		{
			br.ufc.mdcc.hpc.storm.binding.channel.Binding.CompletedStatus status;
			Receive(source, tag, ref values, out status);
		}

		public void Receive<T> (Tuple<int,int> source, int tag, ref T[] values, out br.ufc.mdcc.hpc.storm.binding.channel.Binding.CompletedStatus status)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.RECEIVE_ARRAY, conversation_tag), 0, TAG_SEND_OPERATION);
			this.RootCommunicator.Send<Tuple<int,int,int>> (new Tuple<int,int,int>(source.Item1, source.Item2, tag), 0, conversation_tag);
			MPI.CompletedStatus status_root;
			byte[] v;
			this.RootCommunicator.Receive<byte[]> (0, tag, out v, out status_root);
			T[] values_ = (T[]) ByteArrayToObject(v);

			// Copy the received values to the destination array (forcing original MPI semantics)
			int size = values.Length <= values_.Length ? values.Length : values_.Length;
			for (int i=0; i<size; i++)
				values[i] = values_[i];

			status = br.ufc.mdcc.hpc.storm.binding.channel.Binding.CompletedStatus.createStatus(status_root, source);

		}

		public br.ufc.mdcc.hpc.storm.binding.channel.Binding.ReceiveRequest ImmediateReceive<T> (Tuple<int,int> source, int tag, T[] values)
		{
			int conversation_tag = takeNextConversationTag();
			Console.WriteLine (this.PeerRank + ": 1 - BEGIN RECV FROM <" + source.Item1 + "," + source.Item2 + "> : " + TAG_SEND_OPERATION);
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.RECEIVE_ARRAY, conversation_tag), 0, TAG_SEND_OPERATION);
			Console.WriteLine (this.PeerRank + ": 2 - BEGIN RECV FROM <" + source.Item1 + "," + source.Item2 + "> : " + conversation_tag);
			this.RootCommunicator.Send<Tuple<int,int,int>> (new Tuple<int,int,int>(source.Item1, source.Item2, tag), 0, conversation_tag);
			Console.WriteLine (this.PeerRank + ": 3 - BEGIN RECV FROM <" + source.Item1 + "," + source.Item2 + "> : " + tag);
			MPI.ReceiveRequest root_request = this.RootCommunicator.ImmediateReceive<byte[]>(0, tag);
			Console.WriteLine (this.PeerRank + ": 4 - END RECV FROM <" + source.Item1 + "," + source.Item2 + ">");
			return br.ufc.mdcc.hpc.storm.binding.channel.Binding.ArrayReceiveRequest<T>.createRequest (root_request, source, values);
		}

		// Probe

		public br.ufc.mdcc.hpc.storm.binding.channel.Binding.Status Probe (Tuple<int,int> source, int tag)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.PROBE, conversation_tag), 0, TAG_SEND_OPERATION);
			throw new NotImplementedException ();
		}

		public br.ufc.mdcc.hpc.storm.binding.channel.Binding.Status ImmediateProbe (Tuple<int,int> source, int tag)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.PROBE, conversation_tag), 0, TAG_SEND_OPERATION);
			throw new NotImplementedException ();
		}

		#endregion

		#region Collective implementation


		// All Gather

		public T[] Allgather<T> (int facet, T value)
		{
			T[] result = null;
			Allgather(facet, value, ref result);
			return result;
		}

		public void Allgather<T> (int facet, T inValue, ref T[] outValues)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.ALL_GATHER, conversation_tag), 0, TAG_SEND_OPERATION);
			throw new NotImplementedException ();
		}
		 
		// All Gather Flattened

		public void AllgatherFlattened<T> (int facet, T[] inValues, int count, ref T[] outValues)
		{
			int[] counts = new int[Size];
			for (int i = 0; i < Size; i++)
				counts[i] = count;
			AllgatherFlattened(facet, inValues, counts, ref outValues);
		}

		public void AllgatherFlattened<T> (int facet, T[] inValues, int[] counts, ref T[] outValues)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.ALL_GATHER_FLATTENED, conversation_tag), 0, TAG_SEND_OPERATION);
			throw new NotImplementedException ();
		}

		// All Reduce

		public T Allreduce<T> (int facet, T value, ReductionOperation<T> op)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.ALL_REDUCE, conversation_tag), 0, TAG_SEND_OPERATION + 0);
			throw new NotImplementedException ();
		}

		// All Reduce Array

		public T[] Allreduce<T> (int facet, T[] values, ReductionOperation<T> op)
		{
			T[] result = null;
			Allreduce(facet, values, op, ref result);
			return result;
		}

		public void Allreduce<T> (int facet, T[] inValues, ReductionOperation<T> op, ref T[] outValues)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.ALL_REDUCE_ARRAY, conversation_tag), 0, TAG_SEND_OPERATION + 0);
			throw new NotImplementedException ();
		}

		// AllToAll

		public T[] Alltoall<T> (int facet, T[] values)
		{
			T[] result = null;
			Alltoall(facet, values, ref result);
			return result;
		}

		public void Alltoall<T> (int facet, T[] inValues, ref T[] outValues)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.ALL_TO_ALL, conversation_tag), 0, TAG_SEND_OPERATION);
			throw new NotImplementedException ();
		}

		// All-to-All Flattened

		public void AlltoallFlattened<T> (int facet, T[] inValues, int[] sendCounts, int[] recvCounts, ref T[] outValues)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.ALL_TO_ALL_FLATTENED, conversation_tag), 0, TAG_SEND_OPERATION + 0);
			throw new NotImplementedException ();
		}

		// Reduce-Scatter

		public T[] ReduceScatter<T> (int facet, T[] values, ReductionOperation<T> op, int[] counts)
		{
			T[] result = null;
			ReduceScatter(facet, values, op, counts, ref result);
			return result;
		}

		public void ReduceScatter<T> (int facet, T[] inValues, ReductionOperation<T> op, int[] counts, ref T[] outValues)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.REDUCE_SCATTER, conversation_tag), 0, TAG_SEND_OPERATION + 0);
			throw new NotImplementedException ();
		}

		// BROADCAST

		public void Broadcast<T> (int facet, ref T value, int root)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.BROADCAST, conversation_tag), 0, TAG_SEND_OPERATION + 0);
			throw new NotImplementedException ();
		}

		public void Broadcast<T> (int facet, ref T[] values, int root)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.BROADCAST_ARRAY, conversation_tag), 0, TAG_SEND_OPERATION);
			throw new NotImplementedException ();
		}

		// Scatter

		public void Scatter<T> (int facet, T[] values)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.SCATTER, conversation_tag), 0, TAG_SEND_OPERATION);
			throw new NotImplementedException ();
		}

		public T Scatter<T> (int facet, int root)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.SCATTER, conversation_tag), 0, TAG_SEND_OPERATION);
			throw new NotImplementedException ();
		}

		public void Scatter<T> (int facet)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.SCATTER, conversation_tag), 0, TAG_SEND_OPERATION);
			throw new NotImplementedException ();
		}

		// Scatter from Flattened

		public void ScatterFromFlattened<T> (int facet, T[] inValues, int count)
		{
			T[] temp = new T[0];
			ScatterFromFlattened<T>(facet, inValues, count, Root, ref temp);
		}

		public void ScatterFromFlattened<T> (int facet, T[] inValues, int[] counts)
		{
			T[] temp = new T[0];
			ScatterFromFlattened<T>(facet, inValues, counts, Root, ref temp);
		}

		public void ScatterFromFlattened<T> (int facet, int count, int root, ref T[] outValues)
		{
			ScatterFromFlattened<T>(facet, null, count, root, ref outValues);
		}

		public void ScatterFromFlattened<T> (int facet, int[] counts, int root, ref T[] outValues)
		{
			ScatterFromFlattened<T>(facet, null, counts, root, ref outValues);
		}

		public void ScatterFromFlattened<T> (int facet)
		{
			T[] temp = new T[0];
			ScatterFromFlattened<T>(facet, null, new int[0], Null, ref temp);
		}

		public void ScatterFromFlattened<T> (int facet, T[] inValues, int count, int root, ref T[] outValues)
		{
			int[] counts = new int[Size];
			for (int i = 0; i < Size; i++)
				counts[i] = count;
			ScatterFromFlattened<T>(facet, inValues, counts, root, ref outValues);
		}

		public void ScatterFromFlattened<T> (int facet, T[] inValues, int[] counts, int root, ref T[] outValues)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.SCATTER_FROM_FLATTENED, conversation_tag), 0, TAG_SEND_OPERATION + 0);
			throw new NotImplementedException ();
		}

		// Gather

		public T[] Gather<T> (int facet, T value, int root)
		{
			T[] result = null;
			Gather(facet, value, root, ref result);
			return result;
		}

		public T[] Gather<T> (int facet, int root)
		{
			T value = default(T);
			T[] result = null;
			Gather(facet, value, Root, ref result);
			return result;
		}

		public void Gather<T> (int facet)
		{
			T value = default(T);
			T[] result = null;
			Gather(facet, value, Null, ref result);
		}

		public void Gather<T> (int facet, T inValue, int root, ref T[] outValues)
		{
			Gather_impl<T>(facet, (root == Root), RemoteSize, inValue, root, ref outValues);
		}

		internal void Gather_impl<T>(int facet, bool isRoot, int size, T inValue, int root, ref T[] outValues)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.GATHER, conversation_tag), 0, TAG_SEND_OPERATION + 0);
			throw new NotImplementedException ();
		}

		// Gather Flattened

		public void GatherFlattened<T> (int facet, int count, ref T[] outValues)
		{
			if (outValues == null || outValues.Length != count * RemoteSize)
				outValues = new T[count * RemoteSize];


			int[] counts = new int[Size];
			for (int i = 0; i < Size; i++)
				counts[i] = count;

			GatherFlattened_impl<T>(facet, true, RemoteSize, new T[0], counts, Root, ref outValues);
		}

		public T[] GatherFlattened<T> (int facet, int count)
		{
			T[] outValues = new T[RemoteSize * count];

			int[] counts = new int[Size];
			for (int i = 0; i < Size; i++)
				counts[i] = count;

			GatherFlattened_impl<T>(facet, true, RemoteSize, new T[0], counts, Root, ref outValues);

			return outValues;
		}

		public void GatherFlattened<T> (int facet, T[] inValues, int root)
		{
			T[] temp = new T[0];
			GatherFlattened_impl<T>(facet, false, Size, inValues, new int[0], root, ref temp);
		}

		public void GatherFlattened<T> (int facet)
		{
			T[] temp = new T[0];
			GatherFlattened_impl<T>(facet, false, RemoteSize, new T[0], new int[0], Null, ref temp);
		}

		public void GatherFlattened<T> (int facet, int[] counts, ref T[] outValues)
		{
			int totalCounts = 0;
			for (int i = 0; i < counts.Length; i++)
				totalCounts += counts[i];

			if (outValues == null || outValues.Length != totalCounts * RemoteSize)
				outValues = new T[totalCounts * RemoteSize];

			GatherFlattened_impl<T>(facet, true, RemoteSize, new T[0], counts, Root, ref outValues);
		}

		public T[] GatherFlattened<T> (int facet, int[] counts)
		{ 
			int totalCounts = 0;
			for (int i = 0; i < counts.Length; i++)
				totalCounts += counts[i];
			T[] outValues = new T[totalCounts];

			GatherFlattened_impl<T>(facet, true, RemoteSize, new T[0], counts, Root, ref outValues);

			return outValues;

		}

		internal void GatherFlattened_impl<T>(int facet, bool isRoot, int size, T[] inValues, int[] counts, int root, ref T[] outValues)
		{ 
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.GATHER_FLATTENED, conversation_tag), 0, TAG_SEND_OPERATION + 0);
			throw new NotImplementedException ();
		}

		// Reduce

		public T Reduce<T> (int facet, T value, ReductionOperation<T> op, int root)
		{
			return Reduce_impl<T>(facet, (root == Root), RemoteSize, value, op, root);
		}

		internal T Reduce_impl<T>(int facet, bool isRoot, int size, T value, ReductionOperation<T> op, int root)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.REDUCE, conversation_tag), 0, TAG_SEND_OPERATION + 0);
			throw new NotImplementedException ();
		}

		// Reduce Array

		public T[] Reduce<T> (int facet, T[] values, ReductionOperation<T> op, int root)
		{
			T[] result = null;
			Reduce(facet, values, op, root, ref result);
			return result;
		}

		public void Reduce<T> (int facet, T[] inValues, ReductionOperation<T> op, int root, ref T[] outValues)
		{
			int conversation_tag = takeNextConversationTag();
			this.RootCommunicator.Send<Tuple<int /*AliencommunicatorOperation*/, int>>(new Tuple<int /*AliencommunicatorOperation*/, int>(AliencommunicatorOperation.REDUCE_ARRAY, conversation_tag), 0, TAG_SEND_OPERATION + 0);
			throw new NotImplementedException ();
		}

		#endregion

		/*
		public void SendReceive<T> (T inValue, int dest, int tag, out T outValue)
		{
			CompletedStatus status;
			SendReceive(inValue, dest, tag, dest, tag, out outValue, out status);
		}

		public void SendReceive<T> (T inValue, int dest, int sendTag, int source, int recvTag, out T outValue)
		{
			CompletedStatus status;
			SendReceive(inValue, dest, sendTag, source, recvTag, out outValue, out status);
		}

		public void SendReceive<T> (T inValue, int dest, int sendTag, int source, int recvTag, out T outValue, out CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		public void SendReceive<T> (T[] inValues, int dest, int tag, ref T[] outValues)
		{
			CompletedStatus status;
			SendReceive(inValues, dest, tag, dest, tag, ref outValues, out status);
		}

		public void SendReceive<T> (T[] inValues, int dest, int sendTag, int source, int recvTag, ref T[] outValues)
		{
			CompletedStatus status;
			SendReceive(inValues, dest, sendTag, source, recvTag, ref outValues, out status);
		}

		public void SendReceive<T> (T[] inValues, int dest, int sendTag, int source, int recvTag, ref T[] outValues, out CompletedStatus status)
		{
			throw new NotImplementedException ();
		}
       */

		// Convert an object to a byte array
		private static byte[] ObjectToByteArray(Object obj)
		{
			if(obj == null)
				return null;
			BinaryFormatter bf = new BinaryFormatter();
			MemoryStream ms = new MemoryStream();
			bf.Serialize(ms, obj);
			return ms.ToArray();
		}

		private static Object ByteArrayToObject(byte[] arrBytes)
		{
			MemoryStream memStream = new MemoryStream();
			BinaryFormatter binForm = new BinaryFormatter();
			memStream.Write(arrBytes, 0, arrBytes.Length);
			memStream.Seek(0, SeekOrigin.Begin);
			Object obj = (Object) binForm.Deserialize(memStream);
			return obj;
		}
	}


}
