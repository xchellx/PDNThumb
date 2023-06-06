/*
 * MIT License
 * 
 * Copyright (c) 2023 Yonder
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.ShellExtensions;
using Microsoft.WindowsAPICodePack.Taskbar;
using PDNThumb.IO;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace PDNThumb
{
    [DisplayName("Paint.NET Project File Thumbnail Provider")]
    [Guid("B3FBBDAB-BF71-42B8-9F37-2344CBB9C34B")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ThumbnailProvider("Paint.NET Project File Thumbnail Provider", ".pdn")]
    public class ThumbnailHandler : ThumbnailProvider, IThumbnailFromStream, IThumbnailFromFile,
        IThumbnailFromShellObject
    {
        private ThumbnailAlphaType alphaType = ThumbnailAlphaType.Unknown;

        public override ThumbnailAlphaType GetThumbnailAlphaType() => alphaType;

        public Bitmap ConstructBitmap(Stream stream, int sideSize) => ConstructBitmapImpl(stream, sideSize);

        public Bitmap ConstructBitmap(FileInfo info, int sideSize) => ConstructBitmapImpl(info.FullName, sideSize);

        public Bitmap ConstructBitmap(ShellObject shellObject, int sideSize)
            => ConstructBitmapImpl(shellObject.ParsingName, sideSize);

        private Bitmap ConstructBitmapImpl(string name, int sideSize)
        {
            using (FileStream stream = new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return ConstructBitmapImpl(stream, sideSize);
            }
        }

        private Bitmap ConstructBitmapImpl(Stream stream, int sideSize)
        {
            Bitmap thumb = PDNReader.ReadPDN3ThumbRes(stream, sideSize).Value;
            alphaType = Image.IsAlphaPixelFormat(thumb.PixelFormat)
                ? ThumbnailAlphaType.HasAlphaChannel
                : ThumbnailAlphaType.NoAlphaChannel;
            return thumb;
        }
    }
}
