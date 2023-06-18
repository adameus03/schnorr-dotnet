/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSchnorrSignature
{
    internal class Command
    {
    }
}*/

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeApproximation
{
    internal class Command
    {
    }
}*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FSchnorrSignature
{
    public class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public event EventHandler? ExecuteReceived;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            ExecuteReceived?.Invoke(this, new EventArgs());
        }
    }
}


