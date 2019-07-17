using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace Example4
{
    public class Employee : INotifyPropertyChanged
    {
        public ObservableCollection<string> Employees { get; set; }

        public Employee() {
            Employees = new ObservableCollection<string>() 
            {
                "肥猫", "大牛", "猪头"
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _NewEmployee;
        public string NewEmployee {
            get {
                return this._NewEmployee;
            }
            set {
                if (this._NewEmployee != value) {
                    this._NewEmployee = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("NewEmployee"));
                }
            }
        }

        private ICommand _AddEmployee;
        public ICommand AddEmployee {
            get {
                if (_AddEmployee == null) {
                    _AddEmployee = new AddEmployeeCommand((p) =>
                    {
                        Employees.Add(NewEmployee);
                    });
                }
                return _AddEmployee;
            }
        }
    }

    public class AddEmployeeCommand : ICommand
    {
        Action<object> _Execute;
        public AddEmployeeCommand(Action<object> execute) {
            _Execute = execute;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter) {
            _Execute(parameter);
        }
    }
}
