using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMulticast;
using MPI;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMulticastIntra
{
	public interface IServerMulticastIntra<S> : BaseIServerMulticastIntra<S>, IServerMulticast<S>
		where S:IEnvironmentPortTypeSinglePartner
	{
	}

	public class MulticastServer
	{

		public static void receiveOperation(Intercommunicator comm, out int operId)
		{
				operId = 0;
		}

		public static void scatterArgument<T> (Intercommunicator comm, out T value)
		{
				value = default(T);
		}

		public static void broadcastArgument<T> (Intercommunicator comm, out T value)
		{
				value = default (T);
		}

		public static void gatherResult<T>(Intercommunicator comm, T value)
		{

		}

		public static void reduceResult<T>(Intercommunicator comm, T value)
		{
		
		}

	}
}