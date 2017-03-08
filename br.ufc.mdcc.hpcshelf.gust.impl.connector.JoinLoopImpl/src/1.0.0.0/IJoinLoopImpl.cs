using System;
using System.Threading;
using br.ufc.pargo.hpe.backend.DGAC;
using br.ufc.pargo.hpe.basic;
using br.ufc.pargo.hpe.kinds;
using br.ufc.mdcc.hpcshelf.platform.Maintainer;
using br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction;
using br.ufc.mdcc.common.Data;
using br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction;
using br.ufc.mdcc.hpcshelf.gust.graph.InputFormat;
using br.ufc.mdcc.hpcshelf.gust.connector.JoinLoop;

namespace br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinLoopImpl
{
	public class IJoinLoopImpl<M1, TF, IKey, IValue, OKey, OValue, BF, GIF> : BaseIJoinLoopImpl<M1, TF, IKey, IValue, OKey, OValue, BF, GIF>, IJoinLoop<M1, TF, IKey, IValue, OKey, OValue, BF, GIF>
where M1:IMaintainer
where TF:ITerminateFunction<IKey, IValue, OKey, OValue>
where IKey:IData
where IValue:IData
where OKey:IData
where OValue:IData
where BF:IPartitionFunction<IKey>
where GIF:IInputFormat {
		public override void main() {
			IJoinLoopFeeder<M1,IKey,IValue,GIF> loop_feeder = new IJoinLoopFeederImpl<M1,IKey,IValue,GIF> ();
			IJoinLoopCollector<M1,TF,IKey,IValue,OKey,OValue,BF,GIF> loop_collector = new IJoinLoopCollectorImpl<M1,TF,IKey,IValue,OKey,OValue,BF,GIF> ();
			Thread t_feed = new Thread (new ThreadStart (loop_feeder.main));
			t_feed.Start ();
			loop_collector.main ();
			t_feed.Join ();
		}
	}
}
