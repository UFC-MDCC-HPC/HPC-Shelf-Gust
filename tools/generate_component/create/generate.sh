#!/bin/bash
case $1 in
	#''|*[!0-9]*) 
	''|[!a-z,A-Z]*) 
		echo "namespace ?" 
	;;
	*)
	case $2 in
		#''|*[!0-9]*) 
		''|[!a-z,A-Z]*) 
			echo "Componente ?" 
		;;
		*) 



OLDNAM="br.ufc.mdcc.model"
OLD1="Model"
#NEWNAM="br.ufc.mdcc.common.graph"
#NEW1="Graph"
NEWNAM=$1
NEW1=$2

gmcs GUID.cs
rm -rf $NEWNAM.$NEW1

echo "cp -a ../$OLDNAM.$OLD1 ."
cp -a ../$OLDNAM.$OLD1 .
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

find . -iname *.csproj | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
find . -iname *.cs | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
find . -iname *.hpe | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
find . -iname *.project | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
find . -iname *.xml | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'

find . -iname *.csproj | xargs sed -i 's/'"$OLDNAM"'/'"$NEWNAM"'/g'
find . -iname *.cs | xargs sed -i 's/'"$OLDNAM"'/'"$NEWNAM"'/g'
find . -iname *.hpe | xargs sed -i 's/'"$OLDNAM"'/'"$NEWNAM"'/g'
find . -iname *.project | xargs sed -i 's/'"$OLDNAM"'/'"$NEWNAM"'/g'
find . -iname *.xml | xargs sed -i 's/'"$OLDNAM"'/'"$NEWNAM"'/g'


#Create
./scripts/create.project--br.________.sh
#./scripts/create.src.1.0.0.0.sh

#Delete
./scripts/delete.bin.obj.MyClass--br.________.sh

#sn create/update
./scripts/create.sn--br._____.sh

mono GUID.exe $NEWNAM.$NEW1/$NEWNAM.$NEW1/$NEWNAM.$NEW1.csproj

rm GUID.exe



			echo "Terminate ....."
		;;
	esac
	;;
esac


