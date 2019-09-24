@echo off
title Binary Clearing

cd bin
md projects
rmdir /s /q blob_storage
rmdir /s /q app.publish
rmdir /s /q GPUCache
del *.pdb
del *.log
del *.config
del *.manifest
del *.application
del preferences.bin
echo f | xcopy /y/f "../changelog.txt" "changelog.txt"
echo f | xcopy /y/f "../LICENSE" "LICENSE"
echo d | xcopy /y/s/e "../example" "projects/example"