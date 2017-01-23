#!/bin/bash
case $1 in
	''|[!a-z,A-Z]*) 
		echo "palavra inicial: br ?" 
	;;
	*) 
		case $2 in
			''|[!a-z,A-Z]*) 
				echo "old part?" 
			;;
			*)
				case $3 in
					''|[!a-z,A-Z]*) 
						echo "new part?" 
					;;
					*) 
						src=$2
						tgt=$3
						for folder in $1''.*
						do
								qtd=$(grep -o "\." <<< "$folder" | wc -l)
								space=''
								newspace=''
								for i in `seq 1 $qtd`
								do
									tmp=`echo $folder | cut -d'.' -f$i`
									case $tmp in
										$src) 
											newspace=$newspace''$tgt'.'
										;;
										*) 
											newspace=$newspace''$tmp'.'
										;;
									esac

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
								T=$newspace''$pacote
								case $S in
									$T) 
									;;
									*) 
										#echo $T
										mv $S $T
										mv $T/$S $T/$T
										mv $T/$T/$S.csproj $T/$T/$T.csproj

									;;
								esac
						done;
					;;
				esac
			;;
		esac
	;;
esac












