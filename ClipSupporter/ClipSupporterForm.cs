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

namespace ClipSupporter
{
    public partial class ClipSupporterForm : Form
    {
        public bool DisplayTopMost { get; set; }
        public string DesignColor { get; set; }

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
                    this.BackColor = Color.Blue;
                    BlueToolStripMenuItem.Checked = true;
                    break;
                case "White":
                    this.BackColor = Color.White;
                    WhiteToolStripMenuItem.Checked = true;
                    break;
                case "Green":
                    this.BackColor = Color.Green;
                    GreenToolStripMenuItem.Checked = true;
                    break;
                case "Red":
                    this.BackColor = Color.Red;
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
            int cntCount = tabControl1.TabPages[1].Controls.Count;
            for (int i = cntCount-1; i >= 0; i--)
            {
                if (tabControl1.TabPages[1].Controls[i].Name == "PanelArea")
                {
                    continue;
                }
                tabControl1.TabPages[1].Controls.RemoveAt(i);
            }

            for (int i = 0; i < 3; i++)
            {
                ButtonPanel pnl = new ButtonPanel();
                pnl.TitleLabel.Text = $"機能{i}";
                pnl.Location = new Point(0, i * 50);
                PanelArea.Controls.Add(pnl);
                //tabControl1.TabPages[1].Controls.Add(pnl);
            }

            tabControl1.SelectedIndex = 1;
        }
    }
}
