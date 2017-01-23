using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.ReduceFunction;

namespace br.ufc.mdcc.hpcshelf.gust.example.tc.TriangleCount
{
	public interface ITriangleCount : BaseITriangleCount, IReduceFunction
	{
	}
}