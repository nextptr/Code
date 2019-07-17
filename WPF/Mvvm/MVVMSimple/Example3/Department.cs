using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Example3
{
    public class Department 
    {        
        private string _Name;
        public string Name {
            get {
                return this._Name;
            }
            set {
                this._Name = value;
            }
        }
    }
}
