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

        private void SaveProperty()
        {
            Properties.Settings.Default.Save();
        }

        private void SetDesignColor(string color)
        {
            switch (color)
            {
                case "Control":
                    this.BackColor = SystemColors.Control;
                    GrayToolStripMenuItem.Checked = true;
                    break;
                case "ActiveCaption":
                    this.BackColor = SystemColors.ActiveCaption;
                    BlueToolStripMenuItem.Checked = true;
                    break;
                case "White":
                    this.BackColor = Color.White;
                    WhiteToolStripMenuItem.Checked = true;
                    break;
                case "PaleGreen":
                    this.BackColor = Color.PaleGreen;
                    GreenToolStripMenuItem.Checked = true;
                    break;
                case "LightPink":
                    this.BackColor = Color.LightPink;
                    RedToolStripMenuItem.Checked = true;
                    break;
                default:
                    MessageBox.Show($"起動に失敗しました。DesignColor={DesignColor}");
                    break;
            }
        }

        private void TopMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = this.TopMost = DisplayTopMost = !DisplayTopMost;

            Properties.Settings.Default["TopMost"] = this.TopMost.ToString();
        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrayToolStripMenuItem.Checked = BlueToolStripMenuItem.Checked = WhiteToolStripMenuItem.Checked
                = GreenToolStripMenuItem.Checked = RedToolStripMenuItem.Checked = false;
            string controlName = ((ToolStripMenuItem)sender).AccessibleName;

            SetDesignColor(controlName);

            Properties.Settings.Default["DesignColor"] = this.BackColor.Name.ToString();

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

            var cfg = Config.PanelConfig.GetConfig();
            //foreach (var cfg in Config.PanelConfig.GetConfigs())
            //{
            //    if (cfg is Config.PanelConfig)
            //    {
            for (int i = 0; i < 1; i++)
            {
                int topPos = 3;
                ButtonPanel pnl = new ButtonPanel((Config.PanelConfig)cfg);
                pnl.Top = topPos;
                pnl.Left = 3;
                tabControl1.TabPages[0].Controls.Add(pnl);
                topPos += pnl.Height;
            }
            //    }
            //}

            tabControl1.SelectedIndex = 0;
        }
    }
}
