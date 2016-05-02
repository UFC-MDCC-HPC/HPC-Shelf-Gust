using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.data.Graph;

namespace br.ufc.mdcc.hpcshelf.gust.data.UndirectedGraph
{
	public interface IUndirectedGraph<V,E> : BaseIUndirectedGraph<V,E>, IGraph<V,E>
	{
		int degreeOf(V vertex);
	}
}