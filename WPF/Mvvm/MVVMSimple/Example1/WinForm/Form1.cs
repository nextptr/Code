using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForm
{
    public partial class Form1 : Form
    {
        MyClass _MyClass = new MyClass();
        public Form1() {
            InitializeComponent();

            this.label1.Text = _MyClass.Time;
            this.label2.DataBindings.Add("Text", _MyClass, "Time");
        }

        /// <summary>
        /// 更新了MyClass的Time属性,但界面没有更新.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e) {
            _MyClass.Time = DateTime.Now.ToString();
            this.label1.Text = _MyClass.Time;
        }
    }
}
