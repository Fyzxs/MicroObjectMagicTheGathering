namespace Library.Bytes
{
    public class BytesOf : IBytes
    {
        private readonly byte[] _bytes;

        public BytesOf(byte[] bytes) => _bytes = bytes;

        public byte[] Bytes() => _bytes;
    }
}