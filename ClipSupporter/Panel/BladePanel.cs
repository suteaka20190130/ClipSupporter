using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipSupporter
{
    public partial class BladePanel : UserControl
    {
        public object MainInstance { get; set; }

        public BladePanel()
        {
            InitializeComponent();

#if DEBUG
            TitleLabel.Click += new EventHandler(TitleLabel_Click);
#endif
        }

        private void TitleLabel_Click(object sender, EventArgs e)
        {

        }

        private void BladePanel_Load(object sender, EventArgs e)
        {

        }
    }
}
