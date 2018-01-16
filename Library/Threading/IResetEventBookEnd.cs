namespace Library.Threading {
    public interface IResetEventBookEnd
    {
        void Reset();
        void WaitOne();
        void Set();
    }
}