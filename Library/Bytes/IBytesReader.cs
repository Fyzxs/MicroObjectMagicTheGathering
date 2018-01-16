using System.Threading.Tasks;

namespace Library.Bytes
{
    public interface IBytesReader
    {
        IBytes ReadToEnd();
    }
}