using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using MPI;
using System.Threading;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using EnvelopType = System.Tuple<int /*AliencommunicatorOperation*/,int,int,int,int,int>;

namespace br.ufc.mdcc.hpc.storm.binding.channel.impl.BindingImpl
{
	public class IChannelRootImpl : BaseIChannelRootImpl, IChannelRoot
	{
		public const int TAG_SEND_OPERATION = 999;

		private Thread[] thread_receive_requests = null;

		#region implemented abstract members of BindingRoot
		public override void server ()
		{
			createSockets ();

			Console.WriteLine ("CREATED SOCKETS !!!");

			synchronizer_monitor = new SynchronizerMonitor (this.ThisFacet, client_socket_facet);
			//sockets_initialized_flag = true;
			sockets_initialized_flag.Set ();

			// Create the threads that will listen the sockets for each other facet.
			thread_receive_requests = new Thread[server_socket_facet.Count];
			for (int facet=0; facet < server_socket_facet.Count; facet ++) 
			{
				if (facet != this.ThisFacet)
				{
					Socket server_socket = server_socket_facet [facet];
					thread_receive_requests[facet] = new Thread (new ThreadStart (() => synchronizer_monitor.serverReceiveRequests(server_socket)));
					thread_receive_requests[facet].Start ();
				}
			}

			while (true) 
			{
				synchronizer_monitor.serverReadRequest ();
			}

		}

		private AutoResetEvent sockets_initialized_flag = new AutoResetEvent(false);

		public override void client ()
		{
			//while (!sockets_initialized_flag)	Thread.Sleep (100);
			sockets_initialized_flag.WaitOne ();

			Console.WriteLine ("GO LISTEN WORKERS !!!");
			while (true) 
			{
			   listen_worker ();
			}
		}
		#endregion

		SynchronizerMonitor synchronizer_monitor;

		private IDictionary<int, Socket> client_socket_facet = new Dictionary<int, Socket>();
		private IDictionary<int, Socket> server_socket_facet = new Dictionary<int, Socket>();

		private void connectSockets()
		{
			foreach (KeyValuePair<int, FacetAccess> facet_access in this.Facet) 
			{
				int facet = facet_access.Key;
				if (facet != this.ThisFacet)
				{
					Socket socket = client_socket_facet [facet];
					IPEndPoint endPoint = end_point [facet];

					bool isConnected = false;
					int tries = 0;
					while (!isConnected && tries <=30) 
					{
						try {
							Console.WriteLine ("CONNECTING " + endPoint);
							socket.Connect (endPoint);
							isConnected = true;
							Console.WriteLine ("CONNECTED " + endPoint);
						}
						catch (Exception e) 
						{
							tries ++;
							isConnected = false;
							Console.WriteLine("CONNECTION FAILED N --- ATTEMPT #" + tries + " *** " + e.Message);
							Thread.Sleep(1000);
						}
					}
					if (!isConnected) {
						Console.WriteLine ("createSockets --- It was not possible to talk to the server");
						throw new Exception ("createSockets --- It was not possible to talk to the server");
					}
				}
			}
		}

		private void acceptSockets ()
		{
			foreach (KeyValuePair<int, FacetAccess> facet_access in this.Facet) 
			{
				int facet = facet_access.Key;
				if (facet != this.ThisFacet) 
				{
					Socket socket = server_socket_facet [facet];
					IPEndPoint endPoint = end_point [this.ThisFacet];
					Console.WriteLine ("BINDING " + endPoint);
					socket.Bind (endPoint);
					socket.Listen (10);
					server_socket_facet[facet] = socket.Accept ();
					Console.WriteLine ("BINDED " + endPoint);
				}
			}
		}

		private IDictionary<int,IPEndPoint> end_point = new Dictionary<int,IPEndPoint>();


