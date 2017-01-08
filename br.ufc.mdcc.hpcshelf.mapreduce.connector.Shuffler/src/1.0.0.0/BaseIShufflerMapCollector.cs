/* AUTOMATICALLY GENERATE CODE */

using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.hpc.storm.binding.task.TaskBindingBase;
using br.ufc.mdcc.hpcshelf.mapreduce.port.task.TaskPortTypeAdvance;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpc.storm.binding.channel.Binding;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.mapreduce.connector.Shuffler
{
	public interface BaseIShufflerMapCollector<M1,TKey,TValue,PF> : ISynchronizerKind 
		where M1:IMaintainer
		where PF:IPartitionFunction<TKey>
		where TKey:IData
		where TValue:IData
	{
		ITaskPort<ITaskPortTypeAdvance> Task_binding_shuffle {get;}
	}
}