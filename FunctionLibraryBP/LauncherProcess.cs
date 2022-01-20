using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary;
using CommonLibrary.FileAccess;
using CommonLibrary.SubForms;

namespace FunctionLibraryBP
{
    public class LauncherProcess : BaseFunction
    {
        public LauncherProcess(string panelName)
            : base(nameof(LauncherProcess), panelName)
        {

        }

        public void Launch(object sender, EventArgs e)
        {
            if (!GetTags(sender, out List<string> tags))
            {
                // Tags定義なし
                return;
            }

            Process.Start(tags[0]);
        }

        public override void ShowDegugInfo(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
