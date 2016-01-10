using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNInter
{
	public interface IClientMbyNInter<C> : BaseIClientMbyNInter<C>, IClientMbyN<C>
		where C:IEnvironmentPortTypeMultiplePartner
	{
	}

	public class MbyNClient
	{
		public static void sendOperation(Aliencommunicator comm, int operId)
		{

		}

		public static void allgatherArgument<T> (Aliencommunicator comm, T value)
		{

		}

		public static void allreduceArgument<T> (Aliencommunicator comm, T value)
		{

		}

		public static void IScan<T> (Aliencommunicator comm, T value)
		{

		}

		public static void alltoallArgument<T> (Aliencommunicator comm, T[] value)
		{

		}

		public static void reducescatterArgument<T> (Aliencommunicator comm, T[] value)
		{

		}

		public delegate T Operator<T> (T v1, T v2);

		public static void allgatherResult<T> (Aliencommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void allreduceResult<T> (Aliencommunicator comm, Operator<T> oper, out T value)
		{
			value = default (T);
		}

		public static void scanResult<T> (Aliencommunicator comm, Operator<T> oper, out T value)
		{
			value = default (T);
		}

		public static void alltoAllResult<T> (Aliencommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void reducescatterResult<T> (Aliencommunicator comm, Operator<T> oper, out T value)
		{
			value = default(T);
		}
	}
}