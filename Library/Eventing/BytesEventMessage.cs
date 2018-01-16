using Library.Bytes;

namespace Library.Eventing {
    public class BytesEventMessage : IEventMessage
    {
        private readonly IBytes _bytes;

        public BytesEventMessage(IBytes bytes) => _bytes = bytes;
        //TODO: Encapsulation Violation
        public IBytes Bytes() => _bytes;
    }
}