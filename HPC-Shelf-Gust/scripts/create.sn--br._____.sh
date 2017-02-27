#!/bin/bash
for folder in br.*
do
		qtd=$(grep -o "\." <<< "$folder" | wc -l)
		qtd=$(($qtd + 1))
		pacote=`echo $folder | cut -d'.' -f$qtd`
        echo $folder " - " $pacote
		sn -k $folder/$pacote.snk
		sn -p $folder/$pacote.snk $folder/$pacote.pub
done;

for folder in config.*
do
		qtd=$(grep -o "\." <<< "$folder" | wc -l)
		qtd=$(($qtd + 1))
		pacote=`echo $folder | cut -d'.' -f$qtd`
        echo $folder " - " $pacote
		sn -k $folder/$pacote.snk
		sn -p $folder/$pacote.snk $folder/$pacote.pub
done;

