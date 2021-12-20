using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;

namespace FunctionLibraryBP
{
    public abstract class BaseFunction
    {
        public string TemporaryPath { get; set; }
        public string ConfigPath { get; set; }

        public Dictionary<string, List<string>> Tags { get; set; }

        public abstract void ShowDegugInfo(object sender, EventArgs e);

        public BaseFunction(string instanceClassName, string panelName)
        {
            TemporaryPath = Path.Combine(ShareCompornent.TemplateBasePath, panelName);
            ConfigPath = Path.Combine(ShareCompornent.ConfigBasePath, instanceClassName);
            Tags = new Dictionary<string, List<string>>();
        }
        public bool GetTags(object sender, out List<string>tags)
        {
            tags = new List<string>();

            bool isRet = false;
            var ctrl = sender as System.Windows.Forms.Control;
            //int index = GetControlIndex(sender);
            //if (index < 0)
            //{
            //    return isRet;
            //}

            //if (Tags.ContainsKey(ctrl.Name))
            //{
            //    isRet = true;
            //    tags.AddRange(Tags[ctrl.Name]);
            //}

            if (ctrl.Tag != null)
            {
                tags = ctrl.Tag as List<string>;
                isRet = true;
            }

            return isRet;

        }

        private int GetControlIndex(object sender)
        {
            int retIndex = -1;
            var ctrl = sender as System.Windows.Forms.Control;
            if (ctrl != null)
            {
                string tag = ctrl.Tag.ToString();
                int.TryParse(tag, out retIndex);
            }
            return retIndex;
        }
    }
}
