using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpc.storm.kind.Binding;
using System;
using MPI;

namespace br.ufc.mdcc.hpc.storm.binding.channel.Binding
{

	public interface IChannel : BaseIChannel, IPeer, Aliencommunicator
	{


	}

}