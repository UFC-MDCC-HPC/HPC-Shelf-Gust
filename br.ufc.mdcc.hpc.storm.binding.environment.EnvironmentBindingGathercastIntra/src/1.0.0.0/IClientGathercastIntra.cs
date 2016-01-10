using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingGathercast;
using MPI;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingGathercastIntra
{
	public interface IClientGathercastIntra<C> : BaseIClientGathercastIntra<C>, IClientGathercast<C>
		where C:IEnvironmentPortTypeSinglePartner
	{
	}

	public class GathercastClient
	{
		public static void sendOperation(Intercommunicator comm, int operId)
		{

		}

		public static void gatherArgument<T> (Intercommunicator comm, T value)
		{

		}

		public static void reduceArgument<T> (Intercommunicator comm, T value)
		{

		}

		public static void scatterResult<T> (Intercommunicator comm, out T value)
		{
			value = default(T);
		}

		public static void broadcastResult<T> (Intercommunicator comm, out T value)
		{
			value = default (T);
		}

	}

}