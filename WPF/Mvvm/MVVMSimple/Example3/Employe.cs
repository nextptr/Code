using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;

namespace Example3
{
    public class Employe 
    {
        public ObservableCollection<string> Employees { get; set; }

        public Employe() {
            Employees = new ObservableCollection<string>() 
            {
                "肥猫", "大牛", "猪头"
            };
        }
    }
}
