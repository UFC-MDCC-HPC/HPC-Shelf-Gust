#!/bin/bash
tab='\t'
for folder in br.*
do 
 echo -e "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" > $folder/.project
 echo -e "<projectDescription>" >> $folder/.project
 echo -e "$tab<name>$folder</name>" >> $folder/.project
 echo -e "$tab<comment></comment>" >> $folder/.project
 echo -e "$tab<projects>" >> $folder/.project
 echo -e "$tab</projects>" >> $folder/.project
 echo -e "$tab<buildSpec>" >> $folder/.project
 echo -e "$tab $tab<buildCommand>" >> $folder/.project
 echo -e "$tab $tab $tab<name>org.emonic.base.EMonic_Builder</name>" >> $folder/.project
 echo -e "$tab $tab $tab<arguments>" >> $folder/.project
 echo -e "$tab $tab $tab</arguments>" >> $folder/.project
 echo -e "$tab $tab</buildCommand>" >> $folder/.project
 echo -e "$tab</buildSpec>" >> $folder/.project
 echo -e "$tab<natures>" >> $folder/.project
 echo -e "$tab $tab<nature>org.emonic.base.EMonic_Nature</nature>" >> $folder/.project
 echo -e "$tab</natures>" >> $folder/.project
 echo -e "</projectDescription>" >> $folder/.project
done;

for folder in teste.*
do 
 echo -e "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" > $folder/.project
 echo -e "<projectDescription>" >> $folder/.project
 echo -e "$tab<name>$folder</name>" >> $folder/.project
 echo -e "$tab<comment></comment>" >> $folder/.project
 echo -e "$tab<projects>" >> $folder/.project
 echo -e "$tab</projects>" >> $folder/.project
 echo -e "$tab<buildSpec>" >> $folder/.project
 echo -e "$tab $tab<buildCommand>" >> $folder/.project
 echo -e "$tab $tab $tab<name>org.emonic.base.EMonic_Builder</name>" >> $folder/.project
 echo -e "$tab $tab $tab<arguments>" >> $folder/.project
 echo -e "$tab $tab $tab</arguments>" >> $folder/.project
 echo -e "$tab $tab</buildCommand>" >> $folder/.project
 echo -e "$tab</buildSpec>" >> $folder/.project
 echo -e "$tab<natures>" >> $folder/.project
 echo -e "$tab $tab<nature>org.emonic.base.EMonic_Nature</nature>" >> $folder/.project
 echo -e "$tab</natures>" >> $folder/.project
 echo -e "</projectDescription>" >> $folder/.project
done;

