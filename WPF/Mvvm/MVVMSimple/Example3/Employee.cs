using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Example3
{
    public class Employee : INotifyPropertyChanged
    {
        public Employee() {
        }
        
        private string _FirstName;
        public string FirstName {
            get {
                return this._FirstName;
            }
            set {
                this._FirstName = value;
            }
        }
        
        private string _LastName;
        public string LastName {
            get {
                return this._LastName;
            }
            set {
                this._LastName = value;
            }
        }
        
        private string _Phone;
        public string Phone {
            get {
                return this._Phone;
            }
            set {
                this._Phone = value;
            }
        }
        
        private string _Email;
        public string Email {
            get {
                return this._Email;
            }
            set {
                this._Email = value;
            }
        }

        
        private Department _Department;
        public Department Department {
            get {
                return this._Department;
            }
            set {
                this._Department = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
