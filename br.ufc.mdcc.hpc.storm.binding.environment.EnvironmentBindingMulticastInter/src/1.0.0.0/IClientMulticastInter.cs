using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMulticast;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMulticastInter
{
	public interface IClientMulticastInter<C> : BaseIClientMulticastInter<C>, IClientMulticast<C>
		where C:IEnvironmentPortTypeMultiplePartner
	{
	}
	
	public class MulticastClient
	{
		public static void sendOperation(Aliencommunicator comm, int operId)
		{

		}

		public static void scatterArgument<T> (Aliencommunicator comm, T[] value)
		{

		}

		public static void broadcastArgument<T> (Aliencommunicator comm, T value)
		{

		}

		public delegate T Operator<T> (T v1, T v2);

		public static void gatherResult<T>(Aliencommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void reduceResult<T>(Aliencommunicator comm, Operator<T> oper, out T value)
		{
			value = default(T);
		}

	}
}