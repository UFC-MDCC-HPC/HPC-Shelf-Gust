// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

using br.ufc.mdcc.hpcshelf.gust.computation.Gusty;
using br.ufc.mdcc.hpcshelf.gust.connector.Step;
using br.ufc.mdcc.hpcshelf.gust.connector.Join;
using br.ufc.pargo.hpe.kinds;
using gust.System;
using System;
using System.Threading;


namespace gust.impl.SystemImpl {
    
    
    public class IPeer_3Impl : br.ufc.pargo.hpe.kinds.Application, IPeer_3 {
        
        private void Go(Object o) {
            ((Activate)(o)).go();
        }
        
        public override void main() {
           /* IGusty<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.hpcshelf.gust.example.cw.Tallier.ITallier> gusty = ((IGusty<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.hpcshelf.gust.example.cw.Tallier.ITallier>)(this.Services.getPort("gusty")));
            Thread go_gusty = new Thread(new ParameterizedThreadStart(this.Go));
            go_gusty.Start(gusty);
            IJoinGustyCollector<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction.IPartitionFunction<br.ufc.mdcc.common.Integer.IInteger>, br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction.ITerminateFunction<br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger>> join = ((IJoinGustyCollector<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction.IPartitionFunction<br.ufc.mdcc.common.Integer.IInteger>, br.ufc.mdcc.hpcshelf.gust.custom.TerminateFunction.ITerminateFunction<br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger>>)(this.Services.getPort("join")));
            Thread go_join = new Thread(new ParameterizedThreadStart(this.Go));
            go_join.Start(join);
            IStepGustyFeeder<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger> step = ((IStepGustyFeeder<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger>)(this.Services.getPort("step")));
            Thread go_step = new Thread(new ParameterizedThreadStart(this.Go));
            go_step.Start(step);
            go_gusty.Join();
            go_join.Join();
            go_step.Join();*/
        }
    }
}
