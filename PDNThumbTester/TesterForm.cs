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

using PDNThumbTester.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDNThumbTester
{
    public partial class TesterForm : Form
    {
        private static readonly byte[] Resources_test = Resources.test;

        public TesterForm()
        {
            InitializeComponent();
        }

        private Task Result(IAsyncResult result) => Task.Factory.FromAsync(result, _ => { });

        private MethodInvoker Invoker(MethodInvoker expr) => expr;

        private async Task LoadThumb(int width)
        {
            (bool success, Image previewBmp) = await Task.Run(() =>
            {
                using (MemoryStream testStream = new MemoryStream(Resources_test))
                {
                    return (width == 0)
                        ? PDNThumb.ThumbnailHandler.ReadPDNThumb(testStream)
                        : PDNThumb.ThumbnailHandler.ReadPDNThumbRes(testStream, width);
                }
            });
            await Result(preview.BeginInvoke(Invoker(() => preview.Image = previewBmp)));
            await Result(loadStatus.BeginInvoke(Invoker(() => loadStatus.Text = success ? "Success" : "Failure")));
            await Result(loadButton.BeginInvoke(Invoker(() => loadButton.Enabled = true)));
        }

#pragma warning disable IDE1006 // Naming Styles
        private void loadButton_Click(object sender, EventArgs e)
        {
            loadButton.Enabled = false;
            loadStatus.Text = "Loading . . .";
            preview.Image?.Dispose();
            preview.Image = null;
            _ = LoadThumb(loadWidthInput.Value);
        }

        private void loadWidthInput_ValueChanged(object sender, EventArgs e)
        {
            loadWidthValueLabel.Text = loadWidthInput.Value.ToString();
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
