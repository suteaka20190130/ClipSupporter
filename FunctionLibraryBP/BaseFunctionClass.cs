using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;

namespace FunctionLibraryBP
{
    public abstract class BaseFunctionClass
    {
        public string BasePath { get; set; }

        public abstract void ShowDegugInfo(object sender, EventArgs e);

        public BaseFunctionClass(string panelName)
        {
            BasePath = Path.Combine(ShareCompornent.ApplicationBasePath, panelName);
        }
    }
}
