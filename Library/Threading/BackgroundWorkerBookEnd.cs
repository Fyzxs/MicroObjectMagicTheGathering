using System.ComponentModel;

namespace Library.Threading {
    public class BackgroundWorkerBookEnd : IBackgroundWorkerBookEnd
    {
        private readonly BackgroundWorker _backgroundWorker;

        public BackgroundWorkerBookEnd() : this(new BackgroundWorker()) { }

        public BackgroundWorkerBookEnd(BackgroundWorker backgroundWorker) => _backgroundWorker = backgroundWorker;

        public void RunWorker(DoWorkEventHandler args)
        {
            _backgroundWorker.DoWork += args;
            _backgroundWorker.RunWorkerAsync();
        }
    }
}