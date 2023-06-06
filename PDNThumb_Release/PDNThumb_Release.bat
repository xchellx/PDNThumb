@ECHO OFF
REM This copies file permissions and such, so it requires to run as administrator 
REM tar is included on Windows 10 build 17063 and later
REM If you need tar on older Windows, you can find it at https://github.com/libarchive/libarchive/releases
REM Documentation can be found at https://man.freebsd.org/cgi/man.cgi?query=bsdtar&sektion=1

CD %~dp0

SET "XCOPY1=ECHO F | XCOPY"
SET "XCOPY2=/C /Q /R /K /O /X /Y > NUL"

IF NOT EXIST ..\PDNThumbReg\bin\x64\Release\PDNThumbReg.exe GOTO :NORELEASE
IF NOT EXIST ..\PDNThumbReg\bin\x86\Release\PDNThumbReg.exe GOTO :NORELEASE

IF NOT EXIST .\PDNThumb_Release\x64 MD .\PDNThumb_Release\x64
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\Microsoft.WindowsAPICodePack.dll .\PDNThumb_Release\x64\Microsoft.WindowsAPICodePack.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\Microsoft.WindowsAPICodePack.Shell.dll .\PDNThumb_Release\x64\Microsoft.WindowsAPICodePack.Shell.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\Microsoft.WindowsAPICodePack.ShellExtensions.dll .\PDNThumb_Release\x64\Microsoft.WindowsAPICodePack.ShellExtensions.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumb.dll .\PDNThumb_Release\x64\PDNThumb.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumb.pdb .\PDNThumb_Release\x64\PDNThumb.pdb %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumbReg.exe .\PDNThumb_Release\x64\PDNThumbReg.exe %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumbReg.exe.config .\PDNThumb_Release\x64\PDNThumbReg.exe.config %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumbReg.pdb .\PDNThumb_Release\x64\PDNThumbReg.pdb %XCOPY2%

IF NOT EXIST .\PDNThumb_Release\x86 MD .\PDNThumb_Release\x86
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\Microsoft.WindowsAPICodePack.dll .\PDNThumb_Release\x86\Microsoft.WindowsAPICodePack.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\Microsoft.WindowsAPICodePack.Shell.dll .\PDNThumb_Release\x86\Microsoft.WindowsAPICodePack.Shell.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\Microsoft.WindowsAPICodePack.ShellExtensions.dll .\PDNThumb_Release\x86\Microsoft.WindowsAPICodePack.ShellExtensions.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumb.dll .\PDNThumb_Release\x86\PDNThumb.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumb.pdb .\PDNThumb_Release\x86\PDNThumb.pdb %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumbReg.exe .\PDNThumb_Release\x86\PDNThumbReg.exe %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumbReg.exe.config .\PDNThumb_Release\x86\PDNThumbReg.exe.config %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumbReg.pdb .\PDNThumb_Release\x86\PDNThumbReg.pdb %XCOPY2%

IF EXIST .\PDNThumb_Release.zip DEL /F /Q .\PDNThumb_Release.zip
tar -c --fflags --xattrs --format zip --options zip:compression=deflate -f PDNThumb_Release.zip PDNThumb_Release

GOTO :END

:NORELEASE
ECHO Please build PDNThumbReg x64 and x86 first

:END
IF EXIST .\PDNThumb_Release RD /S /Q .\PDNThumb_Release
PAUSE
