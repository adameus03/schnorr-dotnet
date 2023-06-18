/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSchnorrSignature
{
    internal class VVMAttribute
    {
    }
}*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace FSchnorrSignature
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class VVMAttribute : Attribute
    {
        public Type ViewModelType { get; private set; }
        public Type ViewType { get; private set; }

        public VVMAttribute(Type viewModelType, [CallerMemberName] string viewTypeName = "Window")
        {
            this.ViewModelType = viewModelType;
            this.ViewType = Type.GetType(viewTypeName) ?? typeof(object);
        }
    }
}


