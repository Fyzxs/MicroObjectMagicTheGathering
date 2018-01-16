using Library.Bytes;
using Library.Networking;
using System.Net.Sockets;

namespace Library.Eventing.Tcp
{
    public class TcpClientBookEnd : ITcpClientBookEnd
    {
        private readonly TcpClient _client;

        public TcpClientBookEnd(TcpClient client) => _client = client;
        public IBytesWriter Writer() => new NetworkStreamWriterBookEnd(_client.GetStream());
        public IBytesReader Reader() => new NetworkStreamReaderBookEnd(_client.GetStream());
    }
}