using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Core.Exceptions
{
    public class FIleLoadException : Exception
    {
        public FIleLoadException(string path): base($"Failed to load the file {path}")
        {
            
        }
    }
}
