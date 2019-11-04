using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionLibraryBP
{
    public abstract class BaseFunctionClass
    {
        public string BasePath { get; set; }

        public abstract void ShowDegugInfo(object sender, EventArgs e);
    }
}
