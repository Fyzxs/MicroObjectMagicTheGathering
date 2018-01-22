using System.Threading.Tasks;

namespace Library.Eventing
{

    public interface IEventBus
    {
        void Attach(IListener listener);
        void Notify(IEventMessage eventMessage);
    }
}