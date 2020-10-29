using System;

namespace Log
{
    public sealed class UserLoginItem
    {
        public string UType { get; set; }
        public string UID { get; set; }
        public string UDate { get; set; }
        public string UTime { get; set; }
        public UserLoginItem(string userType, string userID, DateTime dateTime)
        {
            this.UType = userType;
            this.UID = userID;
            this.UDate = dateTime.ToLongDateString();
            this.UTime = dateTime.ToLongTimeString();
        }
        public static string[] GetPropertyNames()
        {
            return DatabaseHelper.GetPropertyNames<UserLoginItem>();
        }
    }
}
