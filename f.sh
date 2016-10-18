#!/bin/sh
OLD="DataContainer"
OLDNSpace="br.ufc.mdcc.hpcshelf.gust.graph.container"
NEW="ContainerGraph"
NEWNSpace="br.ufc.mdcc.hpcshelf.gust.graph.container"

E="$OLDNSpace $OLD $NEWNSpace $NEW"
./renameComponent.sh $E
#echo $E


