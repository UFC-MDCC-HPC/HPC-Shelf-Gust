./renameComponent.sh br.ufc.mdcc.hpcshelf.gust.impl.connector ShufflerImpl br.ufc.mdcc.hpcshelf.gust.impl.connector StepImpl
./renameComponent.sh br.ufc.mdcc.hpcshelf.gust.connector Shuffler br.ufc.mdcc.hpcshelf.gust.connector Step
./renameWords.sh shuffler step
./renameWords.sh StepReduce StepGusty

mv br.ufc.mdcc.hpcshelf.gust.impl.connector.StepImpl/src/1.0.0.0/BaseIShufflerMapCollectorImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.StepImpl/src/1.0.0.0/BaseIStepGustyCollectorImpl.cs
mv br.ufc.mdcc.hpcshelf.gust.impl.connector.StepImpl/src/1.0.0.0/BaseIShufflerReduceFeederImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.StepImpl/src/1.0.0.0/BaseIStepGustyFeederImpl.cs
mv br.ufc.mdcc.hpcshelf.gust.impl.connector.StepImpl/src/1.0.0.0/IShufflerMapCollectorImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.StepImpl/src/1.0.0.0/IStepGustyCollectorImpl.cs
mv br.ufc.mdcc.hpcshelf.gust.impl.connector.StepImpl/src/1.0.0.0/IShufflerReduceFeederImpl.cs br.ufc.mdcc.hpcshelf.gust.impl.connector.StepImpl/src/1.0.0.0/IStepGustyFeederImpl.cs

mv br.ufc.mdcc.hpcshelf.gust.connector.Step/src/1.0.0.0/BaseIShufflerMapCollector.cs br.ufc.mdcc.hpcshelf.gust.connector.Step/src/1.0.0.0/BaseIStepGustyCollector.cs
mv br.ufc.mdcc.hpcshelf.gust.connector.Step/src/1.0.0.0/BaseIShufflerReduceFeeder.cs br.ufc.mdcc.hpcshelf.gust.connector.Step/src/1.0.0.0/BaseIStepGustyFeeder.cs
mv br.ufc.mdcc.hpcshelf.gust.connector.Step/src/1.0.0.0/IShufflerMapCollector.cs br.ufc.mdcc.hpcshelf.gust.connector.Step/src/1.0.0.0/IStepGustyCollector.cs
mv br.ufc.mdcc.hpcshelf.gust.connector.Step/src/1.0.0.0/IShufflerReduceFeeder.cs br.ufc.mdcc.hpcshelf.gust.connector.Step/src/1.0.0.0/IStepGustyFeeder.cs


./renameWords.sh map_collector gusty_collector
./renameWords.sh MapCollector GustyCollector

