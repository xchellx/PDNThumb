@ECHO OFF
REM This copies file permissions and such, so it requires to run as administrator 
CD %~dp0

IF NOT EXIST .\PDNThumb_Release MD .\PDNThumb_Release

SET "XCOPY1=ECHO F | XCOPY"
SET "XCOPY2=/C /Q /R /K /O /X /Y > NUL"

IF NOT EXIST ..\PDNThumbReg\bin\x64\Release GOTO :NORELEASE
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumbReg.exe .\PDNThumb_Release\PDNThumbReg64.exe %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumbReg.exe.config .\PDNThumb_Release\PDNThumbReg64.exe.config %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumbReg.pdb .\PDNThumb_Release\PDNThumbReg64.pdb %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumb.dll .\PDNThumb_Release\PDNThumb64.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumb.pdb .\PDNThumb_Release\PDNThumb64.pdb %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\SharpShell.dll .\PDNThumb_Release\SharpShell64.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\SharpShell.pdb .\PDNThumb_Release\SharpShell64.pdb %XCOPY2%

IF NOT EXIST ..\PDNThumbReg\bin\x86\Release GOTO :NORELEASE
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumbReg.exe .\PDNThumb_Release\PDNThumbReg32.exe %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumbReg.exe.config .\PDNThumb_Release\PDNThumbReg32.exe.config %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumbReg.pdb .\PDNThumb_Release\PDNThumbReg32.pdb %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumb.dll .\PDNThumb_Release\PDNThumb32.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumb.pdb .\PDNThumb_Release\PDNThumb32.pdb %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\SharpShell.dll .\PDNThumb_Release\SharpShell32.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\SharpShell.pdb .\PDNThumb_Release\SharpShell32.pdb %XCOPY2%

REM tar is included on Windows 10 build 17063 and later
REM If you need tar on older Windows, you can find it at https://github.com/libarchive/libarchive/releases
REM Documentation can be found at https://man.freebsd.org/cgi/man.cgi?query=bsdtar&sektion=1
tar -c --fflags --xattrs --format zip --options zip:compression=deflate -f PDNThumb_Release.zip PDNThumb_Release
GOTO :END
:NORELEASE
ECHO Please build PDNThumbReg x64 and x86 first
:END
RD /S /Q .\PDNThumb_Release
PAUSE
