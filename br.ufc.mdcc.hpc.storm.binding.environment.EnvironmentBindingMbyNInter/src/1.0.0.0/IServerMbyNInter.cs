using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortType;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyN;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentPortTypeMultiplePartner;

namespace br.ufc.mdcc.hpc.storm.binding.environment.EnvironmentBindingMbyNInter
{
	public interface IServerMbyNInter<S> : BaseIServerMbyNInter<S>, IServerMbyN<S>
		where S:IEnvironmentPortTypeMultiplePartner
	{
	}

	public class MbyNServer
	{
		public delegate T Operator<T> (T v1, T v2);

		public static void receiveOperation(Aliencommunicator comm, out int operId)
		{
			operId = 0;
		}

		public static void allgatherArgument<T> (Aliencommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void allreduceArgument<T> (Aliencommunicator comm, Operator<T> oper, out T value)
		{
			value = default (T);
		}

		public static void Scan<T> (Aliencommunicator comm, Operator<T> oper, out T value)
		{
			value = default (T);
		}

		public static void alltoAllArgument<T> (Aliencommunicator comm, out T[] value)
		{
			value = null;
		}

		public static void reducescatterArgument<T> (Aliencommunicator comm, Operator<T> oper, out T value)
		{
			value = default(T);
		}

		public static void allgatheResult<T> (Aliencommunicator comm, T value)
		{

		}

		public static void allreduceResult<T> (Aliencommunicator comm, T value)
		{

		}

		public static void scanResult<T> (Aliencommunicator comm, T value)
		{

		}

		public static void alltoallResult<T> (Aliencommunicator comm, T[] value)
		{

		}

		public static void reducescatterResult<T> (Aliencommunicator comm, T[] value)
		{

		}
}
}