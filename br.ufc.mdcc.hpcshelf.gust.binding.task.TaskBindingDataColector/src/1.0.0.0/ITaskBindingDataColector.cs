using br.ufc.pargo.hpe.kinds;

namespace br.ufc.mdcc.hpcshelf.gust.binding.task.TaskBindingDataColector
{
	public interface ITaskBindingDataColector : BaseITaskBindingDataColector
	{
	}
	public class ITaskPortDataColector
	{
	    public static object NEW_JOB = new object();
	    public static object READ_CHUNK = new object();
		public static object PERFORM = new object();
		public static object CHUNK_READY = new object();
	}
}