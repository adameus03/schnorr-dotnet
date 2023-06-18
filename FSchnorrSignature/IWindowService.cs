/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSchnorrSignature
{
    internal interface IWindowService
    {
    }
}*/


using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSchnorrSignature
{
    interface IWindowService
    {
        public void ShowWindow<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel;
        public void CloseWindow<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel;

        public void CloseMainWindow();

        public string? ShowOpenFileDialog();
        public string? ShowSaveFileDialog();

        public void ShowMessage(string message);

    }
}


