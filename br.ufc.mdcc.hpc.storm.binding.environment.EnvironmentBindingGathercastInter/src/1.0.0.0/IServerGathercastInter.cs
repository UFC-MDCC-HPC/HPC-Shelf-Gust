using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingGathercast;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingGathercastInter
{
	public interface IServerGathercastInter<S> : BaseIServerGathercastInter<S>, IServerGathercast<S>
		where S:IEnvironmentPortTypeMultiplePartner
	{
	}

	public class GathercastServer
	{
		public delegate T Operator<T> (T v1, T v2);

		public static void receiveOperation(Aliencommunicator comm, out int operId)
		{
			operId = 0;
		}

		public static void gatherArgument<T> (Aliencommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void reduceArgument<T> (Aliencommunicator comm, Operator<T> oper, out T value)
		{
			value = default(T);
		}

		public static void scatterResult<T> (Aliencommunicator comm, T[] value)
		{

		}

		public static void broadcastResult<T> (Aliencommunicator comm, T value)
		{

		}
	}
}