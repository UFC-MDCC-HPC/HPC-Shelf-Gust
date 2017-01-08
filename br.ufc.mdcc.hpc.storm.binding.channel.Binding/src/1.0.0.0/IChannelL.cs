using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.binding.context.PartnerKind;

namespace br.ufc.mdcc.hpc.storm.binding.channel.Binding { 

public interface IChannelL<PKL> : BaseIChannelL<PKL>, Intercommunicator
where PKL:IPartnerKind
{


} // end main interface 

} // end namespace 
