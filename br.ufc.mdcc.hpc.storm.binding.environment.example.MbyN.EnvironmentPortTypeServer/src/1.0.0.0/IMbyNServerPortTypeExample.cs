using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.example.MbyN.EnvironmentPortTypeServer
{
	public interface IMbyNServerPortTypeExample : BaseIMbyNServerPortTypeExample, IEnvironmentPortTypeMultiplePartner
	{
		void some_method_1(IGather<int> arg1,  // ALL GATHER
		                  IReduce<int> arg2,   // ALL REDUCE
		                  IScan<int> arg3,     // ALL SCAN 
		                  IGather<int> arg4,   // ALL TO ALL
		                  IReduce<int> arg5,   // REDUCE_SCATTER
		                  IScan<int> arg6);    // SCAN_SCATTER

		int some_method_2(IGather<int> arg1,   // ALL GATHER
	                      IReduce<int> arg2,   // ALL REDUCE
	                      IScan<int> arg3,     // ALL SCAN 
	                      IGather<int> arg4,   // ALL TO ALL
	                      IReduce<int> arg5,   // REDUCE_SCATTER
	                      IScan<int> arg6);    // SCAN_SCATTER

		int some_method_3(IGather<int> arg1,   // ALL GATHER
		                  IReduce<int> arg2,   // ALL REDUCE
		                  IScan<int> arg3,     // ALL SCAN 
		                  IGather<int> arg4,   // ALL TO ALL
		                  IReduce<int> arg5,   // REDUCE_SCATTER
		                  IScan<int> arg6);    // SCAN_SCATTER

		int some_method_4(IGather<int> arg1,   // ALL GATHER
		                  IReduce<int> arg2,   // ALL REDUCE
		                  IScan<int> arg3,     // ALL SCAN 
		                  IGather<int> arg4,   // ALL TO ALL
		                  IReduce<int> arg5,   // REDUCE_SCATTER
		                  IScan<int> arg6);    // SCAN_SCATTER

		IScatter<int> some_method_5(IGather<int> arg1,   // ALL GATHER
		                            IReduce<int> arg2,   // ALL REDUCE
		                            IScan<int> arg3,     // ALL SCAN 
		                            IGather<int> arg4,   // ALL TO ALL
		                            IReduce<int> arg5,   // REDUCE_SCATTER
		                            IScan<int> arg6);    // SCAN_SCATTER

		IScatter<int> some_method_6(IGather<int> arg1,   // ALL GATHER
		                            IReduce<int> arg2,   // ALL REDUCE
		                            IScan<int> arg3,     // ALL SCAN 
		                            IGather<int> arg4,   // ALL TO ALL
		                            IReduce<int> arg5,   // REDUCE_SCATTER
		                            IScan<int> arg6);    // SCAN_SCATTER

		IScatter<int> some_method_7(IGather<int> arg1,   // ALL GATHER
		                            IReduce<int> arg2,   // ALL REDUCE
		                            IScan<int> arg3,     // ALL SCAN 
		                            IGather<int> arg4,   // ALL TO ALL
		                            IReduce<int> arg5,   // REDUCE_SCATTER
		                            IScan<int> arg6);    // SCAN_SCATTER
	}

}