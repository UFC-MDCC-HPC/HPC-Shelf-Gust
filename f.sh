#!/bin/sh
OLD="DataContainer"
OLDNSpace="br.ufc.mdcc.hpcshelf.gust.graph.container"
NEW="ContainerGraph"
NEWNSpace="br.ufc.mdcc.hpcshelf.gust.graph.container"

#A="$OLDNSpace $OLD""UndirectedRootEdgeRef $NEWNSpace $NEW""UndirectedRootEdgeRef"
#B="$OLDNSpace $OLD""DirectedRootEdgeRef $NEWNSpace $NEW""DirectedRootEdgeRef"
#C="$OLDNSpace $OLD""Undirected $NEWNSpace $NEW""Undirected"
#D="$OLDNSpace $OLD""Directed $NEWNSpace $NEW""Directed"
#E="$OLDNSpace $OLD $NEWNSpace $NEW"

E="$OLDNSpace $OLD $NEWNSpace $NEW"
./renameComponent.sh $E
#echo $E


#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerUndirectedEWeightedImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerDirectedEWeightedImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerUndirectedVWeightedImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerDirectedVWeightedImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerUndirectedEImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerDirectedEImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerUndirectedVImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerDirectedVImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirectedE
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirectedV
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerDirectedE
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerDirectedV
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirected
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerDirected
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer
#####################################################################3
#####################################################################3
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainer

#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerDirected
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerDirectedE
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerDirectedV

#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirected
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirectedE
#br.ufc.mdcc.hpcshelf.gust.graph.container.DataContainerUndirectedV

#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerDirectedEImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerDirectedEWeightedImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerDirectedVImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerDirectedVWeightedImpl

#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerUndirectedEImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerUndirectedEWeightedImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerUndirectedVImpl
#br.ufc.mdcc.hpcshelf.gust.graph.container.impl.DataContainerUndirectedVWeightedImpl
#########################################################################

