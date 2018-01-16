using System.ComponentModel;

namespace Library.Threading {
    public interface IBackgroundWorkerBookEnd
    {
        void RunWorker(DoWorkEventHandler args);
    }
}