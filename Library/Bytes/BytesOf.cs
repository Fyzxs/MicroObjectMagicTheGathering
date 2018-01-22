using System;
using System.Collections.Generic;

namespace Library.Bytes
{
    public class BytesOf : IBytes
    {
        private class DelayedBytes : IBytes
        {
            private readonly Func<byte[]> _func;

            public DelayedBytes(Func<byte[]> func) => _func = func;

            public byte[] Bytes() => _func();
        }

        private readonly IBytes _bytes;

        public BytesOf(byte[] bytes) : this(new DelayedBytes(() => bytes)) { }

        public BytesOf(List<byte> bytes) : this(new DelayedBytes(bytes.ToArray)) { }

        public BytesOf(IBytes bytes) => _bytes = bytes;

        public byte[] Bytes() => _bytes.Bytes();
    }
}