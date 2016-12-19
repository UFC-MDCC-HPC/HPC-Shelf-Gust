using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.gust.custom.MapFunction;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.String;

namespace br.ufc.mdcc.hpcshelf.gust.example.cw.WordCounter { 

	public interface IWordCounter : BaseIWordCounter, IMapFunction<IInteger,IString,IString,IInteger>
{


} // end main interface 

} // end namespace 
