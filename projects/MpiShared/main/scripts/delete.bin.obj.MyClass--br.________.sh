#!/bin/bash
for folder in br.*
do 
        pacote=$folder/$folder
        echo rm -rf $pacote/MyClass.cs
        rm -rf $pacote/MyClass.cs
        echo rm -rf $pacote/bin
        rm -rf $pacote/bin
        echo rm -rf $pacote/obj
        rm -rf $pacote/obj
done;

for folder in teste.*
do 
        pacote=$folder/$folder
        echo rm -rf $pacote/MyClass.cs
        rm -rf $pacote/MyClass.cs
        echo rm -rf $pacote/bin
        rm -rf $pacote/bin
        echo rm -rf $pacote/obj
        rm -rf $pacote/obj
done;
