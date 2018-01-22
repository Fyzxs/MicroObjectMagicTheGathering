using System;
using System.Net.Sockets;

namespace Library.Eventing.Sockets
{
    public class SendToAsyncSocket : IListener
    {
        private readonly Socket _socket;

        public SendToAsyncSocket(Socket socket) => _socket = socket;

        private void Callback(IAsyncResult ar)
        {
            try
            {
                // Complete sending the data to the remote device.  
                int bytesSent = _socket.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to connection.", bytesSent);
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Notify(IEventMessage eventMessage)
        {
            byte[] msgBytes = eventMessage.Bytes();
            _socket.BeginSend(msgBytes, 0, msgBytes.Length, SocketFlags.None, Callback, _socket);
        }
    }

}