using System.Threading.Tasks;

namespace Library.Eventing
{
    public interface IListener
    {
        Task Update(IEventMessage eventMessage);
        void Attached();
    }
}