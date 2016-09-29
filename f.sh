#!/bin/sh
OLD="ContainerType"
NEW="Behavior"

A="br.ufc.mdcc.hpcshelf.gust.graph.container $OLD""UndirectedRootEdgeRef br.ufc.mdcc.hpcshelf.gust.graph.container $NEW""UndirectedRootEdgeRef"
B="br.ufc.mdcc.hpcshelf.gust.graph.container $OLD""DirectedRootEdgeRef br.ufc.mdcc.hpcshelf.gust.graph.container $NEW""DirectedRootEdgeRef"
C="br.ufc.mdcc.hpcshelf.gust.graph.container $OLD""Undirected br.ufc.mdcc.hpcshelf.gust.graph.container $NEW""Undirected"
D="br.ufc.mdcc.hpcshelf.gust.graph.container $OLD""Directed br.ufc.mdcc.hpcshelf.gust.graph.container $NEW""Directed"
E="br.ufc.mdcc.hpcshelf.gust.graph.container $OLD br.ufc.mdcc.hpcshelf.gust.graph.container $NEW"

./renameComponent.sh $A
./renameComponent.sh $B
./renameComponent.sh $C
./renameComponent.sh $D
./renameComponent.sh $E

