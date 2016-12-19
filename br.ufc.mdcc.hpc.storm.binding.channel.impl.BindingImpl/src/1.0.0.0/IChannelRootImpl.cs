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

using EnvelopType = System.Tuple<int,int,int,int,int,int>;
using System.Diagnostics;

namespace br.ufc.mdcc.hpc.storm.binding.channel.impl.BindingImpl
{
	public class IChannelRootImpl : BaseIChannelRootImpl, IChannelRoot
	{
		public const int TAG_SEND_OPERATION = 999;
			
		private IDictionary<int,Thread> thread_receive_requests = null;

		#region implemented abstract members of BindingRoot
		public override void server ()
		{
			this.TraceFlag = true;

			Trace.WriteLineIf(this.TraceFlag==true, this.GlobalRank + ": BEFORE CREATE SOCKETS !!! " + this.ThisFacet + " / " + this.ThisFacetInstance + " : " + this.CID.getInstanceName());

			createSockets ();

			Trace.WriteLineIf(this.TraceFlag==true, this.GlobalRank + ": AFTER CREATED SOCKETS !!! " + this.ThisFacet + " / " + this.ThisFacetInstance + " : " + this.CID.getInstanceName());

			synchronizer_monitor = new SynchronizerMonitor (this, client_socket_facet, this.ThisFacetInstance, this.GlobalRank, this.CID.getInstanceName());

			sockets_initialized_flag.Set ();

			// Create the threads that will listen the sockets for each other facet.
			thread_receive_requests = new Dictionary<int, Thread>();

			foreach (int facet in this.Facet.Keys) 
			{
				if (facet != this.ThisFacetInstance)
			    {
					Trace.WriteLineIf(this.TraceFlag==true, "loop create thread_receive_requests: " + facet + " / " + this.ThisFacetInstance);
					Socket server_socket = server_socket_facet [facet];
					thread_receive_requests[facet] = new Thread (new ThreadStart (() => synchronizer_monitor.serverReceiveRequests(facet, server_socket)));
					thread_receive_requests[facet].Start ();
				}
			}

			while (true) 
			{
				Thread.Sleep (100);
				synchronizer_monitor.serverReadRequest ();
			}

		}

		private AutoResetEvent sockets_initialized_flag = new AutoResetEvent(false);

		public override void client ()
		{
			this.TraceFlag = true;

			//while (!sockets_initialized_flag)	Thread.Sleep (100);
			sockets_initialized_flag.WaitOne ();

			Trace.WriteLineIf(this.TraceFlag==true, "GO LISTEN WORKERS !!!");
			while (true) 
			{
			   listen_worker ();
			}
		}
		#endregion

		private SynchronizerMonitor synchronizer_monitor;

		private IDictionary<int, Socket> client_socket_facet = new Dictionary<int, Socket>();
		private IDictionary<int, Socket> server_socket_facet = new Dictionary<int, Socket>();

