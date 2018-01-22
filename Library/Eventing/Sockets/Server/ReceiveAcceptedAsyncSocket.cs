using System;
using System.Net.Sockets;

namespace Library.Eventing.Sockets.Server
{
    public class ReceiveAcceptedAsyncSocket : IReceiveAcceptedAsyncSocket
    {
        private const int BufferSize = 256;
        private readonly byte[] _buffer = new byte[BufferSize];
        private readonly EventMessageByteCollection _accumulator = new EventMessageByteCollection();
        public void Receive(Socket client) => client.BeginReceive(_buffer, 0, BufferSize, SocketFlags.None, Callback, client);
        private void Callback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket) ar.AsyncState;

                ReadBuffer(ar, client);

                client.BeginReceive(_buffer, 0, BufferSize, SocketFlags.None, Callback, client);
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReadBuffer(IAsyncResult ar, Socket client)
        {
            int byteRead = client.EndReceive(ar);
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

        public event EventHandler<IEventMessage> MessageHandler;
    }
    public interface IReceiveAcceptedAsyncSocket
    {
        void Receive(Socket client);
        event EventHandler<IEventMessage> MessageHandler;
    }
}