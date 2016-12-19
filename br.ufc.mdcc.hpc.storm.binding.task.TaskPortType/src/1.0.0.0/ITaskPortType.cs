using br.ufc.pargo.hpe.kinds;
using System.Collections.Generic;

namespace br.ufc.mdcc.hpc.storm.binding.task.TaskPortType
{
	public interface ITaskPortType : BaseITaskPortType
	{
	}

	public class ActionDef
	{
		public static IDictionary<object,int> action_ids = new Dictionary<object,int>();
	}
}