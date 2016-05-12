using br.ufc.pargo.hpe.kinds;

namespace br.ufc.mdcc.hpcshelf.gust.binding.task.TaskBindingJoin
{
	public interface ITaskBindingJoin : BaseITaskBindingJoin
	{
	}
	
	public class ITaskPortJoin
	{
		public static object READ_SOURCE = new object();
		public static object TERMINATE = new object();
		public static object WRITE_SINK = new object();
	}
}