using System;
using System.Net.Sockets;

namespace Library.Eventing.Sockets.Client
{
    public class ReceiveAsyncSocket : IReceiveAsyncSocket
    {
        public event EventHandler<IEventMessage> MessageHandler;

        private readonly Socket _socket;
        private const int BufferSize = 256;
        private readonly byte[] _buffer = new byte[BufferSize];
        private readonly EventMessageByteCollection _accumulator = new EventMessageByteCollection();

        public ReceiveAsyncSocket(Socket socket) => _socket = socket;

        public void Receive() => _socket.BeginReceive(_buffer, 0, BufferSize, SocketFlags.None, Callback, null);

        private void Callback(IAsyncResult ar)
        {
            try
            {
                ReadBuffer(ar);

                _socket.BeginReceive(_buffer, 0, BufferSize, SocketFlags.None, Callback, null);

            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReadBuffer(IAsyncResult ar)
        {
            int byteRead = _socket.EndReceive(ar);
            for (int index = 0; index < byteRead; index++)
            {
                //If we hit the terminator
                //AND the read bytes
                //Currently no good object - I expect it'll simplify when sockets are micro'd
                if (MessageContinues(_buffer[index])) continue;

                OnMessageHandler(_accumulator.EventMessage());

                _accumulator.Clear();
            }
        }

        private bool MessageContinues(byte b)
        {
            if (b == '\0') return false;

            _accumulator.Add(b);
            return true;
        }

        private void OnMessageHandler(IEventMessage eventMessage) => MessageHandler?.Invoke(this, eventMessage);
    }

    public interface IReceiveAsyncSocket
    {
        void Receive();
        event EventHandler<IEventMessage> MessageHandler;
    }
}