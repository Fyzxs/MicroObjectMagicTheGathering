using System.Collections.Generic;
using Library.Bytes;

namespace Library.Eventing
{

    public class EventMessageByteCollection
    {
        private readonly List<byte> _bytes = new List<byte>();
        public void Add(IEnumerable<byte> bytes) => _bytes.AddRange(bytes);
        public void Add(byte aByte) => _bytes.Add(aByte);
        public IEventMessage EventMessage() => new NullTerminatedBytesEventMessage(_bytes);
        public void Clear() => _bytes.Clear();
    }
}