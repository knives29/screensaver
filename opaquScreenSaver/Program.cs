using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace opaquScreenSaver
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        internal static System.Windows.Forms.NotifyIcon icon;
        internal static System.Windows.Forms.ContextMenuStrip cms;
        private static void cmsEnableChange(object sender, EventArgs e)
        {
            int i = 0;
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //
            icon = new NotifyIcon();
            cms = new ContextMenuStrip();
            icon.ContextMenuStrip = cms;
            IntPtr dummy = cms.Handle;
            //
            int[] intervals = { 30, 60, 5 * 60, 15 * 60, 30 * 60, 60 * 60, 2 * 60 * 60 };
            foreach (int interval in intervals)
            {
                string contextText = interval.ToString();
                icon.ContextMenuStrip.Items.Add(contextText);
            }
            cms.EnabledChanged += cmsEnableChange;
            //
            Application.Run(new ScreenSaver());
        }
    }
}
