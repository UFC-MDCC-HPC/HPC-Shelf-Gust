#!/bin/bash
for folder in br.*
do
		qtd=$(grep -o "\." <<< "$folder" | wc -l)
		qtd=$(($qtd + 1))
		pacote=`echo $folder | cut -d'.' -f$qtd`
        echo rm $folder/$pacote.snk
        rm $folder/$pacote.snk
        echo rm $folder/$pacote.pub
        rm $folder/$pacote.pub
done;

#for folder in teste.*
#do
#		qtd=$(grep -o "\." <<< "$folder" | wc -l)
#		qtd=$(($qtd + 1))
#		pacote=`echo $folder | cut -d'.' -f$qtd`
#        echo rm $folder/$pacote.snk
#        rm $folder/$pacote.snk
#        echo rm $folder/$pacote.pub
#        rm $folder/$pacote.pub
#done;

