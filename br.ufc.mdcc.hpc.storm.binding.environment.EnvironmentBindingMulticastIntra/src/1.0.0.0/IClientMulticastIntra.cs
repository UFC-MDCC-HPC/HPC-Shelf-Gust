using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMulticast;
using MPI;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMulticastIntra
{
	public interface IClientMulticastIntra<C> : BaseIClientMulticastIntra<C>, IClientMulticast<C>
		where C:IEnvironmentPortTypeMultiplePartner
	{
	}

	public class MulticastClient
	{
		public static void sendOperation(Intercommunicator comm, int operId)
		{

		}

		public static void scatterArgument<T> (Intercommunicator comm, T[] value)
		{

		}

		public static void broadcastArgument<T> (Intercommunicator comm, T value)
		{

		}

		public delegate T Operator<T> (T v1, T v2);

		public static void gatherResult<T>(Intercommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void reduceResult<T>(Intercommunicator comm, Operator<T> oper, out T value)
		{
			value = default(T);
		}

	}
}