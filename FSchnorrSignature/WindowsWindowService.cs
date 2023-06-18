/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSchnorrSignature
{
    internal class WindowsWindowService
    {
    }
}*/

using FSchnorrSignature;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FSchnorrSignature
{
    class WindowsWindowService : IWindowService
    {
        private static Type GetViewType<TViewModel>() where TViewModel : BaseViewModel
        {

            return Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(
                t => t.GetCustomAttributes(typeof(VVMAttribute)).Any(
                    a => ((VVMAttribute)a).ViewModelType == typeof(TViewModel)
                )
            ) ?? typeof(Window);


        }

        private string? ShowFileDialog<TFileDialog>() where TFileDialog : FileDialog, new()
        {
            TFileDialog fileDialog = new TFileDialog();
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            bool? showDialogOutput = fileDialog.ShowDialog();
            if (showDialogOutput != null)
            {
                if ((bool)showDialogOutput)
                {
                    return fileDialog.FileName;
                }
            }
            return null;
        }

        public void ShowWindow<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            Type viewType = WindowsWindowService.GetViewType<TViewModel>();
            Window window = Activator.CreateInstance(viewType) as Window ?? new Window();
            window.DataContext = viewModel;
            window.Show();
        }

        public void CloseWindow<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            Window? window = Application.Current.Windows.OfType<Window>().SingleOrDefault((w => w.DataContext.Equals(viewModel)));
            window?.Close();
        }

        public void CloseMainWindow()
        {
            Application.Current.MainWindow.Close();
        }

        public string? ShowOpenFileDialog()
        {
            /*OpenFileDialog openFileDialog = new OpenFileDialog();
            bool? showDialogOutput = openFileDialog.ShowDialog();
            if (showDialogOutput != null)
            {
                if ((bool)showDialogOutput)
                {
                    return openFileDialog.FileName;
                }
            }
            return null;*/
            return this.ShowFileDialog<OpenFileDialog>();
        }

        public string? ShowSaveFileDialog()
        {
            return this.ShowFileDialog<SaveFileDialog>();
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

    }
}


