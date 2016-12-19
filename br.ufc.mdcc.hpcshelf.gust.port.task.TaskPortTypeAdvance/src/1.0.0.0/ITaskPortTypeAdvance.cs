using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskPortType;

namespace br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeAdvance
{
	public interface ITaskPortTypeAdvance : BaseITaskPortTypeAdvance, ITaskPortType
	{
	}


	public class ITaskPortAdvance
	{
		public static object READ_CHUNK = new object();
		public static object PERFORM = new object();
		public static object CHUNK_READY = new object();
	}
}