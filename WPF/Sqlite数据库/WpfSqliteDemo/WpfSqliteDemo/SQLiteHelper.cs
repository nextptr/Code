using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSqliteDemo
{
    public class TableInfo
    {
        public int Cid;            //0 序号
        public string Name;        //1 名字
        public string Type;        //2 数据类型
        public int NotNull;        //3 能否为null值,0不能，1能
        public string Dflt_value;  //4 缺省值
        public int pk;             //5 是否是主键，0否，1是
    }


    public class SQLiteHelper
    {
        protected string DbPath = "";
        protected SQLiteConnection _con=null;
        public static SQLiteHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SQLiteHelper();
                }
                return _instance;
            }
        }
        protected static SQLiteHelper _instance = null;
        protected SQLiteHelper()
        {
        }


        public void CreateDB(string DirPath,string DbName)
        {
            DbPath = DirPath + "\\" + DbName;
            if (File.Exists(DbPath))
            {
                return;
            }
            _con = new SQLiteConnection("data source=" + DbPath);
            _con.Open();
            _con.Close();

        }
        public void DeleteDB(string DirPath, string DbName)
        {
            if (File.Exists(DirPath + "\\" + DbName))
            {
                File.Delete(DirPath + "\\" + DbName);
            }
        }


        public bool CreateTabel(string tableName)
        {
            if (_con == null) { return false; }
            if (_con.State != ConnectionState.Open)
            {
                _con.Open();
            }
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = _con;
            cmd.CommandText = string.Format("CREATE TABLE IF NOT EXISTS {0}" + tableName + "(id varchar(4),score int)");
            cmd.ExecuteNonQuery();
            _con.Close();
            return true;
        }
        public void DeleteTabel(string tableName)
        {
            if (_con == null) { return; }
            //如果存在同名表则返回true
            if (_con.State != ConnectionState.Open)
            {
                _con.Open();
            }
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = _con;
            cmd.CommandText = "DROP TABLE IS EXISTS " + tableName;
            cmd.ExecuteNonQuery();
            _con.Close();
        }

        public TableInfo GetTableInfo(string tableName)
        {
            List<string> testList = new List<string>();

            if (_con == null) { return null; }
            if (_con.State != ConnectionState.Open)
            {
                _con.Open();
            }
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = _con;
            cmd.CommandText = string.Format("PRAGMA table_info('{0}')", tableName);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string tmp = "";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    tmp += $"{reader[i]},";
                }
                testList.Add(tmp);
            }
            reader.Close();
            _con.Close();

            TableInfo ret = new TableInfo();
            return ret;
        }



    }

    //public class SQLiteHelper
    //{
    //    //创建数据库文件
    //    public static void CreateDBFile(string fileName)
    //    {
    //        string path = System.Environment.CurrentDirectory + @"/Data/";
    //        if (!Directory.Exists(path))
    //        {
    //            Directory.CreateDirectory(path);
    //        }
    //        string databaseFileName = path + fileName;
    //        if (!File.Exists(databaseFileName))
    //        {
    //            SQLiteConnection.CreateFile(databaseFileName);
    //        }
    //    }

    //    //生成连接字符串
    //    private static string CreateConnectionString()
    //    {
    //        SQLiteConnectionStringBuilder connectionString = new SQLiteConnectionStringBuilder();
    //        connectionString.DataSource = @"data/ScriptHelper.db";

    //        string conStr = connectionString.ToString();
    //        return conStr;
    //    }

    //    /// <summary>
    //    /// 对插入到数据库中的空值进行处理
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public static object ToDbValue(object value)
    //    {
    //        if (value == null)
    //        {
    //            return DBNull.Value;
    //        }
    //        else
    //        {
    //            return value;
    //        }
    //    }

    //    /// <summary>
    //    /// 对从数据库中读取的空值进行处理
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public static object FromDbValue(object value)
    //    {
    //        if (value == DBNull.Value)
    //        {
    //            return null;
    //        }
    //        else
    //        {
    //            return value;
    //        }
    //    }

    //    /// <summary>
    //    /// 执行非查询的数据库操作
    //    /// </summary>
    //    /// <param name="sqlString">要执行的sql语句</param>
    //    /// <param name="parameters">参数列表</param>
    //    /// <returns>返回受影响的条数</returns>
    //    public static int ExecuteNonQuery(string sqlString, params SQLiteParameter[] parameters)
    //    {
    //        string connectionString = CreateConnectionString();
    //        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
    //        {
    //            conn.Open();
    //            using (SQLiteCommand cmd = conn.CreateCommand())
    //            {
    //                cmd.CommandText = sqlString;
    //                foreach (SQLiteParameter parameter in parameters)
    //                {
    //                    cmd.Parameters.Add(parameter);
    //                }
    //                return cmd.ExecuteNonQuery();
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 执行查询并返回查询结果第一行第一列
    //    /// </summary>
    //    /// <param name="sqlString">SQL语句</param>
    //    /// <param name="sqlparams">参数列表</param>
    //    /// <returns></returns>
    //    public static object ExecuteScalar(string sqlString, params SQLiteParameter[] parameters)
    //    {
    //        string connectionString = CreateConnectionString();
    //        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
    //        {
    //            conn.Open();
    //            using (SQLiteCommand cmd = conn.CreateCommand())
    //            {
    //                cmd.CommandText = sqlString;
    //                foreach (SQLiteParameter parameter in parameters)
    //                {
    //                    cmd.Parameters.Add(parameter);
    //                }
    //                return cmd.ExecuteScalar();
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 查询多条数据
    //    /// </summary>
    //    /// <param name="sqlString">SQL语句</param>
    //    /// <param name="parameters">参数列表</param>
    //    /// <returns>返回查询的数据表</returns>
    //    public static DataTable GetDataTable(string sqlString, params SQLiteParameter[] parameters)
    //    {
    //        string connectionString = CreateConnectionString();
    //        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
    //        {
    //            conn.Open();
    //            using (SQLiteCommand cmd = conn.CreateCommand())
    //            {
    //                cmd.CommandText = sqlString;
    //                foreach (SQLiteParameter parameter in parameters)
    //                {
    //                    cmd.Parameters.Add(parameter);
    //                }
    //                DataSet ds = new DataSet();
    //                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
    //                adapter.Fill(ds);
    //                return ds.Tables[0];
    //            }
    //        }
    //    }
    //}
}
