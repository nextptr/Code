using Common.Extension;
using System;
using System.Reflection;

namespace Log
{
    public class LogItem
    {
        public string LogDate { get; set; }
        public string LogTime { get; set; }
        public string Kind { get; set; }
        public string Message { get; set; }

        public LogItem()
        {
            LogDate = DateTime.Now.ToYMD();
            LogTime = DateTime.Now.ToLongTimeString();
            Kind = "";
            Message = "";
        }

        public LogItem(string kind, string message)
        {
            LogDate = DateTime.Now.ToYMD();
            LogTime = DateTime.Now.ToLongTimeString();
            Kind = kind;
            Message = message;
        }

        public static string[] GetPropertyNames()
        {
            return DatabaseHelper.GetPropertyNames<LogItem>();
        }

        public static PropertyInfo[] GetProperties()
        {
            Type t = typeof(LogItem);
            return t.GetProperties();
        }

        public override string ToString()
        {
            string s = LogDate + "," + LogTime + "," + Kind + "," + Message;
            return s;
        }
    }
}