		private void connectSockets()
		{
			foreach (KeyValuePair<int, FacetAccess> facet_access in this.Facet) 
			{
				int facet = facet_access.Key;
				if (facet != this.ThisFacetInstance)
				{
					Socket socket = client_socket_facet [facet];
					IPEndPoint endPoint = end_point_client [facet];

					bool isConnected = false;
					int tries = 0;
					while (!isConnected && tries <=30) 
					{
						try {
							//Trace.WriteLineIf(this.TraceFlag==true, "CONNECTING " + "facet=" + facet + " / " + endPoint+ " / " + this.CID.getInstanceName());
							socket.Connect (endPoint);
							isConnected = true;
							//Trace.WriteLineIf(this.TraceFlag==true, "CONNECTED " + "facet=" + facet + " / " + endPoint+ " / " + this.CID.getInstanceName());
						}
						catch (Exception e) 
						{
							//tries ++;
							//isConnected = false;
							//Trace.WriteLineIf(this.TraceFlag==true, "CONNECTION FAILED N --- ATTEMPT #" + tries + " *** " + e.Message + " --- " + endPoint);
							Thread.Sleep(1000);
						}
					}
					if (!isConnected) {
						Trace.WriteLineIf(this.TraceFlag==true, "createSockets --- It was not possible to talk to the server");
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
				if (facet != this.ThisFacetInstance) 
				{
					Socket socket = server_socket_facet [facet];
					IPEndPoint endPoint = end_point_server [facet];
					Trace.WriteLineIf(this.TraceFlag==true, "BINDING " + endPoint + " -- " + facet + " / " + this.CID.getInstanceName());
					socket.Bind (endPoint) ;
					socket.Listen (10);
					server_socket_facet[facet] = socket.Accept ();
					Trace.WriteLineIf(this.TraceFlag==true, "BINDED " + endPoint + " -- " + facet + " / " + this.CID.getInstanceName());
				}
			}
		}

		private IDictionary<int,IPEndPoint> end_point_client = new Dictionary<int,IPEndPoint>();
		private IDictionary<int,IPEndPoint> end_point_server = new Dictionary<int,IPEndPoint>();


		private void createSockets ()
		{
			FacetAccess facet_acess_server = this.Facet [ThisFacetInstance];

			foreach (KeyValuePair<int, FacetAccess> facet_access_client in this.Facet)
			{
				int facet_instance = facet_access_client.Key;
				if (facet_instance != this.ThisFacetInstance) 
				{
					string ip_address_client = facet_access_client.Value.ip_address;
					int port_client = facet_access_client.Value.port + this.ThisFacetInstance;
					IPHostEntry ipHostInfo_client = Dns.GetHostEntry (ip_address_client);
					IPAddress ipAddress_client = ipHostInfo_client.AddressList [0];
					IPEndPoint endPoint_client = new IPEndPoint (ipAddress_client, port_client);
					end_point_client [facet_instance] = endPoint_client;

					Trace.WriteLineIf (this.TraceFlag == true, "CREATE SOCKETS - end_point_client[" + facet_instance + "]=" + endPoint_client);

					string ip_address_server = facet_acess_server.ip_address;
					int port_server = facet_acess_server.port + facet_instance;
					IPHostEntry ipHostInfo_server = Dns.GetHostEntry (ip_address_server);
					IPAddress ipAddress_server = ipHostInfo_server.AddressList [0];
					IPEndPoint endPoint_server = new IPEndPoint (ipAddress_server, port_server);
					end_point_server [facet_instance] = endPoint_server;

					Trace.WriteLineIf (this.TraceFlag == true, "CREATE SOCKETS - end_point_server[" + facet_instance + "]=" + endPoint_server);

					// Create a TCP/IP client socket.
					Socket client_socket = new Socket (AddressFamily.InterNetwork, 
						                       SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

					// Create a TCP/IP server socket.
					Socket server_socket = new Socket (AddressFamily.InterNetwork, 
						                       SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

					server_socket.SendTimeout = server_socket.ReceiveTimeout = client_socket.SendTimeout = client_socket.ReceiveTimeout = -1;
					client_socket_facet [facet_instance] = client_socket;
					server_socket_facet [facet_instance] = server_socket;
				}
			}

			Thread thread_connect_sockets = new Thread (new ThreadStart (connectSockets));
			Thread thread_accept_sockets = new Thread (new ThreadStart (acceptSockets));

			thread_connect_sockets.Start();
			thread_accept_sockets.Start ();

			Trace.WriteLineIf(this.TraceFlag==true, "CREATE_SOCKETS - connectSockets and acceptSockets launched !!!");

			thread_connect_sockets.Join ();
			thread_accept_sockets.Join ();

			Trace.WriteLineIf(this.TraceFlag==true, "CREATE_SOCKETS - connectSockets and acceptSockets finished !!!");
		}			



		private void listen_worker ()
		{
			Tuple<int,int> operation;
			MPI.CompletedStatus status = null;

			Trace.WriteLineIf(this.TraceFlag==true, "listen_workers - WAITING ... " + MPI.Environment.Threading);

			//lock (lock_recv)
				RootCommunicator.Receive<Tuple<int,int>>
									(MPI.Communicator.anySource, 
									 TAG_SEND_OPERATION,
									 out operation,
									 out status);

			Trace.WriteLineIf(this.TraceFlag==true, "listen_workers - RECEIVED FROM WORKER source=" + status.Source + ", tag=" + status.Tag + " / operation = " + operation);

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
					Trace.WriteLineIf(this.TraceFlag==true, "UNRECOGNIZED OPERATION");
					throw new ArgumentOutOfRangeException ();
			}
		}

		void handle_SEND (Tuple<int, int> operation, MPI.CompletedStatus status)
		{

			Trace.WriteLineIf(this.TraceFlag==true, status.Source + ": handle_SEND 1 " + operation);

			Tuple<int,int,int,byte[]> operation_info;
			int conversation_tag = operation.Item2;
			//lock (lock_recv) 
				this.RootCommunicator.Receive<Tuple<int,int,int,byte[]>> (status.Source, conversation_tag, out operation_info);

			Trace.WriteLineIf(this.TraceFlag==true, status.Source + ": handle_SEND 2 --- operation = " + operation);

			int operation_type = operation.Item1;
			int facet_src = this.ThisFacetInstance;
			int facet_dst = operation_info.Item1;
			int src = status.Source;
			int dst = operation_info.Item2;
			int tag = operation_info.Item3;

			Trace.WriteLineIf(this.TraceFlag==true, status.Source + ": handle_SEND 3 --- " + facet_src + "," + facet_dst + "," + src + "," + dst + "," + tag + ", operation = " + operation);

			EnvelopType envelop = new EnvelopType (operation_type, facet_src, facet_dst, src, dst, tag);
			byte[] message1 = operation_info.Item4;
			Trace.WriteLineIf(this.TraceFlag==true, status.Source + ": handle_SEND 4 --- operation = " + operation);

			if (tag >=0 /* tag */)
				synchronizer_monitor.clientSendRequest (envelop, message1);
			else
				synchronizer_monitor.clientSendRequestAnyTag (envelop, message1, ref tag);
			Trace.WriteLineIf(this.TraceFlag==true, status.Source + ": handle_SEND 5 --- operation = " + operation);

		}

		//private object lock_recv = new object();

		void handle_RECEIVE (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			int conversation_tag = operation.Item2;
			Trace.WriteLineIf(this.TraceFlag==true, status.Source + ": handle_RECEIVE 1 - source=" + status.Source + ", tag=" + conversation_tag);
			Tuple<int,int,int> operation_info;

			//lock (lock_recv)
				this.RootCommunicator.Receive<Tuple<int,int,int>> (status.Source, conversation_tag, out operation_info);

			Trace.WriteLineIf(this.TraceFlag==true, status.Source + ": handle_RECEIVE 2");

			int operation_type = operation.Item1;
			int facet_src = this.ThisFacetInstance;
			int facet_dst = operation_info.Item1;
			int src = status.Source;
			int dst = operation_info.Item2;
			int tag = operation_info.Item3;

			Trace.WriteLineIf(this.TraceFlag==true, status.Source + ": handle_RECEIVE 3 --- " + facet_src + "," + facet_dst + "," + src + "," + dst + "," + tag);

			EnvelopType envelop = new EnvelopType (operation_type, facet_src, facet_dst, src, dst, tag);
			byte[] message2 = tag < 0 ? synchronizer_monitor.clientSendRequestAnyTag (envelop, new byte[0], ref tag) : 
				                        synchronizer_monitor.clientSendRequest       (envelop, new byte[0]);

			Trace.WriteLineIf(this.TraceFlag==true, status.Source + ": handle_RECEIVE 4 " + (message2 == null));

			//lock (lock_recv)
				this.RootCommunicator.Send<byte[]>(message2, src, tag);

			Trace.WriteLineIf(this.TraceFlag==true, status.Source + ": handle_RECEIVE 5");

		}

		void handle_PROBE (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_ALL_GATHER (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_ALL_GATHER_FLATTENED (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_ALL_REDUCE (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_ALL_TO_ALL (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_ALL_TO_ALL_FLATTENED (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_REDUCE_SCATTER (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_BROADCAST (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_SCATTER (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_SCATTER_FROM_FLATTENED (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_GATHER (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_GATHER_FLATTENED (Tuple<int, int> operation, MPI.CompletedStatus status)
		{
			throw new NotImplementedException ();
		}

		void handle_REDUCE (Tuple<int, int> operation, MPI.CompletedStatus status)
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
					Trace.WriteLineIf(this.TraceFlag==true, "DISPOSING BINDING ROOT ...");
					foreach (int i in thread_receive_requests.Keys)
					//for (int i=0; i<thread_receive_requests.Count; i++)
						if (i != this.ThisFacetInstance)
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
		private int rank = default(int);
		private string instance_name = null;
		private IDictionary<int,Socket> client_socket_facet = new Dictionary<int,Socket>();
		private IDictionary<EnvelopKey, IDictionary<int,Queue<byte[]>>> reply_pending_list = new Dictionary<EnvelopKey,IDictionary<int,Queue<byte[]>>>();
		private IDictionary<EnvelopKey, IDictionary<int,Queue<AutoResetEvent>>> request_pending_list = new Dictionary<EnvelopKey,IDictionary<int,Queue<AutoResetEvent>>>();
		private IChannelRootImpl unit;

		private object sync = new object();

		public SynchronizerMonitor (IChannelRootImpl unit, IDictionary<int,Socket> client_socket_facet, int server_facet, int rank, string instance_name)
		{
			this.unit = unit;
			this.client_socket_facet = client_socket_facet;
			this.server_facet = server_facet;
			this.rank = rank;
			this.instance_name = instance_name;
		}

		public byte[] clientSendRequest(EnvelopType envelop, byte[] messageSide1)
		{
			EnvelopKey envelop_key = new EnvelopKey (envelop);
			int envelop_tag = envelop.Item6;
			Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequest 1" + " / "  + envelop_key +" -- " + instance_name);

			byte[] messageSide2 = null;
			Monitor.Enter (sync);
			try
			{
				// envia a requisição para o root parceiro
				int facet = envelop.Item3;
				Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequest send to facet " + facet + " - nofsockets=" + client_socket_facet.Count + " / "  + envelop_key+" -- " + instance_name);
				foreach (int f in client_socket_facet.Keys)
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequest --- FACET KEY=" + f);
				Socket socket = client_socket_facet [facet];
				byte[] messageSide1_enveloped_raw = ObjectToByteArray (new Tuple<EnvelopType,byte[]> (envelop, messageSide1));
				Int32 length = messageSide1_enveloped_raw.Length;
				byte[] messageSide1_enveloped_raw_ = new byte[4 + length];
				BitConverter.GetBytes(length).CopyTo(messageSide1_enveloped_raw_,0);
				Array.Copy(messageSide1_enveloped_raw, 0, messageSide1_enveloped_raw_, 4, length);

				socket.Send (messageSide1_enveloped_raw_);

				Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequest 2 nbytes=" + messageSide1_enveloped_raw.Length + " / "  + envelop_key);

				// Verifica se já há resposta para a requisição no "conjunto de respostas pendentes de requisição"
				if (!(reply_pending_list.ContainsKey (envelop_key) && reply_pending_list [envelop_key].ContainsKey(envelop_tag)) || 
					(reply_pending_list.ContainsKey (envelop_key) && reply_pending_list [envelop_key].ContainsKey(envelop_tag) && reply_pending_list [envelop_key][envelop_tag].Count == 0)) 
				{
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequest 3 - BEFORE WAIT " + envelop_key);
					// Se não houver, coloca um item no "conjunto de requisições pendentes de resposta" e espera.

					if (!request_pending_list.ContainsKey(envelop_key))
						request_pending_list [envelop_key] = new Dictionary<int,Queue<AutoResetEvent>>();

					if (!request_pending_list [envelop_key].ContainsKey(envelop_tag))
					{
						request_pending_list [envelop_key][envelop_tag] = new Queue<AutoResetEvent>();
						request_pending_list [envelop_key][envelop_tag].Enqueue(new AutoResetEvent(false));
					}
					
					AutoResetEvent sync_send = request_pending_list [envelop_key][envelop_tag].Peek();

					//request_pending_list [envelop_key][envelop_tag] = sync_send;
					Monitor.Exit(sync);
					Console.WriteLine("clientSendRequest - WAIT / " + unit.CID.getInstanceName() + "/" + sync_send.GetHashCode()  + " BEFORE !!! " );
					sync_send.WaitOne();
					Console.WriteLine("clientSendRequest - WAIT / " + unit.CID.getInstanceName()  + "/" + sync_send.GetHashCode()  + " AFTER !!! " );
					Monitor.Enter(sync);
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequest 3 - AFTER WAIT " + envelop_key);
				}
				Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequest 4" + " / "  + envelop_key);

				Queue<byte[]> pending_replies = reply_pending_list [envelop_key][envelop_tag];
				Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequest 5 -- pending_replies.Count = " + pending_replies.Count);
				if (pending_replies.Count > 0)
				   messageSide2 = reply_pending_list[envelop_key][envelop_tag].Dequeue();
				
				if (pending_replies.Count == 0)
					reply_pending_list[envelop_key].Remove(envelop_tag);

				if (reply_pending_list[envelop_key].Count == 0)
					reply_pending_list.Remove(envelop_key);
				
				//reply_pending_list.Remove(envelop_key);
			}
			finally 
			{
				Monitor.Exit (sync);
			}

			Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequest 5");
			// retorna a menagem ...
			return messageSide2;
		}

		public byte[] clientSendRequestAnyTag(EnvelopType envelop, byte[] messageSide1, ref int envelop_tag)
		{
			EnvelopKey envelop_key = new EnvelopKey (envelop);
			Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequestAnyTag 1" + " / "  + envelop_key +" -- " + instance_name);

			byte[] messageSide2 = null;
			Monitor.Enter (sync);
			try
			{
				// envia a requisição para o root parceiro
				int facet = envelop.Item3;
				Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequestAnyTag send to facet " + facet + " - nofsockets=" + client_socket_facet.Count + " / "  + envelop_key+" -- " + instance_name);
				foreach (int f in client_socket_facet.Keys)
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequestAnyTag --- FACET KEY=" + f);
				Socket socket = client_socket_facet [facet];
				byte[] messageSide1_enveloped_raw = ObjectToByteArray (new Tuple<EnvelopType,byte[]> (envelop, messageSide1));
				Int32 length = messageSide1_enveloped_raw.Length;
				byte[] messageSide1_enveloped_raw_ = new byte[4 + length];
				BitConverter.GetBytes(length).CopyTo(messageSide1_enveloped_raw_,0);
				Array.Copy(messageSide1_enveloped_raw, 0, messageSide1_enveloped_raw_, 4, length);

				socket.Send (messageSide1_enveloped_raw_);

				Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequestAnyTag 2 nbytes=" + messageSide1_enveloped_raw.Length + " / "  + envelop_key);

				// Verifica se já há resposta para a requisição no "conjunto de respostas pendentes de requisição"
				if (!reply_pending_list.ContainsKey (envelop_key))
				{
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequestAnyTag 3 - BEFORE WAIT " + envelop_key);
					// Se não houver, coloca um item no "conjunto de requisições pendentes de resposta" e espera.

					if (!request_pending_list.ContainsKey(envelop_key))
						request_pending_list [envelop_key] = new Dictionary<int,Queue<AutoResetEvent>>();

					if (!request_pending_list [envelop_key].ContainsKey(-1))
					{
						request_pending_list [envelop_key][-1] = new Queue<AutoResetEvent>();
						request_pending_list [envelop_key][-1].Enqueue(new AutoResetEvent(false));
					}

					AutoResetEvent sync_send = request_pending_list [envelop_key][-1].Peek();

					//request_pending_list [envelop_key][envelop_tag] = sync_send;
					Monitor.Exit(sync);
					Console.WriteLine("clientSendRequestAny - WAIT / " + unit.CID.getInstanceName() + "/" + sync_send.GetHashCode()  + " BEFORE !!! " );
					sync_send.WaitOne()	;
					Console.WriteLine("clientSendRequestAny - WAIT / " + unit.CID.getInstanceName()  + "/" + sync_send.GetHashCode()  + " AFTER !!! " );
					Monitor.Enter(sync);
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequestAnyTag 3 - AFTER WAIT " + envelop_key);
				}
				Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequestAnyTag 4" + " / "  + envelop_key);

				int[] keys_vector = new int[reply_pending_list[envelop_key].Keys.Count];
				reply_pending_list[envelop_key].Keys.CopyTo(keys_vector,0);

				envelop_tag = keys_vector[0];
				Queue<byte[]> pending_replies = reply_pending_list [envelop_key][envelop_tag];
				Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequestAnyTag 5 -- pending_replies.Count = " + pending_replies.Count);
				if (pending_replies.Count > 0)
					messageSide2 = reply_pending_list[envelop_key][envelop_tag].Dequeue();
				
				if (pending_replies.Count == 0)
					reply_pending_list[envelop_key].Remove(envelop_tag);
				
				if (reply_pending_list[envelop_key].Count == 0)
					reply_pending_list.Remove(envelop_key);

				//reply_pending_list.Remove(envelop_key);
			}
			finally 
			{
				Monitor.Exit (sync);
			}

			Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": clientSendRequest 5");
			// retorna a menagem ...
			return messageSide2;
		}

		private static int BUFFER_SIZE = 1024*1024;

		public void serverReceiveRequests(int facet, Socket server_socket)
		{
			byte[] buffer = new byte[BUFFER_SIZE];
			byte[] buffer2 = new byte[BUFFER_SIZE];

			int nbytes = default(int);

			Trace.WriteLineIf(unit.TraceFlag==true, "serverReceiveRequest RECEIVE " + unit.CID.getInstanceName() + " / facet=" + facet + " BEFORE 1");
			nbytes = server_socket.Receive (buffer);		    
			Trace.WriteLineIf(unit.TraceFlag==true, "serverReceiveRequest RECEIVE " + unit.CID.getInstanceName() + " / facet=" + facet + " AFTER 1");

			Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReceiveRequests 1 - RECEIVED " + nbytes + " bytes -- " + instance_name);

			if (nbytes == 0) 
			{
				string error_message = server_facet + "/" + rank + ": serverReceiveRequests  -- the partner " + this.server_facet + " is died " + instance_name;
				Trace.WriteLineIf(unit.TraceFlag==true, error_message);
				throw new Exception(error_message);
			}

			while (true)
			{
				int length = BitConverter.ToInt32(buffer,0);
				nbytes = nbytes - 4;
				byte[] message = new byte[length];

				Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReceiveRequests 2 - length is " + length + " bytes" + " / nbytes = " + nbytes);

				Array.Copy(buffer, 4, message, 0, length);
				requestQueue.Add (message);

				if (nbytes == length) 
				{
					Trace.WriteLineIf(unit.TraceFlag==true,"serverReceiveRequest RECEIVE " + unit.CID.getInstanceName() + " / facet=" + facet + " BEFORE 2");
					nbytes = server_socket.Receive (buffer);
					Trace.WriteLineIf(unit.TraceFlag==true,"serverReceiveRequest RECEIVE " + unit.CID.getInstanceName() + " / facet=" + facet + " AFTER 2");
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReceiveRequests 3 - RECEIVED " + nbytes + " bytes --- " + instance_name);

					if (nbytes == 0) 
					{
						string error_message = server_facet + "/" + rank + ": serverReceiveRequests  -- the partner " + this.server_facet + " is died " + instance_name;
						Trace.WriteLineIf(unit.TraceFlag==true, error_message);
						throw new Exception(error_message);
					}
				} 
				else if (nbytes > length) 
				{ 
					// assume that nbytes - length > 4
					byte[] aux = buffer;
					nbytes = nbytes - length;

					Array.Copy(buffer, length + 4, buffer2, 0, nbytes);
					buffer = buffer2;
					buffer2 = aux;
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReceiveRequests 4 - nbytes=" + nbytes);
				}
				else
				{
					string error_message = server_facet + "/" + rank + ": UNEXPECTED CONDITION nbytes=" + nbytes + " < length=" + length;
					Trace.WriteLineIf(unit.TraceFlag==true, error_message);
					throw new Exception(error_message);
				}
			}

		}

		private ProducerConsumerQueue<byte[]> requestQueue = new ProducerConsumerQueue<byte[]>();

		public void serverReadRequest() 
		{
			Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReadRequest 1 ");

			byte[] buffer =	requestQueue.Take ();
			int nbytes =  buffer.Length;

			Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReadRequest 2 " + nbytes + " bytes received.");

			Monitor.Enter (sync);
			try
			{
				// Aguarda uma resposta proveniente do outro root parceiro.
				byte[] messageSide1_enveloped_raw = new byte[nbytes];
				Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReadRequest 2-1 nbytes=" + nbytes);

				// TODO: otimizar isso ...
				for (int i=0; i<nbytes; i++)
					messageSide1_enveloped_raw[i] = buffer[i];
				
				Tuple<EnvelopType,byte[]> messageSide1_enveloped = (Tuple<EnvelopType,byte[]>) ByteArrayToObject (messageSide1_enveloped_raw);

				EnvelopType envelop = messageSide1_enveloped.Item1;
				EnvelopKey envelop_key = new EnvelopKey (envelop);
				int envelop_tag = envelop.Item6;

				// Coloca a resposta no "conjunto de respostas pendentes de requisição"
				if (!reply_pending_list.ContainsKey(envelop_key))
					reply_pending_list [envelop_key] = new Dictionary<int,Queue<byte[]>>();

				if (!reply_pending_list [envelop_key].ContainsKey(envelop_tag))
					reply_pending_list [envelop_key][envelop_tag] = new Queue<byte[]>();
				
				reply_pending_list [envelop_key][envelop_tag].Enqueue(messageSide1_enveloped.Item2);

				Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReadRequest 3 " + envelop.Item1 + "," +  envelop_key);
				foreach (EnvelopKey ek in request_pending_list.Keys) 
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + ": key: " + ek);

				// Busca, no "conjunto de requisições pendentes de resposta", a requisição correspondente a resposta.
				if (request_pending_list.ContainsKey (envelop_key) && request_pending_list[envelop_key].ContainsKey(envelop_tag)) 
				{
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReadRequest 3-1" + " / "  + envelop_key);
					AutoResetEvent sync_send = request_pending_list[envelop_key][envelop_tag].Dequeue();

					sync_send.Set();

					if (request_pending_list[envelop_key][envelop_tag].Count == 0)
						request_pending_list[envelop_key].Remove(envelop_tag);
					
					if (request_pending_list[envelop_key].Count==0)
						request_pending_list.Remove(envelop_key);
					
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReadRequest 3-2"+ " / "  + envelop_key) ;
				} 
				else if (request_pending_list.ContainsKey (envelop_key) && request_pending_list[envelop_key].ContainsKey(-1)) 
				{
					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReadRequest 3-1" + " / "  + envelop_key);
					AutoResetEvent sync_send = request_pending_list[envelop_key][-1].Dequeue();
					//Monitor.Pulse (sync_send);
					sync_send.Set();

					if (request_pending_list[envelop_key][-1].Count == 0)
						request_pending_list[envelop_key].Remove(-1);
					
					if (request_pending_list[envelop_key].Count==0)
						request_pending_list.Remove(envelop_key);

					Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReadRequest 3-2"+ " / "  + envelop_key) ;
				}  
			}
			finally 
			{
				Monitor.Exit (sync);
			}
			Trace.WriteLineIf(unit.TraceFlag==true, server_facet + "/" + rank + ": serverReadRequest 4");
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
//				key = string.Format ("SR-{0}-{1}-{2}-{3}-{4}",envelop.Item2, envelop.Item3, envelop.Item4, envelop.Item5, envelop.Item6);
				key = string.Format ("SR-{0}-{1}-{2}-{3}",envelop.Item2, envelop.Item3, envelop.Item4, envelop.Item5);
				break;
			case AliencommunicatorOperation.RECEIVE:
			case AliencommunicatorOperation.RECEIVE_ARRAY:
//				key = string.Format ("SR-{1}-{0}-{3}-{2}-{4}",envelop.Item2, envelop.Item3, envelop.Item4, envelop.Item5, envelop.Item6);
				key = string.Format ("SR-{1}-{0}-{3}-{2}",envelop.Item2, envelop.Item3, envelop.Item4, envelop.Item5);
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
