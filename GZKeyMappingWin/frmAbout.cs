using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GZKeyboardExtension
{
    public partial class frmAbout : Form
    {

        private static frmAbout frm;

        public static void ShowForm()
        {
            if (frm == null || frm.IsDisposed)
            {
                frm = new GZKeyboardExtension.frmAbout();
                frm.ShowDialog();
            }
            else
            {
                frm.Activate();
            }
        }

        public frmAbout()
        {
            InitializeComponent();
        }
    }
}
