using System;
using System.Reflection;

namespace Log
{
    public class UserItem
    {
        public string Name { get; set; }

        public string UserType { get; set; }

        public string Pwd { get; set; }

        public UserItem()
        {
            Name = "";
            UserType = "";
            Pwd = "";
        }

        public static string[] GetPropertyNames()
        {
            return DatabaseHelper.GetPropertyNames<UserItem>();
        }

        public static PropertyInfo[] GetProperties()
        {
            Type t = typeof(UserItem);
            return t.GetProperties();
        }
    }
}
