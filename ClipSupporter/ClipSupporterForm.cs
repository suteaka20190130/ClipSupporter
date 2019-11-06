using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClipSupporter.Panel;
using System.Reflection;

namespace ClipSupporter
{
    public partial class ClipSupporterForm : Form
    {
        public bool DisplayTopMost { get; set; }
        public string DesignColor { get; set; }
        public string ApplicationBasePath { get; set; }

        /// <summary>
        /// constructor
        /// ・Settingsの読込
        /// ・Configからパネルの設定
        /// </summary>
        public ClipSupporterForm()
        {
            InitializeComponent();

            // 設定の取り込み
            LoadProperty();

            //ClipDataFolderPath = Path.Combine(ConfigurationManager.AppSettings["BasePath"]
            //    , ConfigurationManager.AppSettings["AppName"]);

            //Directory.CreateDirectory(ClipDataFolderPath);
            this.ApplicationBasePath = ConfigurationManager.AppSettings["ApplicationBasePath"];
            this.Text = ConfigurationManager.AppSettings["ApplicationBaseTitle"];

            var a = Config.PanelConfig.GetConfig();
        }

        private void LoadProperty()
        {
            // DisplayTopMost
            DisplayTopMost = (bool)Properties.Settings.Default["TopMost"];
            this.TopMost = DisplayTopMost;
            topMostToolStripMenuItem.Checked = DisplayTopMost;

            // DesignColor
            DesignColor = (string)Properties.Settings.Default["DesignColor"];
            SetDesignColor(DesignColor);

        }

        private void SetDesignColor(string color)
        {
            switch (color)
            {
                case "Gray":
                    this.BackColor = SystemColors.Control;
                    GrayToolStripMenuItem.Checked = true;
                    break;
                case "Blue":
                    this.BackColor = SystemColors.ActiveCaption;
                    BlueToolStripMenuItem.Checked = true;
                    break;
                case "White":
                    this.BackColor = Color.White;
                    WhiteToolStripMenuItem.Checked = true;
                    break;
                case "Green":
                    this.BackColor = Color.PaleGreen;
                    GreenToolStripMenuItem.Checked = true;
                    break;
                case "Red":
                    this.BackColor = Color.LightPink;
                    RedToolStripMenuItem.Checked = true;
                    break;
                default:
                    MessageBox.Show($"起動に失敗しました。DesignColor={DesignColor}");
                    break;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void TopMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = this.TopMost = DisplayTopMost = !DisplayTopMost;
        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrayToolStripMenuItem.Checked = BlueToolStripMenuItem.Checked = WhiteToolStripMenuItem.Checked
                = GreenToolStripMenuItem.Checked = RedToolStripMenuItem.Checked = false;
            string controlName = ((ToolStripMenuItem)sender).AccessibleName;

            SetDesignColor(controlName);
        }

        private void ClipSupporterForm_Load(object sender, EventArgs e)
        {
            // SamplePanelの削除
            // Tabコントロールの生成
            for (int tp = tabControl1.TabPages.Count -1; tp >= 0; tp--)
            {
                int cntCount = tabControl1.TabPages[tp].Controls.Count;
                for (int i = cntCount - 1; i >= 0; i--)
                {
                    tabControl1.TabPages[tp].Controls.RemoveAt(i);
                }
            }

            int nowYPos = 3;
            for (int i = 0; i < 1; i++)
            {
                var cfg = new ButtonPanelConfig();
                cfg.TitleName = $"Insert文生成";
                cfg.PanelBasePath = Path.Combine(ApplicationBasePath, $"Panel{i}");
                cfg.ControlCntX = new int[] { 1, 1, 8, 3 }[i];
                cfg.ControlCntY = new int[] { 1, 1, 1, 3 }[i];

                cfg.InstanceName = new string[]
                {
                    "FunctionLibraryBP.CreateInsertSQL"
                  , "FunctionLibraryBP.ClipSharing"
                }[i];

                //cfg.Controls.AddRange(
                //    new ButtonPanelConfig.PanelControl()
                //    {
                        
                //    }
                //    );

                ButtonPanel pnl = new ButtonPanel(cfg);
                pnl.Location = new Point(5, nowYPos);
                nowYPos += cfg.ControlCntY * 50;

                tabControl1.TabPages[0].Controls.Add(pnl);
            }

            tabControl1.SelectedIndex = 0;
        }
    }
}
