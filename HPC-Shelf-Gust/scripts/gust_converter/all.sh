#!/bin/sh
./removeBinObj.sh br
./removeBinObj.sh config

OLD1="mapreduce"
NEW1="gust"

./renameNameSpacePartInFolders.sh br $OLD1 $NEW1
#./renameNameSpacePartInFolders.sh config $OLD1 $NEW1

./renameWords.sh $OLD1 $NEW1
./renameWords.sh map_reduce gust

./bkp.sh

./splitterRename.sh
./shufflerRename.sh

./reducao.sh

./restore.sh

