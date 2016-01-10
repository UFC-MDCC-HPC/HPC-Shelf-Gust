using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingGathercast;
using MPI;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingGathercastIntra
{
	public interface IServerGathercastIntra<S> : BaseIServerGathercastIntra<S>, IServerGathercast<S>
		where S:IEnvironmentPortTypeMultiplePartner
	{
	}

	public class GathercastServer
	{
		public delegate T Operator<T> (T v1, T v2);

		public static void receiveOperation(Intercommunicator comm, out int operId)
		{
				operId = 0;
		}

		public static void gatherArgument<T> (Intercommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void reduceArgument<T> (Intercommunicator comm, Operator<T> oper, out T value)
		{
			value = default(T);
		}

		public static void scatterResult<T> (Intercommunicator comm, T[] value)
		{

		}

		public static void broadcastResult<T> (Intercommunicator comm, T value)
		{

		}



	}
}