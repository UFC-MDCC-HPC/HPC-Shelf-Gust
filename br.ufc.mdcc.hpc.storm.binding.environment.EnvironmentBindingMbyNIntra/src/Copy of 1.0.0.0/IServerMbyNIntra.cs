using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;
using MPI;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultipleClient;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNIntra
{
	public interface IServerMbyNIntra<S> : BaseIServerMbyNIntra<S>, IServerMbyN<S>
		where S:IEnvironmentPortTypeMultipleClient
	{
	}


	public class MbyNServer
	{
		public delegate T Operator<T> (T v1, T v2);

		public static void receiveOperation(Intercommunicator comm, out int operId)
		{
			operId = 0;
		}

		public static void allgatherArgument<T> (Intercommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void allreduceArgument<T> (Intercommunicator comm, Operator<T> oper, out T value)
		{
			value = default (T);
		}

		public static void scanArgument<T> (Intercommunicator comm, Operator<T> oper, out T value)
		{
			value = default (T);
		}

		public static void alltoAllArgument<T> (Intercommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void reducescatterArgument<T> (Intercommunicator comm, Operator<T> oper, out T value)
		{
			value = default(T);
		}

		public static void allgatheResult<T> (Intercommunicator comm, T value)
		{

		}

		public static void allreduceResult<T> (Intercommunicator comm, T value)
		{

		}

		public static void scanResult<T> (Intercommunicator comm, T value)
		{

		}

		public static void alltoallResult<T> (Intercommunicator comm, T[] value)
		{

		}

		public static void reducescatterResult<T> (Intercommunicator comm, T[] value)
		{

		}
}
}