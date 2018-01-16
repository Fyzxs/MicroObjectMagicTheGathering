using Library.Bytes;

namespace Library.Eventing
{
    public interface IEventMessage
    {
        IBytes Bytes();
    }
}