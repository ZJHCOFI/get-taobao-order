using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Get_taobao_order
{
    public partial class Update_record : Form
    {
        public Update_record()
        {
            InitializeComponent();
        }

        //点击超链接
        private void richTextBox_update_record_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
