using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using Example5;

namespace Example.Model
{
    /// <summary>
    /// 一个Poco类
    /// </summary>
    public class Employee
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public void Add() {
            DataBase.AllEmployees.Add(this);
        }
    }
}
