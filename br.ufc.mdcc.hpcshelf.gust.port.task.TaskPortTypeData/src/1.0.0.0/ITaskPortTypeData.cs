using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskPortType;

namespace br.ufc.mdcc.hpcshelf.gust.port.task.TaskPortTypeData
{
	public interface ITaskPortTypeData : BaseITaskPortTypeData, ITaskPortType
	{
	}

	public class ITaskPortData
	{
		public static object READ_SOURCE = new object();
		public static object TERMINATE = new object();
		public static object WRITE_SINK = new object();
	}
}