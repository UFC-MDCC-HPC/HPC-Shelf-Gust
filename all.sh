#!/bin/sh
#OLD="DataContainer"
#OLDNSpace="br.ufc.mdcc.hpcshelf.gust.graph.container"
#NEW="ContainerGraph"
#NEWNSpace="br.ufc.mdcc.hpcshelf.gust.graph.container"

#E="$OLDNSpace $OLD $NEWNSpace $NEW"
#./renameComponent.sh $E
#echo $E
./removeBinObj.sh br
./removeBinObj.sh mapreduce


OLD1="mapreduce"
NEW1="gust"

./renameNameSpacePartInFolders.sh br $OLD1 $NEW1
./renameNameSpacePartInFolders.sh mapreduce $OLD1 $NEW1

./renameWords.sh $OLD1 $NEW1
./renameWords.sh map_reduce gust

#./bkp.sh

#./splitterRename.sh
#./shufflerRename.sh

#./reducao.sh

#./restore.sh

