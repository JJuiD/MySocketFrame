@echo off
rem switch *.proto To pb.h && pb.cc
rem example protoc --cpp_out=./ person.proto
rem cd %cd%
rem cd %~dp0
set project=ConsoleApplication1
set protoPath=%~dp0protoc.exe
set copyPath="../../Server/%project%/Protocol/_protocol"
rd /s /q %copyPath%
md %copyPath%
xcopy "../_protocol" %copyPath% /e /y
xcopy "./include" %copyPath% /e /y
cd %copyPath% 
set deletePath=%cd%
for /r %%f in (*.proto) do ( 
	rem echo %%~xnf 
	rem echo %%~dpf 
	cd %%~dpf 
	copy "%protoPath%" "%%~dpf"
	protoc %%~xnf --cpp_out=.
	echo "protoc -cpp.out "%%~xnf
	del /q protoc.exe
	rem del %%~xnf
)

cd %deletePath% 
for /r %%f in (*.proto) do ( 
	cd %%~dpf
	echo "delete "%%~xnf
	del %%~xnf
)
@pause

