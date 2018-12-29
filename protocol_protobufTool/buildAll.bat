@echo off
echo start complie c# .Proto
cd ./c#
start "c#" cmd /c call BuildCS.bat
rem call BuildCS.bat
echo start complie c++ .Proto
cd ../c++
start "c++" cmd /c call build.bat
rem call build.bat
@pause