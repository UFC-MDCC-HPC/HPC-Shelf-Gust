using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;
using br.ufc.mdcc.hpcshelf.gust.graph.Vertex;
using br.ufc.mdcc.hpcshelf.gust.example.tc.DataTriangle;

namespace br.ufc.mdcc.hpcshelf.gust.example.tc.TerminateFunctionTC
{
	public interface ITerminateFunctionTC : BaseITerminateFunctionTC, ITerminateFunction<IVertex, IDataTriangle, IVertex, IDataTriangle>
	{
	}
}