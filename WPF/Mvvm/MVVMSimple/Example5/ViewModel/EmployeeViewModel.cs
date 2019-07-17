using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Example.Model;
using System.Windows.Input;
using System.Windows;

namespace Example5.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 供ComboBox绑定
        /// </summary>
        public ObservableCollection<Employee> Employees { get; set; }

        public EmployeeViewModel() {
            Employees = new ObservableCollection<Employee>(DataBase.AllEmployees);
        }

        #region 供textbox 绑定        
        private string _NewEmployeeName;
        public string NewEmployeeName {
            get {
                return this._NewEmployeeName;
            }
            set {
                if (this._NewEmployeeName != value) {
                    this._NewEmployeeName = value;
                    if (this.PropertyChanged != null) {
                        PropertyChanged(this, new PropertyChangedEventArgs("NewEmployeeName"));
                    }
                }
            }
        }

        private string _NewEmployeeEmail;
        public string NewEmployeeEmail {
            get {
                return this._NewEmployeeEmail;
            }
            set {
                if (this._NewEmployeeEmail != value) {
                    this._NewEmployeeEmail = value;
                    if (this.PropertyChanged != null) {
                        PropertyChanged(this, new PropertyChangedEventArgs("NewEmployeeEmail"));
                    }
                }
            }
        }

        private string _NewEmployeePhone;
        public string NewEmployeePhone {
            get {
                return this._NewEmployeePhone;
            }
            set {
                if (this._NewEmployeePhone != value) {
                    this._NewEmployeePhone = value;
                    if (this.PropertyChanged != null) {
                        PropertyChanged(this, new PropertyChangedEventArgs("NewEmployeePhone"));
                    }
                }
            }
        }
        #endregion

        public ICommand AddEmployee {
            get {
                return new RelayCommand(new Action(() =>
                            {
                                if (string.IsNullOrEmpty(NewEmployeeName)) {
                                    MessageBox.Show("姓名不能为空!");
                                    return;
                                }
                                var newEmployee = new Employee { Name = _NewEmployeeName, Email = _NewEmployeeEmail, Phone = _NewEmployeePhone };
                                newEmployee.Add();
                                Employees.Add(newEmployee);
                            }));
            }
        }
    }
}
