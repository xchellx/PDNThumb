# Obsolete
There is no point of this when Paint.NET has it's own Shell Extension for Thumbnails. However, registering it manually
can only be done by the registry. See: [How to register PaintDotNet.ShellExtension.x64.dll manually?
](https://forums.getpaint.net/topic/122627-how-to-register-paintdotnetshellextensionx64dll-manually/)

Using that installer output, I wrote a registry file that works for Paint.NET 5.0.6 (as of now)
[here](https://gist.github.com/xchellx/a0fc1e837273dc49dd253b5b761c4aff).

# Paint.NET Project File Thumbnail Provider
Provides thumbnails for Paint.NET project files in Windows Explorer.

## Table of Contents
- [Info](#info)
- [Building](#building)
- [Installing](#building)
- [Credits](#credits)
- [License](#license)

## Info
Windows Explorer allows for extensions to it's functionality (such as the context menu, preview panel, thumbnails,
etc.). SharpShell allows one to do this in C# managed code. This is a SharpShell extension for Windows Explorer that
displays thumbnails for .pdn Paint.NET project files.

## Building
Open PDNThumb.sln in Visual Studio 2022 (unsure if older versions of VS will work) and click Build -> Build Solution.

To create a release: Batch build the `x86` and `x64` configurations in `Release` then run `PDNThumb_Release.bat` as
administrator in the `PDNThumb_Release` folder. The release script assumes you have Windows 10 build 17063 and later
because it includes tar. If you need tar on older Windows, you can find it at
[libarchive/libarchive/releases](https://github.com/libarchive/libarchive/releases). It requires administrator rights
because it uses the `/O /X` arguments for `XCOPY` which require administrator rights to use (or else it gives
`Access Denied`). If you feel these extra file attributes are not worthy to copy over for a release, you can omit these
from the `%XCOPY2%` variable in the build script and not have to run the script as administrator.

The purpose of `PDNThumbTester` is to test the functionality of reading the thumbnail, which essentially is what the
thumbnail handler invokes.

## Installing
To install `PDNThumb`:

1. Place `PDNThumbReg.exe` alongside it's depencies (as seen in the release script/zip) at a place where you want
`PDNThumb` to be installed (once Windows has a handle on them it wont be deletable unless you uninstall).

2. Run `PDNThumbReg.exe`.

3. If no error occurs and you see a success message, then either:

    a. restart your computer.

    b. ***or*** right click `Windows Explorer` in `Task Manager` then click `Restart`.

You can uninstall `PDNThumb` by repeating this process.

`PDNThumbReg` supports command line arguments:
```
USAGE: PDNThumbReg [hsyiu]
 h = Print this message
 s = Be completely silent (no console output; implies [y])
 y = Assume yes to all "ask yes/no" prompts and "press any key ..." prompts
 i = Force register (override checking if registered AKA. regasm behavior
 u = Force unregister (override checking if registered AKA. regasm behavior
NOTE: These options all go within one argument. Each option is a single character. The
limit of what characters are counted for is 5 characters. Options can be repeated but there
is no usage in that.
```

## Credits
- [Paint.NET by Rick Brewster](https://getpaint.net/)
- [Microsoft-WindowsAPICodePack-ShellExtensions by Microsoft and contributors](https://github.com/wferris722/Windows-API-Code-Pack-1.1)

## License
[MIT](https://choosealicense.com/licenses/mit/)
```
MIT License

Copyright (c) 2023 Yonder

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

```
