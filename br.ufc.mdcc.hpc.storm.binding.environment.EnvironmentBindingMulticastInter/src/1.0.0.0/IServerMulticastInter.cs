using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMulticast;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeSinglePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMulticastInter
{
	public interface IServerMulticastInter<S> : BaseIServerMulticastInter<S>, IServerMulticast<S>
		where S:IEnvironmentPortTypeSinglePartner
	{
	}
	
	public class GathercastServer
	{
		public delegate T Operator<T> (T v1, T v2);

		public static void receiveOperation(Aliencommunicator comm, out int operId)
		{
				operId = 0;
		}

		public static void scatterArgument<T> (Aliencommunicator comm, out T value)
		{
			value = default(T);
		}

		public static void broadcastArgument<T> (Aliencommunicator comm, out T value)
		{
			value = default (T);
		}

		public static void gatherResult<T>(Aliencommunicator comm, T value)
		{

		}

		public static void reduceResult<T>(Aliencommunicator comm, T value)
		{

		}
	}
}