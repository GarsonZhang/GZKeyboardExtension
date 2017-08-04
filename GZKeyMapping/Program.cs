using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZKeyMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            frmKeyboardHook f = new frmKeyboardHook();
            f.Start();
            while (Console.ReadKey()) {
            }
        }
    }
}
