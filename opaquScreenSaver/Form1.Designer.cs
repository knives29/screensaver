namespace opaquScreenSaver
{
    partial class ScreenSaver
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenSaver));
            this.canv = new System.Windows.Forms.Panel();
            this.timerdraw = new System.Windows.Forms.Timer(this.components);
            this.timercheck = new System.Windows.Forms.Timer(this.components);
            this.timercheckSub = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuroot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu0 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu15 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu30 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu60 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu120 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuroot.SuspendLayout();
            this.SuspendLayout();
            // 
            // canv
            // 
            this.canv.Location = new System.Drawing.Point(80, 55);
            this.canv.Name = "canv";
            this.canv.Size = new System.Drawing.Size(96, 79);
            this.canv.TabIndex = 0;
            this.canv.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScreenSaver_MouseMove);
            // 
            // timerdraw
            // 
            this.timerdraw.Interval = 16;
            this.timerdraw.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timercheck
            // 
            this.timercheck.Interval = 1000;
            this.timercheck.Tick += new System.EventHandler(this.timercheck_tick);
            // 
            // timercheckSub
            // 
            this.timercheckSub.Enabled = true;
            this.timercheckSub.Interval = 1000;
            this.timercheckSub.Tick += new System.EventHandler(this.timercheckSub_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.menuroot;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // menuroot
            // 
            this.menuroot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu0,
            this.menu1,
            this.menu15,
            this.menu30,
            this.menu60,
            this.menu120,
            this.menuExit});
            this.menuroot.Name = "contextMenuStrip1";
            this.menuroot.ShowItemToolTips = false;
            this.menuroot.Size = new System.Drawing.Size(181, 180);
            // 
            // menu0
            // 
            this.menu0.CheckOnClick = true;
            this.menu0.Name = "menu0";
            this.menu0.Size = new System.Drawing.Size(180, 22);
            this.menu0.Text = "0";
            this.menu0.CheckedChanged += new System.EventHandler(this.menu0_CheckedChanged);
            // 
            // menu1
            // 
            this.menu1.Checked = true;
            this.menu1.CheckOnClick = true;
            this.menu1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(180, 22);
            this.menu1.Text = "1";
            this.menu1.Click += new System.EventHandler(this.menuX_CheckedChanged);
            // 
            // menu15
            // 
            this.menu15.CheckOnClick = true;
            this.menu15.Name = "menu15";
            this.menu15.Size = new System.Drawing.Size(180, 22);
            this.menu15.Text = "15";
            this.menu15.Click += new System.EventHandler(this.menuX_CheckedChanged);
            // 
            // menu30
            // 
            this.menu30.CheckOnClick = true;
            this.menu30.Name = "menu30";
            this.menu30.Size = new System.Drawing.Size(180, 22);
            this.menu30.Text = "30";
            this.menu30.Click += new System.EventHandler(this.menuX_CheckedChanged);
            // 
            // menu60
            // 
            this.menu60.CheckOnClick = true;
            this.menu60.Name = "menu60";
            this.menu60.Size = new System.Drawing.Size(180, 22);
            this.menu60.Text = "60";
            this.menu60.Click += new System.EventHandler(this.menuX_CheckedChanged);
            // 
            // menu120
            // 
            this.menu120.CheckOnClick = true;
            this.menu120.Name = "menu120";
            this.menu120.Size = new System.Drawing.Size(180, 22);
            this.menu120.Text = "120";
            this.menu120.Click += new System.EventHandler(this.menuX_CheckedChanged);
            // 
            // menuExit
            // 
            this.menuExit.CheckOnClick = true;
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(180, 22);
            this.menuExit.Text = "Exit";
            this.menuExit.CheckStateChanged += new System.EventHandler(this.menuExit_CheckStateChanged);
            // 
            // ScreenSaver
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::opaquScreenSaver.Properties.Resources.splash;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ScreenSaver";
            this.Opacity = 0.5D;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.ActiveBorder;
            this.UseWaitCursor = true;
            this.BackgroundImageChanged += new System.EventHandler(this.ScreenSaver_BackgroundImageChanged);
            this.VisibleChanged += new System.EventHandler(this.ScreenSaver_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScreenSaver_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ScreenSaver_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ScreenSaver_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ScreenSaver_KeyDown);
            this.Resize += new System.EventHandler(this.ScreenSaver_VisibleChanged);
            this.menuroot.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel canv;
        private System.Windows.Forms.Timer timerdraw;
        private System.Windows.Forms.Timer timercheck;
        private System.Windows.Forms.Timer timercheckSub;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip menuroot;
        private System.Windows.Forms.ToolStripMenuItem menu1;
        private System.Windows.Forms.ToolStripMenuItem menu15;
        private System.Windows.Forms.ToolStripMenuItem menu30;
        private System.Windows.Forms.ToolStripMenuItem menu60;
        private System.Windows.Forms.ToolStripMenuItem menu120;
        private System.Windows.Forms.ToolStripMenuItem menu0;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
    }
}

