using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Model;

namespace Example5
{
    public static class DataBase
    {
        private static List<Employee> _Employees = new List<Employee> 
            {
                new Employee{Name="肥猫"},
                new Employee{Name="大牛"},
                new Employee{Name="猪头"},
            };

        public static List<Employee> AllEmployees {
            get {
                return _Employees;
            }
            set {
                _Employees = value;
            }
        }
    }
}
