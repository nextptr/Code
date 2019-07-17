using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinForm
{
    public class MyClass
    {           
        public MyClass() {
            this._Time = DateTime.Now.ToString();
        }     

        private string _Time;
        public string Time {
            get {
                return this._Time;
            }
            set {
                this._Time = value;
            }
        }
    }
}
