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

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace PDNThumb.IO
{
    [ComVisible(false)]
    internal sealed class StreamSubsetWrapper : Stream
    {
        readonly Stream innerStream;
        readonly long endPos;

        public StreamSubsetWrapper(Stream innerStream, int size)
        {
            if (innerStream == null)
                throw new ArgumentNullException("innerStream");
            if (size < 0)
                throw new ArgumentOutOfRangeException("size");

            this.innerStream = innerStream;
            endPos = this.innerStream.Position + size;
        }

        public override bool CanRead => innerStream.CanRead;

        public override bool CanSeek => innerStream.CanSeek;

        public override bool CanWrite => false;

        public override void Flush() => innerStream.Flush();

        public override long Length => endPos;

        public override long Position
        {
            get => innerStream.Position;
            set => innerStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
            => innerStream.Read(buffer, offset, GetAllowedCount(count));

        public override long Seek(long offset, SeekOrigin origin) => innerStream.Seek(offset, origin);

        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        public override bool CanTimeout => innerStream.CanTimeout;

        public override int ReadTimeout
        {
            get => innerStream.ReadTimeout;
            set => innerStream.ReadTimeout = value;
        }

        public override int WriteTimeout
        {
            get => innerStream.ReadTimeout;
            set => innerStream.ReadTimeout = value;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback,
            object state) => innerStream.BeginRead(buffer, offset, GetAllowedCount(count), callback, state);

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback,
            object state) => throw new NotSupportedException();

        public override void Close()
        {
            // This wrapper should not close the underlying stream as it does not own it
        }

        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
            => innerStream.CopyToAsync(destination, bufferSize, cancellationToken);

        public override int EndRead(IAsyncResult asyncResult) => innerStream.EndRead(asyncResult);

        public override Task FlushAsync(CancellationToken cancellationToken)
            => innerStream.FlushAsync(cancellationToken);

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            => innerStream.ReadAsync(buffer, offset, GetAllowedCount(count), cancellationToken);

        public override int ReadByte() => GetAllowedCount(1) != 0 ? innerStream.ReadByte() : -1;

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            => throw new NotSupportedException();

        public override void WriteByte(byte value) => throw new NotSupportedException();

        private int GetAllowedCount(int count)
        {
            long maxCount = endPos - innerStream.Position;
            return (int) ((count > maxCount) ? maxCount : count);
        }
    }
}
