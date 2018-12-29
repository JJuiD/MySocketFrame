@echo off  
rem ×ª»»ÎÄ¼þ 
rem cd %~dp0
set project="1"
set protoPath=%~dp0protogen.exe
set copyPath="../../Client/%project%/Assets/Scripts/Protocol"
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
	echo %%~xnf 
	%protoPath% -i:%%f -o:%%~nf.cs  -ns:Proto
	rem del %%~xnf
)

cd %deletePath% 
for /r %%f in (*.proto) do ( 
	cd %%~dpf
	echo "delete "%%~xnf
	del %%~xnf
)
pause  

