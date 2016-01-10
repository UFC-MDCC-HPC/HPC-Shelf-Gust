using System;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.example.MbyN.EnvironmentPortTypeServer;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNIntra;
using System.Collections.Generic;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.impl.BindingMbyNIntraExample
{
//	public class IBindingMyNIntraExampleServer<S> : BaseIBindingMyNIntraExampleServer<S>, IServerMbyNIntra<S>
//		where S:IMbyNServerPortTypeExample
	public class IBindingMyNIntraExampleServer : BaseIBindingMyNIntraExampleServer<IMbyNServerPortTypeExample>, 
													IServerMbyNIntra<IMbyNServerPortTypeExample>
	{

		private MPI.Intercommunicator channel;

		public override void after_initialize ()
		{
			int remote_leader = this.UnitRanks ["client"] [0];
			channel = new MPI.Intercommunicator(this.PeerComm, 0, this.Communicator, remote_leader, 0);
		}

		private const int OPERATION_TAG = 0;
		private const int OPERATION_1 = 1;
		private const int OPERATION_2 = 2;
		private const int OPERATION_3 = 3;
		private const int OPERATION_4 = 4;
		private const int OPERATION_5 = 5;
		private const int OPERATION_6 = 6;
		private const int OPERATION_7 = 7;

		public override void main()
		{
			int remote_size = channel.RemoteSize;

			while (true) 
			{
				MPI.RequestList reqList = new MPI.RequestList ();
				int operation_tag = channel.Receive<int> (0, OPERATION_TAG);

				for (int client=0; client<remote_size; client++) 
				{
					MPI.ReceiveRequest req = channel.ImmediateReceive<Tuple<int,int,int,int,int,int>> (client, operation_tag);
					reqList.Add (req);
				}

				IList<MPI.Request> reqList_complete = reqList.WaitAll ();

				switch (operation_tag) 
				{
				case OPERATION_1:
					handleOperation1 (reqList_complete);
					break;
				case OPERATION_2:
					handleOperation2 (reqList_complete);
					break;
				case OPERATION_3:
					handleOperation3 (reqList_complete);
					break;
				case OPERATION_4:
					handleOperation4 (reqList_complete);
					break;
				case OPERATION_5:
					handleOperation5 (reqList_complete);
					break;
				case OPERATION_6:
					handleOperation6 (reqList_complete);
					break;
				case OPERATION_7:
					handleOperation7 (reqList_complete);
					break;
				}
			}
		}

		private int sum(int a, int b) {
			return a + b;
		}

		void returnResult (int[] result, int operation_tag)
		{
			MPI.RequestList reqList = new MPI.RequestList ();
			int remote_size = channel.RemoteSize;
			for (int client=0; client<remote_size; client++) 
			{
				MPI.Request req = channel.ImmediateSend<int> (result[client], client, operation_tag);
				reqList.Add (req);
			}
			reqList.WaitAll ();
		}

		void handleOperation1 (IList<MPI.Request> reqList_complete)
		{
			int[] arg1_values = new int[channel.RemoteSize];
			int[] arg2_values = new int[channel.RemoteSize];
			int[] arg3_values = new int[channel.RemoteSize];
			int[] arg4_values = new int[channel.RemoteSize];
			int[] arg5_values = new int[channel.RemoteSize];
			int[] arg6_values = new int[channel.RemoteSize];

			foreach (MPI.Request req in reqList_complete) 
			{
				MPI.ReceiveRequest recv_req = (MPI.ReceiveRequest) req;
				MPI.CompletedStatus status = recv_req.Test ();
				Tuple<int,int,int,int,int,int> value = (Tuple<int,int,int,int,int,int>)recv_req.GetValue ();
				arg1_values [status.Source] = value.Item1;
				arg2_values [status.Source] = value.Item2;
				arg3_values [status.Source] = value.Item3;
				arg4_values [status.Source] = value.Item4;
				arg5_values [status.Source] = value.Item5;
				arg6_values [status.Source] = value.Item6;
			}

			IGather<int> arg1 = Gather<int>.create (channel, arg1_values);
			IReduce<int> arg2 = Reduce<int>.create (channel, arg2_values, sum, 0);
			IScan<int> arg3 = Scan<int>.create (channel, arg3_values, sum, 0);
			IGather<int> arg4 = Gather<int>.create (channel, arg4_values);
			IReduce<int> arg5 = Reduce<int>.create (channel, arg5_values, sum, 0);
			IScan<int> arg6 = Scan<int>.create (channel, arg6_values, sum, 0);

			service.some_method_1 (arg1, arg2, arg3, arg4, arg5, arg6);
		}

		void handleOperation2 (IList<MPI.Request> reqList_complete)
		{
			int[] arg1_values = new int[channel.RemoteSize];
			int[] arg2_values = new int[channel.RemoteSize];
			int[] arg3_values = new int[channel.RemoteSize];
			int[] arg4_values = new int[channel.RemoteSize];
			int[] arg5_values = new int[channel.RemoteSize];
			int[] arg6_values = new int[channel.RemoteSize];

			foreach (MPI.Request req in reqList_complete) 
			{
				MPI.ReceiveRequest recv_req = (MPI.ReceiveRequest) req;
				MPI.CompletedStatus status = recv_req.Test ();
				Tuple<int,int,int,int,int,int> value = (Tuple<int,int,int,int,int,int>)recv_req.GetValue ();
				arg1_values [status.Source] = value.Item1;
				arg2_values [status.Source] = value.Item2;
				arg3_values [status.Source] = value.Item3;
				arg4_values [status.Source] = value.Item4;
				arg5_values [status.Source] = value.Item5;
				arg6_values [status.Source] = value.Item6;
			}

			IGather<int> arg1 = Gather<int>.create (channel, arg1_values);
			IReduce<int> arg2 = Reduce<int>.create (channel, arg2_values, sum, 0);
			IScan<int> arg3 = Scan<int>.create (channel, arg3_values, sum, 0);
			IGather<int> arg4 = Gather<int>.create (channel, arg4_values);
			IReduce<int> arg5 = Reduce<int>.create (channel, arg5_values, sum, 0);
			IScan<int> arg6 = Scan<int>.create (channel, arg6_values, sum, 0);

			int result = service.some_method_2 (arg1, arg2, arg3, arg4, arg5, arg6);

			int[] broadcast_result = new int[channel.RemoteSize];
			for (int i=0; i<broadcast_result.Length; i++)
				broadcast_result [i] = result;

			returnResult (broadcast_result, OPERATION_2);
		}

		void handleOperation3 (IList<MPI.Request> reqList_complete)
		{
			int[] arg1_values = new int[channel.RemoteSize];
			int[] arg2_values = new int[channel.RemoteSize];
			int[] arg3_values = new int[channel.RemoteSize];
			int[] arg4_values = new int[channel.RemoteSize];
			int[] arg5_values = new int[channel.RemoteSize];
			int[] arg6_values = new int[channel.RemoteSize];

			foreach (MPI.Request req in reqList_complete) 
			{
				MPI.ReceiveRequest recv_req = (MPI.ReceiveRequest) req;
				MPI.CompletedStatus status = recv_req.Test ();
				Tuple<int,int,int,int,int,int> value = (Tuple<int,int,int,int,int,int>)recv_req.GetValue ();
				arg1_values [status.Source] = value.Item1;
				arg2_values [status.Source] = value.Item2;
				arg3_values [status.Source] = value.Item3;
				arg4_values [status.Source] = value.Item4;
				arg5_values [status.Source] = value.Item5;
				arg6_values [status.Source] = value.Item6;
			}

			IGather<int> arg1 = Gather<int>.create (channel, arg1_values);
			IReduce<int> arg2 = Reduce<int>.create (channel, arg2_values, sum, 0);
			IScan<int> arg3 = Scan<int>.create (channel, arg3_values, sum, 0);
			IGather<int> arg4 = Gather<int>.create (channel, arg4_values);
			IReduce<int> arg5 = Reduce<int>.create (channel, arg5_values, sum, 0);
			IScan<int> arg6 = Scan<int>.create (channel, arg6_values, sum, 0);

			int result = service.some_method_3 (arg1, arg2, arg3, arg4, arg5, arg6);

			int[] broadcast_result = new int[channel.RemoteSize];
			for (int i=0; i<broadcast_result.Length; i++)
				broadcast_result [i] = result;

			returnResult (broadcast_result, OPERATION_3);
		}

		void handleOperation4 (IList<MPI.Request> reqList_complete)
		{
			int[] arg1_values = new int[channel.RemoteSize];
			int[] arg2_values = new int[channel.RemoteSize];
			int[] arg3_values = new int[channel.RemoteSize];
			int[] arg4_values = new int[channel.RemoteSize];
			int[] arg5_values = new int[channel.RemoteSize];
			int[] arg6_values = new int[channel.RemoteSize];

			foreach (MPI.Request req in reqList_complete) 
			{
				MPI.ReceiveRequest recv_req = (MPI.ReceiveRequest) req;
				MPI.CompletedStatus status = recv_req.Test ();
				Tuple<int,int,int,int,int,int> value = (Tuple<int,int,int,int,int,int>)recv_req.GetValue ();
				arg1_values [status.Source] = value.Item1;
				arg2_values [status.Source] = value.Item2;
				arg3_values [status.Source] = value.Item3;
				arg4_values [status.Source] = value.Item4;
				arg5_values [status.Source] = value.Item5;
				arg6_values [status.Source] = value.Item6;
			}

			IGather<int> arg1 = Gather<int>.create (channel, arg1_values);
			IReduce<int> arg2 = Reduce<int>.create (channel, arg2_values, sum, 0);
			IScan<int> arg3 = Scan<int>.create (channel, arg3_values, sum, 0);
			IGather<int> arg4 = Gather<int>.create (channel, arg4_values);
			IReduce<int> arg5 = Reduce<int>.create (channel, arg5_values, sum, 0);
			IScan<int> arg6 = Scan<int>.create (channel, arg6_values, sum, 0);

			int result = service.some_method_4 (arg1, arg2, arg3, arg4, arg5, arg6);

			int[] broadcast_result = new int[channel.RemoteSize];
			for (int i=0; i<broadcast_result.Length; i++)
				broadcast_result [i] = result;

			returnResult (broadcast_result, OPERATION_4);
		}

		void handleOperation5 (IList<MPI.Request> reqList_complete)
		{
			int[] arg1_values = new int[channel.RemoteSize];
			int[] arg2_values = new int[channel.RemoteSize];
			int[] arg3_values = new int[channel.RemoteSize];
			int[] arg4_values = new int[channel.RemoteSize];
			int[] arg5_values = new int[channel.RemoteSize];
			int[] arg6_values = new int[channel.RemoteSize];

			foreach (MPI.Request req in reqList_complete) 
			{
				MPI.ReceiveRequest recv_req = (MPI.ReceiveRequest) req;
				MPI.CompletedStatus status = recv_req.Test ();
				Tuple<int,int,int,int,int,int> value = (Tuple<int,int,int,int,int,int>)recv_req.GetValue ();
				arg1_values [status.Source] = value.Item1;
				arg2_values [status.Source] = value.Item2;
				arg3_values [status.Source] = value.Item3;
				arg4_values [status.Source] = value.Item4;
				arg5_values [status.Source] = value.Item5;
				arg6_values [status.Source] = value.Item6;
			}

			IGather<int> arg1 = Gather<int>.create (channel, arg1_values);
			IReduce<int> arg2 = Reduce<int>.create (channel, arg2_values, sum, 0);
			IScan<int> arg3 = Scan<int>.create (channel, arg3_values, sum, 0);
			IGather<int> arg4 = Gather<int>.create (channel, arg4_values);
			IReduce<int> arg5 = Reduce<int>.create (channel, arg5_values, sum, 0);
			IScan<int> arg6 = Scan<int>.create (channel, arg6_values, sum, 0);

			IScatter<int> result = service.some_method_5 (arg1, arg2, arg3, arg4, arg5, arg6);

			returnResult (result.Value, OPERATION_5);
		}

		void handleOperation6 (IList<MPI.Request> reqList_complete)
		{
			int[] arg1_values = new int[channel.RemoteSize];
			int[] arg2_values = new int[channel.RemoteSize];
			int[] arg3_values = new int[channel.RemoteSize];
			int[] arg4_values = new int[channel.RemoteSize];
			int[] arg5_values = new int[channel.RemoteSize];
			int[] arg6_values = new int[channel.RemoteSize];

			foreach (MPI.Request req in reqList_complete) 
			{
				MPI.ReceiveRequest recv_req = (MPI.ReceiveRequest) req;
				MPI.CompletedStatus status = recv_req.Test ();
				Tuple<int,int,int,int,int,int> value = (Tuple<int,int,int,int,int,int>)recv_req.GetValue ();
				arg1_values [status.Source] = value.Item1;
				arg2_values [status.Source] = value.Item2;
				arg3_values [status.Source] = value.Item3;
				arg4_values [status.Source] = value.Item4;
				arg5_values [status.Source] = value.Item5;
				arg6_values [status.Source] = value.Item6;
			}

			IGather<int> arg1 = Gather<int>.create (channel, arg1_values);
			IReduce<int> arg2 = Reduce<int>.create (channel, arg2_values, sum, 0);
			IScan<int> arg3 = Scan<int>.create (channel, arg3_values, sum, 0);
			IGather<int> arg4 = Gather<int>.create (channel, arg4_values);
			IReduce<int> arg5 = Reduce<int>.create (channel, arg5_values, sum, 0);
			IScan<int> arg6 = Scan<int>.create (channel, arg6_values, sum, 0);

			IScatter<int> result = service.some_method_6 (arg1, arg2, arg3, arg4, arg5, arg6);

			returnResult (result.Value, OPERATION_6);
		}

		void handleOperation7 (IList<MPI.Request> reqList_complete)
		{
			int[] arg1_values = new int[channel.RemoteSize];
			int[] arg2_values = new int[channel.RemoteSize];
			int[] arg3_values = new int[channel.RemoteSize];
			int[] arg4_values = new int[channel.RemoteSize];
			int[] arg5_values = new int[channel.RemoteSize];
			int[] arg6_values = new int[channel.RemoteSize];

			foreach (MPI.Request req in reqList_complete) 
			{
				MPI.ReceiveRequest recv_req = (MPI.ReceiveRequest) req;
				MPI.CompletedStatus status = recv_req.Test ();
				Tuple<int,int,int,int,int,int> value = (Tuple<int,int,int,int,int,int>)recv_req.GetValue ();
				arg1_values [status.Source] = value.Item1;
				arg2_values [status.Source] = value.Item2;
				arg3_values [status.Source] = value.Item3;
				arg4_values [status.Source] = value.Item4;
				arg5_values [status.Source] = value.Item5;
				arg6_values [status.Source] = value.Item6;
			}

			IGather<int> arg1 = Gather<int>.create (channel, arg1_values);
			IReduce<int> arg2 = Reduce<int>.create (channel, arg2_values, sum, 0);
			IScan<int> arg3 = Scan<int>.create (channel, arg3_values, sum, 0);
			IGather<int> arg4 = Gather<int>.create (channel, arg4_values);
			IReduce<int> arg5 = Reduce<int>.create (channel, arg5_values, sum, 0);
			IScan<int> arg6 = Scan<int>.create (channel, arg6_values, sum, 0);

			IScatter<int> result = service.some_method_7 (arg1, arg2, arg3, arg4, arg5, arg6);

			returnResult (result.Value, OPERATION_7);
		}

		#region IServerBase implementation

		private IMbyNServerPortTypeExample service;

		public IMbyNServerPortTypeExample Service {
			set {
				this.service = value;
			}
		}
		#endregion


		// PROXY for a remote server ...
		/* For avoiding to write code for automatic generation of this proxy in the proxy component
		 * (application/workflow), the binding server side implementation must provide the proxy class.
		 * In the proxy component, it will be instantiated and assigned to the Service property.
		 * REMARK: in future implementations, the proxy code will be automatically generated by proxy
		 *         components.
		 */

		[System.Web.Services.WebServiceBinding(Name="BackEnd_WSSoap", Namespace="http://backend.hPE/")]
		[System.Diagnostics.DebuggerStepThroughAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute("code")]
		public partial class IMbyNServerPortTypeExampleStub : System.Web.Services.Protocols.SoapHttpClientProtocol,
		IMbyNServerPortTypeExample
		{
			#region IMbyNServerPortTypeExample implementation
			public void some_method_1 (IGather<int> arg1, IReduce<int> arg2, IScan<int> arg3, IGather<int> arg4, IReduce<int> arg5, IScan<int> arg6)
			{
				throw new NotImplementedException ();
			}
			public int some_method_2 (IGather<int> arg1, IReduce<int> arg2, IScan<int> arg3, IGather<int> arg4, IReduce<int> arg5, IScan<int> arg6)
			{
				throw new NotImplementedException ();
			}
			public int some_method_3 (IGather<int> arg1, IReduce<int> arg2, IScan<int> arg3, IGather<int> arg4, IReduce<int> arg5, IScan<int> arg6)
			{
				throw new NotImplementedException ();
			}
			public int some_method_4 (IGather<int> arg1, IReduce<int> arg2, IScan<int> arg3, IGather<int> arg4, IReduce<int> arg5, IScan<int> arg6)
			{
				throw new NotImplementedException ();
			}
			public IScatter<int> some_method_5 (IGather<int> arg1, IReduce<int> arg2, IScan<int> arg3, IGather<int> arg4, IReduce<int> arg5, IScan<int> arg6)
			{
				throw new NotImplementedException ();
			}
			public IScatter<int> some_method_6 (IGather<int> arg1, IReduce<int> arg2, IScan<int> arg3, IGather<int> arg4, IReduce<int> arg5, IScan<int> arg6)
			{
				throw new NotImplementedException ();
			}
			public IScatter<int> some_method_7 (IGather<int> arg1, IReduce<int> arg2, IScan<int> arg3, IGather<int> arg4, IReduce<int> arg5, IScan<int> arg6)
			{
				throw new NotImplementedException ();
			}
			#endregion
		}
	}





}
