using Library.Bytes;

namespace Library.Eventing.Tcp
{
    public interface ITcpClientBookEnd
    {
        IBytesWriter Writer();
        IBytesReader Reader();
    }
}