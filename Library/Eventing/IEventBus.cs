using System.Threading.Tasks;

namespace Library.Eventing {
    public interface IEventBus
    {
        void Attach(IListener listener);
        Task Notify(IEventMessage eventMessage);
        void Detach(IListener listener);
    }
}