#define ShowConsole
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
            GZConsole.WriteLine(GZConsole.MsgType.text, "Debug模式下控制台已经开启，此控制台直接运行exe不会出现,正式使用请注释掉Program.cs中的第一行预定义变量");
            GZConsole.WriteLine(GZConsole.MsgType.text, "使用GZConsole.WriteLine方法可输出内容到此控制台");

            GZConsole.WriteLine(GZConsole.MsgType.text, "这是一条普通消息，类型为：GZConsole.MsgType.text");
            GZConsole.WriteLine(GZConsole.MsgType.info, "这是一条注意消息，类型为：GZConsole.MsgType.info");
            GZConsole.WriteLine(GZConsole.MsgType.error, "这是一条错误消息，类型为：GZConsole.MsgType.error");
            GZConsole.WriteLine(GZConsole.MsgType.warring, "这是一条警告消息，类型为：GZConsole.MsgType.warring");

            GZConsole.WriteLine(GZConsole.MsgType.text, "下面开始正式输出内容");
            GZConsole.WriteLine(GZConsole.MsgType.text, "====================================");
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
#if DEBUG && ShowConsole
            GZConsole.WriteLine(GZConsole.MsgType.info, "2秒后关闭...");
            Thread.Sleep(2000);
            GZConsole.CloseConsole();
#endif
        }


    }


}
