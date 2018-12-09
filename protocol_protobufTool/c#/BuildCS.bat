@echo off  
rem ×ª»»ÎÄ¼þ 
set project="1"
set protoPath=%~dp0protogen.exe
set copyPath="../../Client/%project%/Assets/Scripts/Protocol"
rd /s /q %copyPath%
md %copyPath%
xcopy "../_protocol" %copyPath% /e /y
xcopy "./include" %copyPath% /e /y
cd %copyPath% 
for /r %%f in (*.proto) do ( 
	rem echo %%~xnf 
	rem echo %%~dpf 
	cd %%~dpf 
	%protoPath% -i:%%f -o:%%~nf.cs  -ns:Proto
	del %%~xnf
)
pause  

