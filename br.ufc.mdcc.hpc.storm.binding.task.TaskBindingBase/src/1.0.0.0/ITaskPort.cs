using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.task.TaskPortType;
using System.Threading;
using System;

namespace br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase
{
	public interface ITaskPort<T> : BaseITaskPort<T>
		where T:ITaskPortType
	{
		// (1) invokes and waits for action completion.
		void invoke(object action_id);

		// (2) invokes and returns a future object for further testing or waiting of action completion.
		void invoke(object action_id, out IActionFuture future);

		// (3) As (2), but it only completes after executing a procedure.
		Thread invoke(object action_id, Action reaction, out IActionFuture future);
	}
	
	public interface IActionFuture
	{
		void wait();
		bool test();
		IActionFutureSet createSet();
	}

	public interface IActionFutureSet 
	{
		void addAction(IActionFuture new_future);

		void waitAll();
		IActionFuture waitAny();

		bool testAll();
		IActionFuture testAny();

		IActionFuture[] Completed { get; }
		IActionFuture[] Pending { get; }
	}
}