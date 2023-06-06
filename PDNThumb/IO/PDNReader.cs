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

using PDNThumb.Properties;
using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Xml;

namespace PDNThumb.IO
{
    public class PDNReader
    {
        public static (bool Success, Bitmap Value) ReadPDN3ThumbRes(Stream reader, int sideSize)
        {
            (bool success, Bitmap thumb) = ReadPDN3Thumb(reader);
            Bitmap resThumb = null;
            if (success && sideSize < thumb.Width)
            {
                try
                {
                    int resWidth;
                    int resHeight;
                    if (sideSize == 1)
                    {
                        resWidth = 1;
                        resHeight = 1;
                    }
                    else
                    {
                        int scalingFactor = Math.Max(thumb.Width, thumb.Height);
                        resWidth = sideSize * thumb.Width / scalingFactor;
                        resHeight = sideSize * thumb.Height / scalingFactor;
                    }

                    resThumb = new Bitmap(resWidth, resHeight);
                    using (Graphics resGraphics = Graphics.FromImage(resThumb))
                    {
                        resGraphics.Clear(Color.Transparent);
                        resGraphics.CompositingMode = CompositingMode.SourceOver;
                        resGraphics.CompositingQuality = CompositingQuality.HighQuality;
                        resGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        resGraphics.DrawImage(thumb, 0, 0, resWidth, resHeight);
                    }
                    return (true, resThumb);
                }
#if !DEBUG
                catch (Exception)
                {
                    resThumb?.Dispose();
                    return (false, Resources.fallback);
                }
#endif
                finally
                {
                    thumb.Dispose();
                }
            }
            else
                return (success, thumb);
        }

        public static (bool Success, Bitmap Value) ReadPDN3Thumb(Stream reader)
        {
#if !DEBUG
            try
            {
#endif
            byte[] buffX4 = new byte[4] { 0, 0, 0, 0 };

            // "PDN3"
            if (reader.Read(buffX4, 0, 4) == -1
            || buffX4[0] != 'P'
            || buffX4[1] != 'D'
            || buffX4[2] != 'N'
            || buffX4[3] != '3')
                return (false, Resources.fallback);

            if (reader.Read(buffX4, 0, 3) == -1)
                return (false, Resources.fallback);

            // "<pdnImage><custom></custom></pdnImage>"
            int headerSz = buffX4[0] + (buffX4[1] << 8) + (buffX4[2] << 16);
            if (headerSz < 38)
                return (false, Resources.fallback);

            using (StreamSubsetWrapper headerReader = new StreamSubsetWrapper(reader, headerSz))
            {
                XmlDocument headerXml = new XmlDocument();
                headerXml.Load(headerReader);
                string png = headerXml.SelectSingleNode("/pdnImage/custom/thumb")?.Attributes["png"]?.Value;
                headerXml = null;
                byte[] pngData = Convert.FromBase64String(png);
                png = null;
                Bitmap thumb;
                using (MemoryStream pngStream = new MemoryStream(pngData))
                {
                    thumb = (Bitmap) Image.FromStream(pngStream);
                }
                pngData = null;
                return (true, thumb);
            }
#if !DEBUG
            }
            catch (Exception)
            {
                return (false, Resources.fallback);
            }
#endif
        }
    }
}
