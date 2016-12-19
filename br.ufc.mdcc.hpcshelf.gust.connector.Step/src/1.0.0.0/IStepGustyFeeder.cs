using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;

namespace br.ufc.mdcc.hpcshelf.gust.connector.Step
{
	public interface IStepGustyFeeder<M0,TKey, TValue> : BaseIStepGustyFeeder<M0,TKey, TValue>
		where M0:IMaintainer
		where TKey:IData
		where TValue:IData
	{
	}
}