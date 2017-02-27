./renameComponent.sh br.ufc.mdcc.hpcshelf.gust.impl.connector SplitterImpl br.ufc.mdcc.hpcshelf.gust.impl.connector JoinImpl
./renameComponent.sh br.ufc.mdcc.hpcshelf.gust.connector Splitter br.ufc.mdcc.hpcshelf.gust.connector Join
./renameWords.sh splitter join
./renameWords.sh JoinReduce JoinGusty

mv br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/BaseISplitterMapFeederImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/BaseIJoinGustyFeederImpl.cs
mv br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/BaseISplitterReadSourceImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/BaseIJoinReadSourceImpl.cs
mv br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/BaseISplitterReduceCollectorImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/BaseIJoinGustyCollectorImpl.cs
mv br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/BaseISplitterWriteSinkImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/BaseIJoinWriteSinkImpl.cs
mv br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/ISplitterMapFeederImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/IJoinGustyFeederImpl.cs
mv br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/ISplitterReadSourceImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/IJoinReadSourceImpl.cs
mv br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/ISplitterReduceCollectorImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/IJoinGustyCollectorImpl.cs
mv br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/ISplitterWriteSinkImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.JoinImpl/src/1.0.0.0/IJoinWriteSinkImpl.cs

mv br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/BaseISplitterMapFeeder.cs br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/BaseIJoinGustyFeeder.cs
mv br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/BaseISplitterReadSource.cs br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/BaseIJoinReadSource.cs
mv br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/BaseISplitterReduceCollector.cs br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/BaseIJoinGustyCollector.cs
mv br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/BaseISplitterWriteSink.cs br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/BaseIJoinWriteSink.cs
mv br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/ISplitterMapFeeder.cs br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/IJoinGustyFeeder.cs
mv br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/ISplitterReadSource.cs br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/IJoinReadSource.cs
mv br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/ISplitterReduceCollector.cs br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/IJoinGustyCollector.cs
mv br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/ISplitterWriteSink.cs br.ufc.mdcc.hpcshelf.gust.connector.Join/src/1.0.0.0/IJoinWriteSink.cs



./renameWords.sh map_feeder gusty_feeder
./renameWords.sh MapFeeder GustyFeeder







