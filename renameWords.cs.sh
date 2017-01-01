#!/bin/bash
case $1 in
	'') 
		echo "source?" 
	;;
	*) 
		case $2 in
			'') 
				echo "target?" 
			;;
			*)
			OLD1=$1
			NEW1=$2
			find . -iname *.cs | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
			;;
		esac
	;;
esac



