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
			#find . -iname *.csproj | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
			find . -iname *.cs | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
			#find . -iname *.userprefs | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
			#find . -iname *.sln | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
			find . -iname *.hpe | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
			#find . -iname *.project | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
			#find . -iname *.xml | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
			;;
		esac
	;;
esac