		private void createSockets ()
		{
			foreach (KeyValuePair<int, FacetAccess> facet_access in this.Facet)
			{
				int facet = facet_access.Key;
				string ip_address = facet_access.Value.ip_address;
				int port = facet_access.Value.port;

				// Establish the remote endpoint for the socket.
				IPHostEntry ipHostInfo = Dns.GetHostEntry(ip_address);
				IPAddress ipAddress = ipHostInfo.AddressList[0];
				IPEndPoint endPoint = new IPEndPoint(ipAddress,port);

				end_point [facet] = endPoint;

				if (facet != this.ThisFacet) 
				{
					Console.WriteLine ("CREATE SOCKETS facet=" + facet + ", port=" + port + ", ip_address=" + ip_address);

					// Create a TCP/IP client socket.
					Socket client_socket = new Socket (AddressFamily.InterNetwork, 
					                                 SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

					// Create a TCP/IP server socket.
					Socket server_socket = new Socket (AddressFamily.InterNetwork, 
					                                 SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

					client_socket_facet [facet] = client_socket;
					server_socket_facet [facet] = server_socket;
				}
			}

			Thread thread_connect_sockets = new Thread (new ThreadStart (connectSockets));
			Thread thread_accept_sockets = new Thread (new ThreadStart (acceptSockets));

			thread_connect_sockets.Start();
			thread_accept_sockets.Start ();

			thread_connect_sockets.Join ();
			thread_accept_sockets.Join ();
		}			



		private void listen_worker ()
		{
			Tuple<int /*AliencommunicatorOperation*/,int> operation;
			MPI.CompletedStatus status = null;

			Console.WriteLine ("listen_workers - WAITING ... " + MPI.Environment.Threading);

			RootCommunicator.Receive<Tuple<int /*AliencommunicatorOperation*/,int>>
									(MPI.Communicator.anySource, 
									 TAG_SEND_OPERATION,
									 out operation,
									 out status);

			Console.WriteLine ("listen_workers - RECEIVED FROM WORKER source=" + status.Source + ", tag=" + status.Tag + " / operation = " + operation);

			switch (operation.Item1) 
			{
				case AliencommunicatorOperation.SEND:
				case AliencommunicatorOperation.SEND_ARRAY:
					(new Thread(() => handle_SEND (operation, status))).Start();
					break;
				case AliencommunicatorOperation.RECEIVE:
				case AliencommunicatorOperation.RECEIVE_ARRAY:
					(new Thread(() => handle_RECEIVE (operation, status))).Start();
					break;
				case AliencommunicatorOperation.PROBE:
					(new Thread(() => handle_PROBE(operation, status))).Start();
					break;
				case AliencommunicatorOperation.ALL_GATHER:
					(new Thread(() => handle_ALL_GATHER(operation, status))).Start();
					break;
				case AliencommunicatorOperation.ALL_GATHER_FLATTENED:
					(new Thread(() => handle_ALL_GATHER_FLATTENED(operation, status))).Start();
					break;
				case AliencommunicatorOperation.ALL_REDUCE:
					handle_ALL_REDUCE(operation, status);
					break;
				case AliencommunicatorOperation.ALL_REDUCE_ARRAY:
					(new Thread(() => handle_ALL_REDUCE(operation, status))).Start();
					break;
				case AliencommunicatorOperation.ALL_TO_ALL:
					(new Thread(() => handle_ALL_TO_ALL(operation, status))).Start();
					break;
				case AliencommunicatorOperation.ALL_TO_ALL_FLATTENED:
					(new Thread(() => handle_ALL_TO_ALL_FLATTENED(operation, status))).Start();
					break;
				case AliencommunicatorOperation.REDUCE_SCATTER:
					(new Thread(() => handle_REDUCE_SCATTER(operation, status))).Start();
					break;
				case AliencommunicatorOperation.BROADCAST:
					(new Thread(() => handle_BROADCAST(operation, status))).Start();
					break;
				case AliencommunicatorOperation.BROADCAST_ARRAY:
					(new Thread(() => handle_BROADCAST(operation, status))).Start();
					break;
				case AliencommunicatorOperation.SCATTER:
					handle_SCATTER(operation, status);
					break;
				case AliencommunicatorOperation.SCATTER_FROM_FLATTENED:
					(new Thread(() => 	handle_SCATTER_FROM_FLATTENED(operation, status))).Start();
					break;
				case AliencommunicatorOperation.GATHER:
					(new Thread(() => handle_GATHER(operation, status))).Start();
					break;
				case AliencommunicatorOperation.GATHER_FLATTENED:
					(new Thread(() => handle_GATHER_FLATTENED(operation, status))).Start();
					break;
				case AliencommunicatorOperation.REDUCE:
					(new Thread(() => handle_REDUCE(operation, status))).Start();
					break;
				case AliencommunicatorOperation.REDUCE_ARRAY:
					(new Thread(() => handle_REDUCE(operation, status))).Start();
					break;
				default:
					Console.WriteLine ("UNRECOGNIZED OPERATION");
					throw new ArgumentOutOfRangeException ();
			}
		}

		void handle_SEND (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{

			Console.WriteLine (status.Source + ": handle_SEND 1");

			Tuple<int,int,int,byte[]> operation_info;
			int conversation_tag = operation.Item2;
			this.RootCommunicator.Receive<Tuple<int,int,int,byte[]>> (status.Source, conversation_tag, out operation_info);

			Console.WriteLine (status.Source + ": handle_SEND 2");

			int /*AliencommunicatorOperation*/ operation_type = operation.Item1;
			int facet_src = this.ThisFacet;
			int facet_dst = operation_info.Item1;
			int src = status.Source;
			int dst = operation_info.Item2;
			int tag = operation_info.Item3;

			Console.WriteLine (status.Source + ": handle_SEND 3 --- " + facet_src + "," + facet_dst + "," + src + "," + dst + "," + tag);

			EnvelopType envelop = new EnvelopType (operation_type,facet_src,facet_dst,src,dst,tag);
			byte[] message1 = operation_info.Item4;
			Console.WriteLine (status.Source + ": handle_SEND 4");

			synchronizer_monitor.clientSendRequest (envelop, message1);
			Console.WriteLine (status.Source + ": handle_SEND 5");

		}

		void handle_RECEIVE (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			int conversation_tag = operation.Item2;
			Console.WriteLine (status.Source + ": handle_RECEIVE 1 - source=" + status.Source + ", tag=" + conversation_tag);
			Tuple<int,int,int> operation_info;

			this.RootCommunicator.Receive<Tuple<int,int,int>> (status.Source, conversation_tag, out operation_info);

			Console.WriteLine (status.Source + ": handle_RECEIVE 2");

			int /*AliencommunicatorOperation*/ operation_type = operation.Item1;
			int facet_src = this.ThisFacet;
			int facet_dst = operation_info.Item1;
			int src = status.Source;
			int dst = operation_info.Item2;
			int tag = operation_info.Item3;

			Console.WriteLine (status.Source + ": handle_RECEIVE 3 --- " + facet_src + "," + facet_dst + "," + src + "," + dst + "," + tag);

			EnvelopType envelop = new EnvelopType (operation_type,facet_src,facet_dst,src,dst,tag);
			byte[] message2 = synchronizer_monitor.clientSendRequest (envelop, new byte[0]);

			Console.WriteLine (status.Source + ": handle_RECEIVE 4 " + (message2 == null));

			this.RootCommunicator.Send<byte[]>(message2, src, tag);

			Console.WriteLine (status.Source + ": handle_RECEIVE 5");

		}

		void handle_PROBE (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_ALL_GATHER (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_ALL_GATHER_FLATTENED (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_ALL_REDUCE (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_ALL_TO_ALL (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_ALL_TO_ALL_FLATTENED (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_REDUCE_SCATTER (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_BROADCAST (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_SCATTER (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_SCATTER_FROM_FLATTENED (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_GATHER (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_GATHER_FLATTENED (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_REDUCE (Tuple<int /*AliencommunicatorOperation*/, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		#region IDisposable implementation
		private bool disposed = false;

		protected override void Dispose(bool disposing)
		{

			if (!disposed)
			{
				if (disposing)
				{
					Console.WriteLine ("DISPOSING BINDING ROOT ...");
					for (int i=0; i<thread_receive_requests.Length; i++)
						if (i != this.ThisFacet)
						   thread_receive_requests[i].Abort ();
				}
				base.Dispose (disposing);
			}
			//dispose unmanaged resources
			disposed = true;
		}

		#endregion

	}

	class SynchronizerMonitor
	{
		private int server_facet = default(int);
		private IDictionary<int, Socket> client_socket_facet = new Dictionary<int, Socket>();
		private IDictionary<EnvelopKey, Queue<byte[]>> reply_pending_list = new Dictionary<EnvelopKey,Queue<byte[]>>();
		private IDictionary<EnvelopKey, AutoResetEvent> request_pending_list = new Dictionary<EnvelopKey,AutoResetEvent>();

		private object sync = new object();

		public SynchronizerMonitor (int server_facet, IDictionary<int, Socket> client_socket_facet)
		{
			this.server_facet = server_facet;
			this.client_socket_facet = client_socket_facet;
		}

		public byte[] clientSendRequest(EnvelopType envelop, byte[] messageSide1)
		{
			EnvelopKey envelop_key = new EnvelopKey (envelop);
			Console.WriteLine ("clientSendRequest 1" + " / "  + envelop_key);

			byte[] messageSide2 = null;
			Monitor.Enter (sync);
			try
			{
				// envia a requisição para o root parceiro
				int facet = envelop.Item3;
				Console.WriteLine("clientSendRequest send to facet " + facet + " - nofsockets=" + client_socket_facet.Count + " / "  + envelop_key);
				Socket socket = client_socket_facet [facet];
				byte[] messageSide1_enveloped_raw = ObjectToByteArray (new Tuple<EnvelopType,byte[]> (envelop, messageSide1));
				Int32 length = messageSide1_enveloped_raw.Length;
				byte[] messageSide1_enveloped_raw_ = new byte[4 + length];
				BitConverter.GetBytes(length).CopyTo(messageSide1_enveloped_raw_,0);
				Array.Copy(messageSide1_enveloped_raw, 0, messageSide1_enveloped_raw_, 4, length);

				socket.Send (messageSide1_enveloped_raw_);

				Console.WriteLine ("clientSendRequest 2 nbytes=" + messageSide1_enveloped_raw.Length + " / "  + envelop_key);

				// Verifica se já há resposta para a requisição no "conjunto de respostas pendentes de requisição"
				if (!reply_pending_list.ContainsKey (envelop_key) || 
				          (reply_pending_list.ContainsKey (envelop_key) && reply_pending_list [envelop_key].Count == 0)) 
				{
					Console.WriteLine ("clientSendRequest 3 - BEFORE WAIT " + envelop_key);
					// Se não houver, coloca um item no "conjunto de requisições pendentes de resposta" e espera.

					if (!request_pending_list.ContainsKey(envelop_key))
						request_pending_list [envelop_key] = new AutoResetEvent(false);

					AutoResetEvent sync_send = request_pending_list [envelop_key];

					request_pending_list [envelop_key] = sync_send;
					Monitor.Exit(sync);
					sync_send.WaitOne();
					Monitor.Enter(sync);
					Console.WriteLine ("clientSendRequest 3 - AFTER WAIT " + envelop_key);
				}
				Console.WriteLine ("clientSendRequest 4" + " / "  + envelop_key);

				Queue<byte[]> pending_replies = reply_pending_list [envelop_key];
				Console.WriteLine ("clientSendRequest 5 -- pending_replies.Count = " + pending_replies.Count);
				if (pending_replies.Count > 0)
				   messageSide2 = reply_pending_list[envelop_key].Dequeue();

				//reply_pending_list.Remove(envelop_key);
			}
			finally 
			{
				Monitor.Exit (sync);
			}

			Console.WriteLine ("clientSendRequest 5");
			// retorna a menagem ...
			return messageSide2;
		}

		private static int BUFFER_SIZE = 64000;

		public void serverReceiveRequests(Socket server_socket)
		{
			//Socket socket = client_socket_facet [server_facet];
			byte[] buffer = new byte[BUFFER_SIZE];
			byte[] buffer2 = new byte[BUFFER_SIZE];
			int nbytes = server_socket.Receive (buffer);		    

			Console.WriteLine ("serverReceiveRequests 1 - RECEIVED " + nbytes + " bytes");

			while (true)
			{
				int length = BitConverter.ToInt32(buffer,0);
				nbytes = nbytes - 4;
				byte[] message = new byte[length];

				Console.WriteLine ("serverReceiveRequests 2 - length is " + length + " bytes");

				//while (nbytes < length) 
				//	nbytes += socket.Receive (buffer);

				Array.Copy (buffer, 4, message, 0, length);
				requestQueue.Add (message);

				if (nbytes == length) 
				{
					nbytes = server_socket.Receive (buffer);
					Console.WriteLine ("serverReceiveRequests 3 - RECEIVED " + nbytes + " bytes");
				} 
				else // nbytes > length 
				{ 
					// assume that nbytes - length > 4
					byte[] aux = buffer;
					nbytes = nbytes - length;
					Array.Copy(buffer, length + 4, buffer2, 0, nbytes);
					buffer = buffer2;
					buffer2 = aux;
					Console.WriteLine ("serverReceiveRequests 4 - nbytes=" + nbytes);
				}
			}

		}


		private ProducerConsumerQueue<byte[]> requestQueue = new ProducerConsumerQueue<byte[]>();

		public void serverReadRequest() 
		{
			Console.WriteLine ("clientReceiveRequest 1 " + server_facet);

			byte[] buffer =	requestQueue.Take ();
			int nbytes =  buffer.Length;

			Console.WriteLine ("clientReceiveRequest 2 " + nbytes + " bytes received.");

			Monitor.Enter (sync);
			try
			{
				// Aguarda uma resposta proveniente do outro root parceiro.
				byte[] messageSide1_enveloped_raw = new byte[nbytes];
				Console.WriteLine ("clientReceiveRequest 2-1 nbytes=" + nbytes);

				// TODO: otimizar isso ...
				for (int i=0; i<nbytes; i++)
					messageSide1_enveloped_raw[i] = buffer[i];
				Tuple<EnvelopType,byte[]> messageSide1_enveloped = (Tuple<EnvelopType,byte[]>) ByteArrayToObject (messageSide1_enveloped_raw);

				EnvelopType envelop = messageSide1_enveloped.Item1;
				EnvelopKey envelop_key = new EnvelopKey (envelop);

				// Coloca a resposta no "conjunto de respostas pendentes de requisição"
				if (!reply_pending_list.ContainsKey(envelop_key))
					reply_pending_list [envelop_key] = new Queue<byte[]>();

				reply_pending_list [envelop_key].Enqueue(messageSide1_enveloped.Item2);

				Console.WriteLine ("clientReceiveRequest 3 " + envelop.Item1 + "," +  envelop_key);
				foreach (EnvelopKey ek in request_pending_list.Keys) 
					Console.WriteLine("key: " + ek);

				// Busca, no "conjunto de requisições pendentes de resposta", a requisição correspondente a resposta.
				if (request_pending_list.ContainsKey (envelop_key)) 
				{
					Console.WriteLine ("clientReceiveRequest 3-1" + " / "  + envelop_key);
					AutoResetEvent sync_send = request_pending_list[envelop_key];
					//Monitor.Pulse (sync_send);
					sync_send.Set();
					request_pending_list.Remove(envelop_key);
					Console.WriteLine ("clientReceiveRequest 3-2"+ " / "  + envelop_key) ;
				}
			}
			finally 
			{
				Monitor.Exit (sync);
			}
			Console.WriteLine ("clientReceiveRequest 4");
		}

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

		// Convert a byte array to an Object
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

	class EnvelopKey
	{
		private EnvelopType envelop = null;
		public EnvelopKey(EnvelopType envelop)
		{
			this.envelop = envelop;
		}

		public override string ToString ()
		{
			string key=base.ToString();
			switch (envelop.Item1) {
			case AliencommunicatorOperation.SEND:
			case AliencommunicatorOperation.SEND_ARRAY:
				key = string.Format ("SR-{0}-{1}-{2}-{3}-{4}",envelop.Item2, envelop.Item3, envelop.Item4, envelop.Item5, envelop.Item6);
				break;
			case AliencommunicatorOperation.RECEIVE:
			case AliencommunicatorOperation.RECEIVE_ARRAY:
				key = string.Format ("SR-{1}-{0}-{3}-{2}-{4}",envelop.Item2, envelop.Item3, envelop.Item4, envelop.Item5, envelop.Item6);
				break;
			case AliencommunicatorOperation.PROBE:
				break;
			case AliencommunicatorOperation.ALL_GATHER:
				break;
			case AliencommunicatorOperation.ALL_GATHER_FLATTENED:
				break;
			case AliencommunicatorOperation.ALL_REDUCE:
				break;
			case AliencommunicatorOperation.ALL_REDUCE_ARRAY:
				break;
			case AliencommunicatorOperation.ALL_TO_ALL:
				break;
			case AliencommunicatorOperation.ALL_TO_ALL_FLATTENED:
				break;
			case AliencommunicatorOperation.REDUCE_SCATTER:
				break;
			case AliencommunicatorOperation.BROADCAST:
				break;
			case AliencommunicatorOperation.BROADCAST_ARRAY:
				break;
			case AliencommunicatorOperation.SCATTER:
				break;
			case AliencommunicatorOperation.SCATTER_FROM_FLATTENED:
				break;
			case AliencommunicatorOperation.GATHER:
				break;
			case AliencommunicatorOperation.GATHER_FLATTENED:
				break;
			case AliencommunicatorOperation.REDUCE:
				break;
			case AliencommunicatorOperation.REDUCE_ARRAY:
				break;
			default:
				throw new ArgumentOutOfRangeException ();
			}

			return key;
		}

		public override bool Equals(object obj)
		{
			EnvelopKey fooItem = obj as EnvelopKey;

			return fooItem.ToString().Equals(this.ToString());
		}

		public override int GetHashCode ()
		{
			return this.ToString().GetHashCode();
		}

	}

	public class ProducerConsumerQueue<T> : BlockingCollection<T>
	{
		/// <summary>
		/// Initializes a new instance of the ProducerConsumerQueue, Use Add and TryAdd for Enqueue and TryEnqueue and Take and TryTake for Dequeue and TryDequeue functionality
		/// </summary>
		public ProducerConsumerQueue()  
			: base(new ConcurrentQueue<T>())
		{
		}

		/// <summary>
		/// Initializes a new instance of the ProducerConsumerQueue, Use Add and TryAdd for Enqueue and TryEnqueue and Take and TryTake for Dequeue and TryDequeue functionality
		/// </summary>
		/// <param name="maxSize"></param>
		public ProducerConsumerQueue(int maxSize)
			: base(new ConcurrentQueue<T>(), maxSize)
		{
		}



	}

}
