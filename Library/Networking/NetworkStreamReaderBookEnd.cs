using Library.Bytes;
using Library.Threading;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Library.Networking
{
    public class NetworkStreamReaderBookEnd : IBytesReader
    {
        private readonly NetworkStream _stream;
        private const byte Terminator = (byte) '\0';
        private SemaphoreSlimBookEnd _ssbe = new SemaphoreSlimBookEnd();

        public NetworkStreamReaderBookEnd(NetworkStream stream) => _stream = stream;

        public IBytes ReadToEnd()
        {
            lock (_stream)
            {

                List<byte> fullBytes = new List<byte>();
                int byteValue = -1;
                while (( byteValue = _stream.ReadByte() ) != Terminator)
                {
                    fullBytes.Add((byte) byteValue);
                }

                return new BytesOf(fullBytes.ToArray());
            }

        }
    }
}