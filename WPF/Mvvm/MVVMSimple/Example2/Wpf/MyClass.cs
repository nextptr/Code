using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Example3
{
    public class MyClass : INotifyPropertyChanged
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
                if (this._Time != value) {
                    this._Time = value;
                    if (PropertyChanged != null) {
                        PropertyChanged(this, new PropertyChangedEventArgs("Time"));
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
