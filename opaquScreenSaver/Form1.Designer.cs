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
            this.canv = new System.Windows.Forms.Panel();
            this.timerdraw = new System.Windows.Forms.Timer(this.components);
            this.timercheck = new System.Windows.Forms.Timer(this.components);
            this.timercheckSub = new System.Windows.Forms.Timer(this.components);
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
            // ScreenSaver
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::opaquScreenSaver.Properties.Resources.splash;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ScreenSaver";
            this.Opacity = 0.25D;
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
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel canv;
        private System.Windows.Forms.Timer timerdraw;
        private System.Windows.Forms.Timer timercheck;
        private System.Windows.Forms.Timer timercheckSub;
    }
}

