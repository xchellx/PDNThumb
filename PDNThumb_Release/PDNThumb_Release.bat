@ECHO OFF
REM This copies file permissions and such, so it requires to run as administrator 
REM tar is included on Windows 10 build 17063 and later
REM If you need tar on older Windows, you can find it at https://github.com/libarchive/libarchive/releases
REM Documentation can be found at https://man.freebsd.org/cgi/man.cgi?query=bsdtar&sektion=1

CD %~dp0

SET "XCOPY1=ECHO F | XCOPY"
SET "XCOPY2=/C /Q /R /K /O /X /Y > NUL"

IF NOT EXIST ..\PDNThumbReg\bin\x64\Release GOTO :NORELEASE
IF NOT EXIST .\PDNThumb_Release64 MD .\PDNThumb_Release64
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumbReg.exe .\PDNThumb_Release64\PDNThumbReg.exe %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumbReg.exe.config .\PDNThumb_Release64\PDNThumbReg.exe.config %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumbReg.pdb .\PDNThumb_Release64\PDNThumbReg.pdb %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumb.dll .\PDNThumb_Release64\PDNThumb.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\PDNThumb.pdb .\PDNThumb_Release64\PDNThumb.pdb %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\SharpShell.dll .\PDNThumb_Release64\SharpShell.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x64\Release\SharpShell.pdb .\PDNThumb_Release64\SharpShell.pdb %XCOPY2%
tar -c --fflags --xattrs --format zip --options zip:compression=deflate -f PDNThumb_Release64.zip PDNThumb_Release64

IF NOT EXIST ..\PDNThumbReg\bin\x86\Release GOTO :NORELEASE
IF NOT EXIST .\PDNThumb_Release32 MD .\PDNThumb_Release32
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumbReg.exe .\PDNThumb_Release32\PDNThumbReg.exe %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumbReg.exe.config .\PDNThumb_Release32\PDNThumbReg.exe.config %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumbReg.pdb .\PDNThumb_Release32\PDNThumbReg.pdb %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumb.dll .\PDNThumb_Release32\PDNThumb.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\PDNThumb.pdb .\PDNThumb_Release32\PDNThumb.pdb %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\SharpShell.dll .\PDNThumb_Release32\SharpShell.dll %XCOPY2%
%XCOPY1% ..\PDNThumbReg\bin\x86\Release\SharpShell.pdb .\PDNThumb_Release32\SharpShell.pdb %XCOPY2%
tar -c --fflags --xattrs --format zip --options zip:compression=deflate -f PDNThumb_Release32.zip PDNThumb_Release32

GOTO :END

:NORELEASE
ECHO Please build PDNThumbReg x64 and x86 first

:END
RD /S /Q .\PDNThumb_Release64
RD /S /Q .\PDNThumb_Release32
PAUSE
