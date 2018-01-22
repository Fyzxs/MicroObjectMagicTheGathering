using Library.Booleans;
using System;
using System.ComponentModel;

namespace Library.Threading
{
    public class BackgroundWorkerBookEnd : IBackgroundWorkerBookEnd
    {
        private readonly BackgroundWorker _backgroundWorker;

        public BackgroundWorkerBookEnd() : this(BackgroundWorkerCancelable.NotCancelable) { }

        public BackgroundWorkerBookEnd(IBackgroundWorkerCancelable cancelable) : this(new BackgroundWorker(), cancelable)
        { }

        public BackgroundWorkerBookEnd(BackgroundWorker backgroundWorker, IBackgroundWorkerCancelable cancelable)
        {
            _backgroundWorker = backgroundWorker;
            _backgroundWorker.WorkerSupportsCancellation = cancelable.SupportsCancellation();
        }

        public void RunWorker(DoWorkEventHandler args)
        {
            _backgroundWorker.DoWork += args;
            _backgroundWorker.RunWorkerAsync();
        }

        public void Cancel() => _backgroundWorker.CancelAsync();

        public bool NotCancelled() => _backgroundWorker.CancellationPending.Not();

        public void Dispose() => _backgroundWorker?.Dispose();
    }
    public interface IBackgroundWorkerBookEnd : IDisposable
    {
        void RunWorker(DoWorkEventHandler args);
        void Cancel();
        bool NotCancelled();
    }
}