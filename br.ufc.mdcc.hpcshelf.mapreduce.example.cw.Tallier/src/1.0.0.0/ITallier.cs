using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.mapreduce.custom.ReduceFunction;
using br.ufc.mdcc.common.String;
using br.ufc.mdcc.common.Integer;
using br.ufc.mdcc.common.KVPair;

namespace br.ufc.mdcc.hpcshelf.mapreduce.example.cw.Tallier { 

	public interface ITallier : BaseITallier, IReduceFunction<IString,IInteger,IString,IInteger>
{


} // end main interface 

} // end namespace 
