using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extension;

namespace Log
{
    public class Access
    {
        public static Access Instance { get { return GetInstance(); } }
        public string Dir { get; set; }
        public string ActiveFile { get; set; }
        public string UserFile { get; set; }
        public string UserLoginFile
        {
            get
            {
                return "UserLogin.accdb";
            }
        }
        public string TblLog { get; set; }
        public string TblUser { get; set; }
        public string TblUserLogin { get; set; }
        public string Password { get; set; }

        public DateTime CurrentDate { get; set; }

        private const string Provider = "Provider = Microsoft.ACE.OLEDB.12.0;";
        private const string DbPassword = "Jet OLEDB:Database Password";

        private static Access _instance = null;

        private OleDbConnection _odbc = null;

        private Access()
        {
            Dir = "Database";
            ActiveFile = DateTime.Now.ToYMD() + ".accdb";
            UserFile = "User.accdb";
            TblLog = "Logs";
            TblUser = "Users";
            TblUserLogin = "UserLogin";
            //CurrentDate = new DateTime(1, 1, 1);
            CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            DatabaseHelper.CreateFile(Dir + "\\" + UserLoginFile);
            CreateTableUserLogin();

            _odbc = GetOdbc(ActiveFile);
        }

        private static Access GetInstance()
        {
            if (null == _instance)
            {
                _instance = new Access();
            }
            return _instance;
        }

        public void CreateTableUser()
        {
            DatabaseHelper.CreateTable(Dir + "\\" + UserFile, TblUser, UserItem.GetPropertyNames(), Password);
        }

        public void CreateTableUserLogin()
        {
            DatabaseHelper.CreateTable(Dir + "\\" + UserLoginFile, TblUserLogin, UserLoginItem.GetPropertyNames(), Password);
        }

        public void CreateTableLog()
        {
            DatabaseHelper.CreateTable(Dir + "\\" + ActiveFile, TblLog, LogItem.GetPropertyNames(), Password);
            _odbc = GetOdbc(ActiveFile);
        }

        public bool CreateFile(DateTime dt)
        {
            if (CurrentDate.Day != dt.Day ||
                CurrentDate.Month != dt.Month ||
                CurrentDate.Year != dt.Year)
            {
                CurrentDate = new DateTime(dt.Year, dt.Month, dt.Day);
                ActiveFile = CurrentDate.ToYMD() + ".accdb";
                DatabaseHelper.CreateFile(Dir + "\\" + ActiveFile, Password);

                return true;
            }

            return false;
        }

        public void Insert(LogItem item)
        {
            if (CreateFile(DateTime.Now))
            {
                if (_odbc != null && _odbc.State == System.Data.ConnectionState.Open)
                {
                    _odbc.Close();
                }

                CreateTableLog();
                _odbc.Open();
            }
            //DatabaseHelper.Insert(_odbc, item, TblLog);
            DatabaseHelper.InsertOpen(_odbc, item, TblLog);
        }

        private OleDbConnection GetOdbc(string file)
        {
            string path = Dir + "\\" + file;
            string con = Provider + @"Data Source = " + path + ";" + DbPassword + "=" + Password;
            OleDbConnection odbc = new OleDbConnection(con);
            return odbc;
        }

        public void Insert(UserItem item)
        {
            string path = Dir + "\\" + UserFile;
            string con = Provider + @"Data Source = " + path + ";" + DbPassword + "=" + Password;
            OleDbConnection odbc = new OleDbConnection(con);
            DatabaseHelper.Insert(odbc, item, TblUser);
        }
        public void Insert(UserLoginItem item)
        {
            string path = Dir + "\\" + UserLoginFile;
            string con = Provider + @"Data Source = " + path + ";" + DbPassword + "=" + Password;
            OleDbConnection odbc = new OleDbConnection(con);
            DatabaseHelper.Insert(odbc, item, TblUserLogin);
        }

        public void Open()
        {
            _odbc.Open();
        }

        public void Close()
        {
            _odbc.Close();
        }
    }
}
