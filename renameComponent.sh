#!/bin/bash
#TMP="./"

if [[ "$1" =~ ^br.* ]]; then
	case $2 in
		#''|*[!0-9]*) 
		''|[!a-z,A-Z]*) 
			echo "Componente Fonte?" 
		;;
		*) 
			if [[ "$3" =~ ^br.* ]]; then
				case $4 in
					#''|*[!0-9]*) 
					''|[!a-z,A-Z]*) 
						echo "Componente Destino?" 
					;;
					*) 

						OLDNAM=$1
						OLD1=$2
						NEWNAM=$3
						NEW1=$4

						echo "mv $OLDNAM.$OLD1 $NEWNAM.$NEW1"
						mv $OLDNAM.$OLD1 $NEWNAM.$NEW1
						echo "mv $NEWNAM.$NEW1/$OLDNAM.$OLD1 $NEWNAM.$NEW1/$NEWNAM.$NEW1"
						mv $NEWNAM.$NEW1/$OLDNAM.$OLD1 $NEWNAM.$NEW1/$NEWNAM.$NEW1
						echo "mv $NEWNAM.$NEW1/$NEWNAM.$NEW1/$OLDNAM.$OLD1.csproj $NEWNAM.$NEW1/$NEWNAM.$NEW1/$NEWNAM.$NEW1.csproj"
						mv $NEWNAM.$NEW1/$NEWNAM.$NEW1/$OLDNAM.$OLD1.csproj $NEWNAM.$NEW1/$NEWNAM.$NEW1/$NEWNAM.$NEW1.csproj
						echo "mv $NEWNAM.$NEW1/src/1.0.0.0/I$OLD1.cs $NEWNAM.$NEW1/src/1.0.0.0/I$NEW1.cs"
						mv $NEWNAM.$NEW1/src/1.0.0.0/I$OLD1.cs $NEWNAM.$NEW1/src/1.0.0.0/I$NEW1.cs
						echo "mv $NEWNAM.$NEW1/src/1.0.0.0/BaseI$OLD1.cs $NEWNAM.$NEW1/src/1.0.0.0/BaseI$NEW1.cs"
						mv $NEWNAM.$NEW1/src/1.0.0.0/BaseI$OLD1.cs $NEWNAM.$NEW1/src/1.0.0.0/BaseI$NEW1.cs
						echo "mv $NEWNAM.$NEW1/$OLD1.hpe $NEWNAM.$NEW1/$NEW1.hpe"
						mv $NEWNAM.$NEW1/$OLD1.hpe $NEWNAM.$NEW1/$NEW1.hpe
						echo "mv $NEWNAM.$NEW1/$OLD1.pub $NEWNAM.$NEW1/$NEW1.pub"
						mv $NEWNAM.$NEW1/$OLD1.pub $NEWNAM.$NEW1/$NEW1.pub
						echo "mv $NEWNAM.$NEW1/$OLD1.snk $NEWNAM.$NEW1/$NEW1.snk"
						mv $NEWNAM.$NEW1/$OLD1.snk $NEWNAM.$NEW1/$NEW1.snk
						
						
						find . -iname *.csproj | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
						find . -iname *.cs | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
						find . -iname *.userprefs | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
						find . -iname *.sln | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
						find . -iname *.hpe | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
						find . -iname *.project | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
						find . -iname *.xml | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'

						find . -iname *.csproj | xargs sed -i 's/'"$OLDNAM.$NEW1"'/'"$NEWNAM.$NEW1"'/g'
						find . -iname *.cs | xargs sed -i 's/'"$OLDNAM.$NEW1"'/'"$NEWNAM.$NEW1"'/g'
						find . -iname *.userprefs | xargs sed -i 's/'"$OLDNAM.$NEW1"'/'"$NEWNAM.$NEW1"'/g'
						find . -iname *.sln | xargs sed -i 's/'"$OLDNAM.$NEW1"'/'"$NEWNAM.$NEW1"'/g'
						find . -iname *.hpe | xargs sed -i 's/'"$OLDNAM.$NEW1"'/'"$NEWNAM.$NEW1"'/g'
						find . -iname *.project | xargs sed -i 's/'"$OLDNAM.$NEW1"'/'"$NEWNAM.$NEW1"'/g'
						find . -iname *.xml | xargs sed -i 's/'"$OLDNAM.$NEW1"'/'"$NEWNAM.$NEW1"'/g'

						find . -iname *.hpe | xargs sed -i 's/'"name=\"$NEW1\" package=\"$OLDNAM"'/'"name=\"$NEW1\" package=\"$NEWNAM"'/g'
						find . -iname *.hpe | xargs sed -i 's/'"name=\"$NEW1\" packagePath=\"$OLDNAM"'/'"name=\"$NEW1\" packagePath=\"$NEWNAM"'/g'

						
						echo "Terminating ....."
					;;
				esac
			else
				echo "namespace br* destino?" 
			fi
		;;
	esac
else
	echo "namespace br* fonte ?" 
fi







