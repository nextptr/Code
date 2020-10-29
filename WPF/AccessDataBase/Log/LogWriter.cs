namespace Log
{
    public class LogWriter
    {
        #region  单例
        private static volatile LogWriter instance;
        private static readonly object obj = new object();
        private LogWriter() { }
        public static LogWriter Instance
        {
            get
            {
                if (null == instance)
                {
                    lock (obj)
                    {
                        if (null == instance)
                        {
                            instance = new LogWriter();
                        }
                    }

                }
                return instance;
            }
        }
        #endregion

        public void LogAlarm(string msg)
        {
            LogItem item = new LogItem("报警", msg);
            Access.Instance.Insert(item);

        }
        public void LogProcess(string msg)
        {
            LogItem item = new LogItem("流程", msg);
            Access.Instance.Insert(item);

        }
        public void LogOperation(string msg)
        {
            LogItem item = new LogItem("操作", msg);
            Access.Instance.Insert(item);
        }
        public void LogCommunication(string msg)
        {
            LogItem item = new LogItem("通信", msg);
            Access.Instance.Insert(item);

        }
        public void LogParameter(string msg)
        {
            LogItem item = new LogItem("参数", msg);
            Access.Instance.Insert(item);

        }
    }
}
