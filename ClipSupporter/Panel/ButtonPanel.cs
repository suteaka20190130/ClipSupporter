using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FunctionLibraryBP;

namespace ClipSupporter.Panel
{
    public partial class ButtonPanel : ClipSupporter.BladePanel
    {
        public ButtonPanel(ButtonPanelConfig cfg)
        {
            InitializeComponent();

            int areaLeft = ButtonArea.Left;
            int areaTop = ButtonArea.Top;
            int btnWidth = ButtonArea.Width / cfg.ButtonCntX;
            int btnHeight = ButtonArea.Height / cfg.ButtonCntY;

            this.Controls.Remove(ButtonArea);

            int btnNumber = 1;
            for (int y = 0, btnTop = areaTop
                ;y < cfg.ButtonCntY
                ;y++, btnTop += btnHeight)
            {
                for (int x = 0, btnLeft = areaLeft
                    ; x < cfg.ButtonCntX
                    ; x++, btnLeft += btnWidth)
                {
                    Button btn = new Button();
                    btn.Name = $"Button{btnNumber++}";
                    btn.Width = btnWidth;
                    btn.Height = btnHeight;
                    btn.Left = btnLeft;
                    btn.Top = btnTop;

                    btn.Text = (btnNumber - 1).ToString();
                    var tp = new ToolTip();
                    tp.SetToolTip(btn, $"aaaaaa{btnNumber - 1}");

                    this.MainInstance = new ClipSharing();
                    btn.Click += new EventHandler(((ClipSharing)MainInstance).SaveClipBoard);

                    this.Controls.Add(btn);
                }
            }


        }

        private void ButtonPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
