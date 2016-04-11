#!/bin/bash
for folder in br.*
do 
        pacote=$folder/$folder
        echo mkdir -p $folder/src/1.0.0.0
        mkdir -p $folder/src/1.0.0.0
done;

for folder in teste.*
do 
        pacote=$folder/$folder
        echo mkdir -p $folder/src/1.0.0.0
        mkdir -p $folder/src/1.0.0.0
done;
