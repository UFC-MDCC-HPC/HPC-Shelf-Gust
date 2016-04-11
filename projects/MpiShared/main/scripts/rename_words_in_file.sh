#!/bin/bash
OLD1="Shuffler"
NEW1="Shuffle"
find . -iname *.csproj | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
find . -iname *.cs | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
find . -iname *.userprefs | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
find . -iname *.sln | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
find . -iname *.hpe | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
find . -iname *.project | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
find . -iname *.xml | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'
find . -iname *.sh | xargs sed -i 's/'"$OLD1"'/'"$NEW1"'/g'



