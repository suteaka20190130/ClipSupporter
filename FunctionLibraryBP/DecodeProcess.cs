using System;
using System.Collections.Generic;
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
    public class DecodeProccess : BaseFunction
    {

        public DecodeProccess(string panelName)
            : base(nameof(DecodeProccess), panelName)
        {

        }

        public void HtmlDecode(object sender, EventArgs e)
        {
            string inputData = Clipboard.GetText();

            Clipboard.Clear();
            Clipboard.SetText(WebUtility.UrlDecode(inputData));
        }

        public void ReplaceText(object sender, EventArgs e)
        {
            if (!GetTags(sender, out List<string> tags))
            {
                // Tags定義なし
                return;
            }

            string inputData = Clipboard.GetText();
            if (String.IsNullOrEmpty(inputData))
            {
                return;
            }
            
            Clipboard.Clear();
            Clipboard.SetText(inputData.Replace(tags[0], tags[1]));
        }

        public void CopyText(object sender, EventArgs e)
        {
            if (!GetTags(sender, out List<string> tags))
            {
                // Tags定義なし
                return;
            }


            Clipboard.Clear();
            Clipboard.SetText(tags[0]);
        }

        public override void ShowDegugInfo(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
