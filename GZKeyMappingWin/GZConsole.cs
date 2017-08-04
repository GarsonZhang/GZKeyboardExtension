using System;
using System.Collections.Generic;
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
            ConsoleShow = importHelper.AllocConsole();
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
