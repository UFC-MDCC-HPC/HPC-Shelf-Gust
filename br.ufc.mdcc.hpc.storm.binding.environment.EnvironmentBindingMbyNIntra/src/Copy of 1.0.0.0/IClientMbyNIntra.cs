using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;
using MPI;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultipleServer;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNIntra
{
	public interface IClientMbyNIntra<C> : BaseIClientMbyNIntra<C>, IClientMbyN<C>
		where C:IEnvironmentPortTypeMultipleServer
	{
	}
	
	public class MbyNClient
	{
		public static void sendOperation(Intercommunicator comm, int operId)
		{

		}

		public static void allgatherArgument<T> (Intercommunicator comm, T value)
		{

		}

		public static void allreduceArgument<T> (Intercommunicator comm, T value)
		{

		}

		public static void scanArgument<T> (Intercommunicator comm, T value)
		{

		}

		public static void alltoallArgument<T> (Intercommunicator comm, T[] value)
		{

		}

		public static void reducescatterArgument<T> (Intercommunicator comm, T[] value)
		{

		}

		public delegate T Operator<T> (T v1, T v2);

		public static void allgatherResult<T> (Intercommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void allreduceResult<T> (Intercommunicator comm, Operator<T> oper, out T value)
		{
			value = default (T);
		}

		public static void scanResult<T> (Intercommunicator comm, Operator<T> oper, out T value)
		{
			value = default (T);
		}

		public static void alltoAllResult<T> (Intercommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void reducescatterResult<T> (Intercommunicator comm, Operator<T> oper, out T value)
		{
			value = default(T);
		}
}
}