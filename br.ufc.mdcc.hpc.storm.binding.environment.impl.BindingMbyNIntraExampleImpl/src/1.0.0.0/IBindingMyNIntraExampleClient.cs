using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.example.MbyN.EnvironmentPortTypeClient;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNIntra;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Services;
using System.IO;

namespace br.ufc.mdcc.hpc.storm.binding.environment.impl.BindingMbyNIntraExample
{
//	public class IBindingMyNIntraExampleClient<C> : BaseIBindingMyNIntraExampleClient<C>, IClientMbyNIntra<C>
//		where C:IMbyNClientPortTypeExample
	public class IBindingMyNIntraExampleClient : BaseIBindingMyNIntraExampleClient<IMbyNClientPortTypeExample>, 
												IClientMbyNIntra<IMbyNClientPortTypeExample>
	{

		private MPI.Intercommunicator channel;

		public override void after_initialize ()
		{
			int remote_leader = this.UnitRanks ["server"] [0];
			channel = new MPI.Intercommunicator(this.PeerComm, 0, this.Communicator, remote_leader, 0);
			((IMbyNClientPortTypeExampleImpl)service).Channel = channel;
			service = new IMbyNClientPortTypeExampleImpl ();

			// ABRINDO SERVIÇO para conexão do web service ... a ser feito pelo objeto proxy 
			// var binding = new WSHttpBinding ();
			// var address = new Uri ("http://localhost:8085");
			// var host = new ServiceHost (service);
			// host.AddServiceEndpoint (typeof (IMbyNClientPortTypeExample), binding, address);
			// host.Open ();		


		}

		public override void main()
		{
		}

		#region IClientBase implementation

		private IMbyNClientPortTypeExample service;

		public IMbyNClientPortTypeExample Service {
			get {
				return service;
			}
		}
		#endregion

		internal class IMbyNClientPortTypeExampleImpl : IMbyNClientPortTypeExample 
		{

			public IMbyNClientPortTypeExampleImpl()
			{
			}

			private MPI.Intercommunicator channel;

			public MPI.Intercommunicator Channel { set {this.channel = value; } }

			private const int OPERATION_TAG = 0;
			private const int OPERATION_1 = 1;
			private const int OPERATION_2 = 2;
			private const int OPERATION_3 = 3;
			private const int OPERATION_4 = 4;
			private const int OPERATION_5 = 5;
			private const int OPERATION_6 = 6;
			private const int OPERATION_7 = 7;

			#region IMbyNClientPortTypeExample implementation

			private void sendArguments (int operation_tag, int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6, ref MPI.RequestList reqList)
			{
				int remote_size = channel.RemoteSize;

				for (int server = 0; server < remote_size; server++) 
				{
					if (channel.Rank == 0)
						channel.Send<int> (operation_tag, server, OPERATION_TAG);

					int arg1_ = arg1;
					int arg2_ = arg2;
					int arg3_ = arg3;
					int arg4_ = arg4.Value[server];
					int arg5_ = arg5.Value[server];
					int arg6_ = arg6.Value[server];

					Tuple<int,int,int,int,int,int> send_value = new Tuple<int,int,int,int,int,int> (arg1_, arg2_, arg3_, arg4_, arg5_, arg6_);

					MPI.Request req = channel.ImmediateSend<Tuple<int,int,int,int,int,int>> (send_value, server, operation_tag);
					reqList.Add (req);
				}

			}

			private void receiveResult<T> (ref MPI.RequestList reqList, int operation_tag)
			{
				int remote_size = channel.RemoteSize;

				for (int server = 0; server < remote_size; server++) 
				{
					MPI.Request req = channel.ImmediateReceive<T> (server, operation_tag);
					reqList.Add (req);
				}

			}

			private int[] takeResults (IList<MPI.Request> reqList_complete)
			{
				int[] result_values = new int[channel.RemoteSize];

				foreach (MPI.Request req in reqList_complete) 
				{
					if (req is MPI.ReceiveRequest) 
					{
						MPI.ReceiveRequest recv_req = (MPI.ReceiveRequest)req;
						MPI.CompletedStatus status = recv_req.Wait ();
						int value = (int) recv_req.GetValue ();
						result_values [status.Source] = value;
					}
				}

				return result_values;
			}

			public void some_method_1 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				MPI.RequestList reqList = new MPI.RequestList ();

				sendArguments (OPERATION_1, arg1, arg2, arg3, arg4, arg5, arg6, ref reqList);
				receiveResult<int> (ref reqList, OPERATION_1);

				reqList.WaitAll ();
			}

			public IGather<int> some_method_2 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				MPI.RequestList reqList = new MPI.RequestList ();

				sendArguments (OPERATION_2, arg1, arg2, arg3, arg4, arg5, arg6, ref reqList);
				receiveResult<int> (ref reqList, OPERATION_2);

				IList<MPI.Request> reqList_complete = reqList.WaitAll ();
				
				int[] result_values = takeResults (reqList_complete);

				return Gather<int>.create (channel, result_values);
			}

			public IReduce<int> some_method_3 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				MPI.RequestList reqList = new MPI.RequestList ();

				sendArguments (OPERATION_3, arg1, arg2, arg3, arg4, arg5, arg6, ref reqList);
				receiveResult<int> (ref reqList, OPERATION_3);

				IList<MPI.Request> reqList_complete = reqList.WaitAll ();

				int[] result_values = takeResults (reqList_complete);

