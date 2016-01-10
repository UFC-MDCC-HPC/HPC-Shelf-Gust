using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingGathercast;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingGathercastInter
{
	public interface IClientGathercastInter<C> : BaseIClientGathercastInter<C>, IClientGathercast<C>
		where C:IEnvironmentPortTypeSinglePartner
	{
	}

	public class GathercastClient
	{
		public static void sendOperation(Aliencommunicator comm, int operId)
		{

		}

		public static void gatherArgument<T> (Aliencommunicator comm, T value)
		{

		}

		public static void reduceArgument<T> (Aliencommunicator comm, T value)
		{

		}

		public static void scatterResult<T> (Aliencommunicator comm, out T value)
		{
			value = default(T);
		}

		public static void broadcastResult<T> (Aliencommunicator comm, out T value)
		{
			value = default (T);
		}

	}
}