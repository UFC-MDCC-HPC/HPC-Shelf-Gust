// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

using br.ufc.mdcc.hpcshelf.gust.connector.Join;
using br.ufc.pargo.hpe.kinds;
using config.example.tc.System;
using System;
using System.Threading;


namespace config.example.tc.impl.SystemImpl {
    
    
    public class IPeer_2Impl : br.ufc.pargo.hpe.kinds.Application, IPeer_2 {
        
        private void Go(Object o) {
            ((Activate)(o)).go();
        }
        
        public override void main() {
          /*  IMapper<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.hpcshelf.gust.example.cw.WordCounter.IWordCounter> mapper = ((IMapper<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.hpcshelf.gust.example.cw.WordCounter.IWordCounter>)(this.Services.getPort("mapper")));
            Thread go_mapper = new Thread(new ParameterizedThreadStart(this.Go));
            go_mapper.Start(mapper);
            IJoinGustyFeeder<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString> join = ((IJoinGustyFeeder<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString>)(this.Services.getPort("join")));
            Thread go_join = new Thread(new ParameterizedThreadStart(this.Go));
            go_join.Start(join);
            IStepGustyCollector<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction.IPartitionFunction<br.ufc.mdcc.common.String.IString>> step = ((IStepGustyCollector<br.ufc.mdcc.hpcshelf.platform.maintainer.ComputeHost.IComputeHost, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction.IPartitionFunction<br.ufc.mdcc.common.String.IString>>)(this.Services.getPort("step")));
            Thread go_step = new Thread(new ParameterizedThreadStart(this.Go));
            go_step.Start(step);
            go_mapper.Join();
            go_join.Join();
            go_step.Join();*/
        }
    }
}
