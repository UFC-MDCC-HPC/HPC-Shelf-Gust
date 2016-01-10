using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;
using System.ServiceModel;

namespace br.ufc.mdcc.hpc.storm.binding.environment.example.MbyN.EnvironmentPortTypeClient
{
	[ServiceContract]
	public interface IMbyNClientPortTypeExample : BaseIMbyNClientPortTypeExample, IEnvironmentPortTypeMultiplePartner
	{
		[OperationContract]
		void some_method_1(int arg1,                	 		  // ALL GATHER
		                 int arg2,                		 		  // ALL REDUCE
		                 int arg3,                		  		  // ALL SCAN
		                 IScatter<int> arg4,      		  // ALL TO ALL
		                 IScatter<int> arg5,      		  // REDUCE_SCATTER
		                 IScatter<int> arg6);    		  // SCAN_SCATTER

		[OperationContract]
		IGather<int> some_method_2(int arg1,                // ALL GATHER
				                 int arg2,                		  // ALL REDUCE
				                 int arg3,                		  // ALL SCAN
				                 IScatter<int> arg4,      // ALL TO ALL
				                 IScatter<int> arg5,      // REDUCE_SCATTER
				                 IScatter<int> arg6);     // SCAN_SCATTER

		[OperationContract]
		IReduce<int> some_method_3(int arg1,                // ALL GATHER
		                         int arg2,                		  // ALL REDUCE
		                         int arg3,                		  // ALL SCAN
		                         IScatter<int> arg4,      // ALL TO ALL
		                         IScatter<int> arg5,      // REDUCE_SCATTER
		                         IScatter<int> arg6);     // SCAN_SCATTER

		[OperationContract]
		IScan<int> some_method_4(int arg1,                  // ALL GATHER
		                         int arg2,                		  // ALL REDUCE
		                         int arg3,                        // ALL SCAN
		                         IScatter<int> arg4,      // ALL TO ALL
		                         IScatter<int> arg5,      // REDUCE_SCATTER
		                         IScatter<int> arg6);     // SCAN_SCATTER

		[OperationContract]
		IGather<int> some_method_5(int arg1,                // ALL GATHER
		                           int arg2,                      // ALL REDUCE
		                           int arg3,                      // ALL SCAN
		                           IScatter<int> arg4,    // ALL TO ALL
		                           IScatter<int> arg5,    // REDUCE_SCATTER
		                           IScatter<int> arg6);   // SCAN_SCATTER

		[OperationContract]
		IReduce<int> some_method_6(int arg1,                // ALL GATHER
		                           int arg2,                      // ALL REDUCE
		                           int arg3,                      // ALL SCAN
		                           IScatter<int> arg4,    // ALL TO ALL
		                           IScatter<int> arg5,    // REDUCE_SCATTER
		                           IScatter<int> arg6);   // SCAN_SCATTER

		[OperationContract]
		IScan<int> some_method_7(int arg1,                  // ALL GATHER
		                         int arg2,                        // ALL REDUCE
		                         int arg3,                        // ALL SCAN
		                         IScatter<int> arg4,      // ALL TO ALL
		                         IScatter<int> arg5,      // REDUCE_SCATTER
		                         IScatter<int> arg6);     // SCAN_SCATTER
	}
}