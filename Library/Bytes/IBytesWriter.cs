using System.Threading.Tasks;

namespace Library.Bytes
{
    public interface IBytesWriter
    {
        Task Write(IBytes bytes);
    }
}