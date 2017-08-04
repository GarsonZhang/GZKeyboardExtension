//#define ShowConsole
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace GZKeyboardExtension
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG && ShowConsole
            GZConsole.OpenConsole();
            
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;
            Application.Run(new frmMain());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            GZConsole.CloseConsole();
        }
    }


}
