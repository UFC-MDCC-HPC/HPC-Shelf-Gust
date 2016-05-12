using br.ufc.pargo.hpe.kinds;

namespace br.ufc.mdcc.hpcshelf.gust.binding.task.TaskBindingGusty
{
	public interface ITaskBindingGusty : BaseITaskBindingGusty
	{
	}
	
	public class ITaskPortGusty
	{
		public static object READ_CHUNK = new object();
		public static object PERFORM = new object();
		public static object CHUNK_READY = new object();
	}
}