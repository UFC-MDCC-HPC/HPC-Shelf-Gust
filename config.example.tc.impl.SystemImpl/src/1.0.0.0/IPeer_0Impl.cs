// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

using br.ufc.mdcc.hpcshelf.gust.connector.JoinLoop;
using br.ufc.mdcc.hpcshelf.gust.datasource.DataSource;
using br.ufc.pargo.hpe.kinds;
using config.example.tc.System;
using System;
using System.Threading;


namespace config.example.tc.impl.SystemImpl {
    
    
    public class IPeer_0Impl : br.ufc.pargo.hpe.kinds.Application, IPeer_0 {
        
        private void Go(Object o) {
            ((Activate)(o)).go();
        }
        
        public override void main() {
          /*  IDataSource<br.ufc.mdcc.hpcshelf.platform.maintainer.DataHost.IDataHost> data_source = ((IDataSource<br.ufc.mdcc.hpcshelf.platform.maintainer.DataHost.IDataHost>)(this.Services.getPort("data_source")));
            Thread go_data_source = new Thread(new ParameterizedThreadStart(this.Go));
            go_data_source.Start(data_source);
            IJoinReadSource<br.ufc.mdcc.hpcshelf.platform.maintainer.DataHost.IDataHost, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction.IPartitionFunction<br.ufc.mdcc.common.Integer.IInteger>> join = ((IJoinReadSource<br.ufc.mdcc.hpcshelf.platform.maintainer.DataHost.IDataHost, br.ufc.mdcc.common.Integer.IInteger, br.ufc.mdcc.common.String.IString, br.ufc.mdcc.hpcshelf.gust.custom.PartitionFunction.IPartitionFunction<br.ufc.mdcc.common.Integer.IInteger>>)(this.Services.getPort("join")));
            Thread go_join = new Thread(new ParameterizedThreadStart(this.Go));
            go_join.Start(join);
            go_data_source.Join();
            go_join.Join();*/
        }
    }
}