				return Reduce<int>.create (channel, result_values, sum, 0);
			}

			public IScan<int> some_method_4 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				MPI.RequestList reqList = new MPI.RequestList ();

				sendArguments (OPERATION_4, arg1, arg2, arg3, arg4, arg5, arg6, ref reqList);
				receiveResult<int> (ref reqList, OPERATION_4);

				IList<MPI.Request> reqList_complete = reqList.WaitAll ();

				int[] result_values = takeResults (reqList_complete);

				return Scan<int>.create (channel, result_values, sum, 0);
			}

			public IGather<int> some_method_5 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				MPI.RequestList reqList = new MPI.RequestList ();

				sendArguments (OPERATION_5, arg1, arg2, arg3, arg4, arg5, arg6, ref reqList);
				receiveResult<int> (ref reqList, OPERATION_5);

				IList<MPI.Request> reqList_complete = reqList.WaitAll ();

				int[] result_values = takeResults (reqList_complete);

				return Gather<int>.create (channel, result_values);
			}

			public IReduce<int> some_method_6 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				MPI.RequestList reqList = new MPI.RequestList ();

				sendArguments (OPERATION_6, arg1, arg2, arg3, arg4, arg5, arg6, ref reqList);
				receiveResult<int> (ref reqList, OPERATION_6);

				IList<MPI.Request> reqList_complete = reqList.WaitAll ();

				int[] result_values = takeResults (reqList_complete);

				return Reduce<int>.create (channel, result_values, sum, 0);
			}

			public IScan<int> some_method_7 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				MPI.RequestList reqList = new MPI.RequestList ();

				sendArguments (OPERATION_7, arg1, arg2, arg3, arg4, arg5, arg6, ref reqList);
				receiveResult<int> (ref reqList, OPERATION_7);

				IList<MPI.Request> reqList_complete = reqList.WaitAll ();

				int[] result_values = takeResults (reqList_complete);

				return Scan<int>.create (channel, result_values, sum, 0);
			}

			#endregion

			private int sum(int a, int b) {
				return a + b;
			}


		}
	}

	// WEB SERVICE for the connection of an application or workflow ...
	/* For avoiding to write code for automatic generation of this web service in the proxy component
	 * (application/workflow), the binding client side implementation must provide the web service class.
	 * The proxy component will put this library in the web service bin directory, create an asmx
	 * file point to the IMbyNClientPortTypeExampleService class and start xsp for publishing the
	 * web service for the client.
	 * Notice that it is necessary to create a client object for connecting the web service to the 
	 * client binding object, via WCF. 
	 * REMARK: in future implementations, the web service code will be automatically generated by proxy
	 *         components.
	 */

	public class IMbyNClientPortTypeExampleSkeleton : System.Web.Services.WebService,
													 IMbyNClientPortTypeExample
	{
		private IMbyNClientPortTypeExample binding_client_object;

		public IMbyNClientPortTypeExampleSkeleton()
		{
			var binding = new WSHttpBinding ();
			var address = new EndpointAddress ("http://localhost:8085");
			binding_client_object = new IMbyNClientPortTypeExampleImpl (binding, address);
		}

		#region IMbyNClientPortTypeExample implementation

		[WebMethod]
		public void some_method_1 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
		{
			binding_client_object.some_method_1 (arg1, arg2, arg3, arg4, arg5, arg6);
		}

		[WebMethod]
		public IGather<int> some_method_2 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
		{
			return binding_client_object.some_method_2 (arg1, arg2, arg3, arg4, arg5, arg6);
		}

		[WebMethod]
		public IReduce<int> some_method_3 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
		{
			return binding_client_object.some_method_3 (arg1, arg2, arg3, arg4, arg5, arg6);
		}

		[WebMethod]
		public IScan<int> some_method_4 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
		{
			return binding_client_object.some_method_4 (arg1, arg2, arg3, arg4, arg5, arg6);
		}

		[WebMethod]
		public IGather<int> some_method_5 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
		{
			return binding_client_object.some_method_5 (arg1, arg2, arg3, arg4, arg5, arg6);
		}

		[WebMethod]
		public IReduce<int> some_method_6 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
		{
			return binding_client_object.some_method_6 (arg1, arg2, arg3, arg4, arg5, arg6);
		}

		[WebMethod]
		public IScan<int> some_method_7 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
		{
			return binding_client_object.some_method_7 (arg1, arg2, arg3, arg4, arg5, arg6);
		}
		#endregion

		internal class IMbyNClientPortTypeExampleImpl : ClientBase<IMbyNClientPortTypeExample>, 
														IMbyNClientPortTypeExample
		{
			public IMbyNClientPortTypeExampleImpl (System.ServiceModel.Channels.Binding binding, EndpointAddress address)
				: base (binding, address)
			{
			}

			#region IMbyNClientPortTypeExample implementation

			public void some_method_1 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				Channel.some_method_1 (arg1, arg2, arg3, arg4, arg5, arg6);
			}

			public IGather<int> some_method_2 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				return Channel.some_method_2 (arg1, arg2, arg3, arg4, arg5, arg6);
			}

			public IReduce<int> some_method_3 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				return Channel.some_method_3 (arg1, arg2, arg3, arg4, arg5, arg6);
			}

			public IScan<int> some_method_4 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				return Channel.some_method_4 (arg1, arg2, arg3, arg4, arg5, arg6);
			}

			public IGather<int> some_method_5 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				return Channel.some_method_5 (arg1, arg2, arg3, arg4, arg5, arg6);
			}

			public IReduce<int> some_method_6 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				return Channel.some_method_6 (arg1, arg2, arg3, arg4, arg5, arg6);
			}

			public IScan<int> some_method_7 (int arg1, int arg2, int arg3, IScatter<int> arg4, IScatter<int> arg5, IScatter<int> arg6)
			{
				return Channel.some_method_7 (arg1, arg2, arg3, arg4, arg5, arg6);
			}

			#endregion


		}

	}

}
