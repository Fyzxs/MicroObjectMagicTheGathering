namespace Library.Threading
{
    public class BackgroundWorkerCancelable : IBackgroundWorkerCancelable
    {
        public static IBackgroundWorkerCancelable Cancelable = new BackgroundWorkerCancelable();
        public static IBackgroundWorkerCancelable NotCancelable = new BackgroundWorkerCancelable();

        private BackgroundWorkerCancelable() { }
        public bool SupportsCancellation() => this == Cancelable;
    }

    public interface IBackgroundWorkerCancelable
    {
        bool SupportsCancellation();
    }
}