#!/bin/bash
rm bin/*
rm -rf HPC-Shelf-Gust/HPC-Shelf-Gust/bin HPC-Shelf-Gust/HPC-Shelf-Gust/obj

case $1 in
	''|[!a-z,A-Z]*) 
		echo "initial folder name: br ?" 
	;;
	*) 
		for folder in $1''.*
		do
				qtd=$(grep -o "\." <<< "$folder" | wc -l)
				space=''
				for i in `seq 1 $qtd`
				do
					tmp=`echo $folder | cut -d'.' -f$i`
					space=$space''$tmp'.'
				done
				qtd=$(($qtd + 1))
				pacote=`echo $folder | cut -d'.' -f$qtd`
	
				#echo $folder " - " $pacote
				#echo $space " - " $pacote
				#echo $space''$pacote
				#echo $newspace''$pacote
				#ls -1 $space''$pacote'/'$space''$pacote'/'$space''$pacote.csproj
	
				S=$space''$pacote
				rm -rf $S/$S/bin
				rm -rf $S/$S/obj
				echo "remove $S/$S/bin"
				echo "remove $S/$S/obj"
		done;
	;;
esac



