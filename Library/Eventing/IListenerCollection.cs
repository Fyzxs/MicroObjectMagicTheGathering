using System.Threading.Tasks;

namespace Library.Eventing {
    public interface IListenerCollection
    {
        void Add(IListener listener);
        Task<Task[]> NotifyAll(IEventMessage eventMessage);
    }
}