using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GZKeyboardExtension
{
    public partial class frmMain : Form
    {
        //勾子管理类   
        private KeyboardHookLib _keyboardHook = null;

        public frmMain()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //安装勾子   
            _keyboardHook = new KeyboardHookLib();
            _keyboardHook.InstallHook(this.OnKeyPress);
            lblMsg.Text = "键盘映射已经启动，祝你工作愉快！！！";


            //menu_CloseConsole.Visible = GZConsole.ConsoleShow;

        }


        /// <summary>
        /// 按下按键
        /// </summary>
        int KEYEVENTF_KEYDOWN = 0x00;
        /// <summary>
        /// 该值可以让HOME END LEFT RIGHT等带上shift效果，如过单纯使用KEYEVENTF_KEYDOWN则并不会带上shift按下效果
        /// </summary>
        int KEYEVENTF_EXTENDEDKEY = 0x01;
        /// <summary>
        /// 弹起按键
        /// </summary>
        int KEYEVENTF_KEYUP = 0x02;


        bool enable;

        /// <summary>   
        /// 客户端键盘捕捉事件.   
        /// </summary>   
        /// <param name="hookStruct">由Hook程序发送的按键信息</param>   
        /// <param name="handle">是否拦截</param>   
        public void OnKeyPress(KeyboardHookLib.HookStruct hookStruct, out bool handle)
        {
            handle = false; //预设不拦截任何键   
                            //string str;



            //Keys key = (Keys)hookStruct.vkCode;
            //string str = $"你按下:{Control.ModifierKeys}({(int)Control.ModifierKeys }) + {key}({(int)key})";
            ////addLog(str);
            //addLog($"vkCode：{hookStruct.vkCode}  scanCode：{hookStruct.scanCode}  flags：{hookStruct.flags}  time：{hookStruct.time}  dwExtraInfo：{hookStruct.dwExtraInfo}");

            //if (hookStruct.vkCode == (int)Keys.LWin)
            //{
            //    enable = hookStruct.flags == 1;
            //    return;
            //}

            if (hookStruct.flags > 0)
            {
                return;
            }
            //if (hookStruct.vkCode == (int)Keys.OemOpenBrackets)
            //{
            //    addLog("Home");
            //    keybd_event((int)Keys.LWin, 0, KEYEVENTF_KEYUP, 0);
            //    keybd_event((int)Keys.Home, 0, KEYEVENTF_KEYDOWN, 0);
            //    keybd_event((int)Keys.Home, 0, KEYEVENTF_KEYUP, 0);
            //}
            //if (hookStruct.vkCode == (int)Keys.Oem6)
            //{
            //    addLog("End");
            //    keybd_event((int)Keys.LWin, 0, KEYEVENTF_KEYUP, 0);
            //    keybd_event((int)Keys.End, 0, KEYEVENTF_KEYDOWN, 0);
            //    keybd_event((int)Keys.End, 0, KEYEVENTF_KEYUP, 0);
            //}
            //return;
            //按键映射参见:http://www.cppblog.com/ztwaker/archive/2006/08/23/11614.html

            //Control+Shift+alt+] 选择整行内容
            if (Control.ModifierKeys == (Keys.Control | Keys.Shift | Keys.Alt) && hookStruct.vkCode == (int)Keys.OemOpenBrackets)
            {
                addLog("END→Shift+Home 选择一整行，从行尾到行首");
                keybd_event((int)Keys.Oem6, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.Menu, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.End, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event((int)Keys.End, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.Home, 0, KEYEVENTF_EXTENDEDKEY, 0);
                keybd_event((int)Keys.Home, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event((int)Keys.Menu, 0, KEYEVENTF_KEYDOWN, 0);

                handle = true;
                return;
                
            }
            //Control+Shift+alt+] 选择整行内容
            if (Control.ModifierKeys == (Keys.Control | Keys.Shift | Keys.Alt) && hookStruct.vkCode == (int)Keys.Oem6)
            {
                addLog("Home→Shift+End 选择一整行，从行首到行尾");
                //addLog("映射为SHIFT+HOME键");
                //弹起按键，以免影响到Home,End操作
                keybd_event((int)Keys.OemOpenBrackets, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.Menu, 0, KEYEVENTF_KEYUP, 0);

                //按下home键，光标移动至行首
                keybd_event((int)Keys.Home, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event((int)Keys.Home, 0, KEYEVENTF_KEYUP, 0);
                //按下End键，并附带Shift键扩展，选中整行，第三个参数1表示扩展按键
                keybd_event((int)Keys.End, 0, KEYEVENTF_EXTENDEDKEY, 0);
                keybd_event((int)Keys.End, 0, KEYEVENTF_KEYUP, 0);
                //恢复按键按下状态
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event((int)Keys.Menu, 0, KEYEVENTF_KEYDOWN, 0);
                //addLog("结束");
                handle = true;
                return;
            }


            //Control+Shift+[ 映射为Shift+HOME组合按键,选择光标到行首的内容
            if (Control.ModifierKeys == (Keys.Control | Keys.Shift) && hookStruct.vkCode == (int)Keys.OemOpenBrackets)
            {
                addLog("Shift+Home从光标选择到行首");
                keybd_event((int)Keys.OemOpenBrackets, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYUP, 0);//弹起Control
                keybd_event((int)Keys.Home, 0, KEYEVENTF_EXTENDEDKEY, 0);
                keybd_event((int)Keys.Home, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYDOWN, 0);//恢复按下Control
                handle = true;
                return;
            }

            //Control+Shift+] 映射为Shift+END组合按键，选择光标到行尾的文本
            if (Control.ModifierKeys == (Keys.Control | Keys.Shift) && hookStruct.vkCode == (int)Keys.Oem6)
            {
                addLog("Shift+END 从光标选择到行尾");
                keybd_event((int)Keys.Oem6, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYUP, 0);//恢复按下Control
                keybd_event((int)Keys.End, 0, KEYEVENTF_EXTENDEDKEY, 0);
                keybd_event((int)Keys.End, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYDOWN, 0);//恢复按下Control
                handle = true;
                return;
            }

            //Control+[ 映射为HOME按键
            if (Control.ModifierKeys == Keys.Control && hookStruct.vkCode == (int)Keys.OemOpenBrackets)
            {
                addLog("HOME 光标移动到行首");
                keybd_event((int)Keys.OemOpenBrackets, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYUP, 0);//弹起Control
                keybd_event((int)Keys.Home, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event((int)Keys.Home, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYDOWN, 0);//恢复按下Control
                handle = true;
                return;
            }

            //Control+] 映射为END按键
            if (Control.ModifierKeys == Keys.Control && hookStruct.vkCode == (int)Keys.Oem6)
            {
                addLog("END 光标移动到行尾");
                keybd_event((int)Keys.Oem6, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYUP, 0);//弹起Control
                keybd_event((int)Keys.End, 0, KEYEVENTF_KEYDOWN, 0);
                keybd_event((int)Keys.End, 0, KEYEVENTF_KEYUP, 0);
                keybd_event((int)Keys.ControlKey, 0, KEYEVENTF_KEYDOWN, 0);//恢复按下Control
                handle = true;
                return;
            }

        }


        void addLog(string str)
        {
            GZConsole.WriteLine(GZConsole.MsgType.info, str);
        }

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
            byte bVk,
            byte bScan,
            int dwFlags,  //这里是整数类型  0 为按下，2为释放
              int dwExtraInfo  //这里是整数类型 一般情况下设成为 0
        );

        [DllImport("user32.dll")]
        public static extern int MapVirtualKey(uint Ucode, uint uMapType);

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                //卸载钩子
                _keyboardHook.UninstallHook();
                //notifyIcon1.Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;

            }


        }

        private void btn_about_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAbout.ShowForm();
        }

        private void menu_About_Click(object sender, EventArgs e)
        {
            frmAbout.ShowForm();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            ShowMain();
        }

        private void btn_ShowMain_Click(object sender, EventArgs e)
        {
            ShowMain();
        }

        private void ShowMain()
        {
            this.ShowInTaskbar = true;
            this.Show();
            this.Activate();
            this.WindowState = FormWindowState.Normal;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }

        private void menu_CloseConsole_Click(object sender, EventArgs e)
        {
            GZConsole.CloseConsole();
        }

        private void menu_ShowConsole_Click(object sender, EventArgs e)
        {
            GZConsole.OpenConsole();
        }
    }
}
