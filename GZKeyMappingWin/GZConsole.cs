using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GZKeyboardExtension
{
    internal static class GZConsole
    {
        public enum MsgType
        {
            warring,
            error,
            info,
            text
        }
        public static bool ConsoleShow = false;
        public static void OpenConsole()
        {
            if (ConsoleShow == false)
            {
                ConsoleShow = importHelper.AllocConsole();
                StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput(), System.Text.Encoding.Default);

                standardOutput.AutoFlush = true;
          
                Console.SetOut(standardOutput);
                Console.ForegroundColor = ConsoleColor.Blue;
                //Console.Clear();
                GZConsole.WriteLine(GZConsole.MsgType.text, "控制台已经开启");
                GZConsole.WriteLine(GZConsole.MsgType.text, "使用GZConsole.WriteLine方法可输出内容到此控制台");

                GZConsole.WriteLine(GZConsole.MsgType.text, "这是一条普通消息，类型为：GZConsole.MsgType.text");
                GZConsole.WriteLine(GZConsole.MsgType.info, "这是一条注意消息，类型为：GZConsole.MsgType.info");
                GZConsole.WriteLine(GZConsole.MsgType.error, "这是一条错误消息，类型为：GZConsole.MsgType.error");
                GZConsole.WriteLine(GZConsole.MsgType.warring, "这是一条警告消息，类型为：GZConsole.MsgType.warring");

                GZConsole.WriteLine(GZConsole.MsgType.text, "下面开始正式输出内容");
                GZConsole.WriteLine(GZConsole.MsgType.text, "====================================");
            }
        }
        public static void CloseConsole()
        {
            if (ConsoleShow)
            {
                if (importHelper.FreeConsole())
                    ConsoleShow = false;
            }
        }
        /// <summary>  
        /// 输出信息  
        /// </summary>  
        /// <param name="output"></param>  
        public static void WriteLine(MsgType type, string output)
        {
            if (ConsoleShow == false) return;
            Console.ForegroundColor = GetConsoleColor(type, output);
            Console.WriteLine(@"[{0}]{1}", DateTimeOffset.Now, type.ToString() + "   " + output);
        }

        /// <summary>  
        /// 根据输出文本选择控制台文字颜色  
        /// </summary>  
        /// <param name="output"></param>  
        /// <returns></returns>  
        private static ConsoleColor GetConsoleColor(MsgType type, string output)
        {
            ConsoleColor color;
            switch (type)
            {
                case MsgType.warring:
                    color = ConsoleColor.Yellow;
                    break;
                case MsgType.error:
                    color = ConsoleColor.Red;
                    break;
                case MsgType.info:
                    color = ConsoleColor.Green;
                    break;
                default:
                    color = ConsoleColor.Gray;
                    break;
            }
            return color;
        }


    }

    public class importHelper
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();
    }
}
