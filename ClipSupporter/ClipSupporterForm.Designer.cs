namespace ClipSupporter
{
    partial class ClipSupporterForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClipSupporterForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageKC = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPageCalc = new System.Windows.Forms.TabPage();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topMostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.背景色変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BlueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WhiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.samplePanel2 = new ClipSupporter.Panel.DesignSample.SamplePanel();
            this.PanelArea = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPageKC.SuspendLayout();
            this.tabPageCalc.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "a";
            this.notifyIcon1.BalloonTipTitle = "b";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageKC);
            this.tabControl1.Controls.Add(this.tabPageCalc);
            this.tabControl1.Location = new System.Drawing.Point(5, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(219, 333);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageKC
            // 
            this.tabPageKC.Controls.Add(this.button1);
            this.tabPageKC.Location = new System.Drawing.Point(4, 22);
            this.tabPageKC.Name = "tabPageKC";
            this.tabPageKC.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKC.Size = new System.Drawing.Size(211, 307);
            this.tabPageKC.TabIndex = 0;
            this.tabPageKC.Text = "業務";
            this.tabPageKC.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // tabPageCalc
            // 
            this.tabPageCalc.Controls.Add(this.samplePanel2);
            this.tabPageCalc.Controls.Add(this.PanelArea);
            this.tabPageCalc.Location = new System.Drawing.Point(4, 22);
            this.tabPageCalc.Name = "tabPageCalc";
            this.tabPageCalc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCalc.Size = new System.Drawing.Size(211, 307);
            this.tabPageCalc.TabIndex = 1;
            this.tabPageCalc.Text = "Calc";
            this.tabPageCalc.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(231, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.topMostToolStripMenuItem,
            this.背景色変更ToolStripMenuItem,
            this.終了ToolStripMenuItem});
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.設定ToolStripMenuItem.Text = "設定";
            // 
            // topMostToolStripMenuItem
            // 
            this.topMostToolStripMenuItem.Name = "topMostToolStripMenuItem";
            this.topMostToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.topMostToolStripMenuItem.Text = "常に前に表示";
            this.topMostToolStripMenuItem.Click += new System.EventHandler(this.TopMostToolStripMenuItem_Click);
            // 
            // 背景色変更ToolStripMenuItem
            // 
            this.背景色変更ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GrayToolStripMenuItem,
            this.BlueToolStripMenuItem,
            this.WhiteToolStripMenuItem,
            this.RedToolStripMenuItem,
            this.GreenToolStripMenuItem});
            this.背景色変更ToolStripMenuItem.Name = "背景色変更ToolStripMenuItem";
            this.背景色変更ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.背景色変更ToolStripMenuItem.Text = "背景色変更";
            // 
            // GrayToolStripMenuItem
            // 
            this.GrayToolStripMenuItem.AccessibleName = "Gray";
            this.GrayToolStripMenuItem.Name = "GrayToolStripMenuItem";
            this.GrayToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.GrayToolStripMenuItem.Text = "グレー";
            this.GrayToolStripMenuItem.Click += new System.EventHandler(this.ColorToolStripMenuItem_Click);
            // 
            // BlueToolStripMenuItem
            // 
            this.BlueToolStripMenuItem.AccessibleName = "Blue";
            this.BlueToolStripMenuItem.Name = "BlueToolStripMenuItem";
            this.BlueToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.BlueToolStripMenuItem.Text = "ブルー";
            this.BlueToolStripMenuItem.Click += new System.EventHandler(this.ColorToolStripMenuItem_Click);
            // 
            // WhiteToolStripMenuItem
            // 
            this.WhiteToolStripMenuItem.AccessibleName = "White";
            this.WhiteToolStripMenuItem.Name = "WhiteToolStripMenuItem";
            this.WhiteToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.WhiteToolStripMenuItem.Text = "ホワイト";
            this.WhiteToolStripMenuItem.Click += new System.EventHandler(this.ColorToolStripMenuItem_Click);
            // 
            // RedToolStripMenuItem
            // 
            this.RedToolStripMenuItem.AccessibleName = "Red";
            this.RedToolStripMenuItem.Name = "RedToolStripMenuItem";
            this.RedToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.RedToolStripMenuItem.Text = "レッド";
            this.RedToolStripMenuItem.Click += new System.EventHandler(this.ColorToolStripMenuItem_Click);
            // 
            // GreenToolStripMenuItem
            // 
            this.GreenToolStripMenuItem.AccessibleName = "Green";
            this.GreenToolStripMenuItem.Name = "GreenToolStripMenuItem";
            this.GreenToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.GreenToolStripMenuItem.Text = "グリーン";
            this.GreenToolStripMenuItem.Click += new System.EventHandler(this.ColorToolStripMenuItem_Click);
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.終了ToolStripMenuItem.Text = "終了";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // samplePanel2
            // 
            this.samplePanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.samplePanel2.Location = new System.Drawing.Point(6, 6);
            this.samplePanel2.Name = "samplePanel2";
            this.samplePanel2.Size = new System.Drawing.Size(200, 50);
            this.samplePanel2.TabIndex = 1;
            // 
            // PanelArea
            // 
            this.PanelArea.Location = new System.Drawing.Point(5, 5);
            this.PanelArea.Name = "PanelArea";
            this.PanelArea.Size = new System.Drawing.Size(200, 300);
            this.PanelArea.TabIndex = 2;
            // 
            // ClipSupporterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(231, 371);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ClipSupporterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ClipSupporter";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ClipSupporterForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageKC.ResumeLayout(false);
            this.tabPageCalc.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageKC;
        private System.Windows.Forms.TabPage tabPageCalc;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topMostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 背景色変更ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BlueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WhiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private Panel.DesignSample.SamplePanel samplePanel2;
        private System.Windows.Forms.Panel PanelArea;
    }
}

