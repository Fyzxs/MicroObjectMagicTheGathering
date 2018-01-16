using Library.Bytes;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Library.Networking
{
    public class NetworkStreamWriterBookEnd : IBytesWriter
    {
        private readonly NetworkStream _origin;
        private const byte Terminator = (byte) '\0';

        public NetworkStreamWriterBookEnd(NetworkStream origin) => _origin = origin;
        public async Task Write(IBytes bytes)
        {
            byte[] buffer = bytes.Bytes();
            _origin.Write(buffer, 0, buffer.Length);
            _origin.Write(new[] { Terminator }, 0, 1);
            await _origin.FlushAsync();
        }
    }
}