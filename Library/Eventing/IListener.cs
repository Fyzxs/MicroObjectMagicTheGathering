namespace Library.Eventing
{
    public interface IListener
    {
        void Notify(IEventMessage eventMessage);
    }
}