using Library.Bytes;
using System.Collections.Generic;
using System.Linq;

namespace Library.Eventing
{
    public class NullTerminatedBytesEventMessage : IEventMessage
    {
        private readonly IBytes _origin;

        public NullTerminatedBytesEventMessage(IBytes bytes) => _origin = bytes;

        public NullTerminatedBytesEventMessage(List<byte> bytes) : this(new BytesOf(bytes)) { }

        public byte[] Bytes()
        {
            byte[] msgBytes = _origin.Bytes();
            if (msgBytes.LastOrDefault() == '\0') return msgBytes;

            return new List<byte>(msgBytes) { (byte) '\0' }.ToArray();
        }
    }
}