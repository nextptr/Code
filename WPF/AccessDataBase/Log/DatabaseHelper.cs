using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;

namespace Log
{
    public static class DatabaseHelper
    {
        private const string Provider = "Provider=Microsoft.ACE.OLEDB.12.0;";

        private const string Password = "Jet OLEDB:Database Password";

        public static void CreateFile(string fileName, string pwd = "")
        {
            string dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (!File.Exists(fileName))
            {
                ADOX.Catalog cat = new ADOX.Catalog();
                cat.Create(Provider + @"Data Source=" + fileName + ";" + Password + "=" + pwd);

                ADODB.Connection con = cat.ActiveConnection as ADODB.Connection;
                if (con != null)
                {
                    con.Close();
                }
                cat.ActiveConnection = null;

                Marshal.ReleaseComObject(cat);
                cat = null;

                GC.Collect();
            }
        }

        public static bool TableExist(string dbName, string tblName, string pwd = "")
        {
            try
            {
                ADOX.Catalog cat = new ADOX.Catalog();
                ADODB.Connection cn = new ADODB.Connection();

                cn.Open(Provider + @"Data Source=" + dbName + ";" + Password + "=" + pwd);
                cat.ActiveConnection = cn;

                for (int i = 0; i < cat.Tables.Count; ++i)
                {
                    ADOX.Table t = cat.Tables[i];
                    if (t.Name == tblName)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return false;
        }

        public static List<object[]> ReadTable(string dbName, string tblName)
        {
            List<object[]> data = new List<object[]>();

            if (!TableExist(dbName, tblName))
            {
                return data;
            }

            OleDbConnection conn = new OleDbConnection(Provider + @"Data Source=" + dbName + ";");
            conn.Open();

            string strSelect = "SELECT * FROM " + tblName;
            OleDbCommand cmd = new OleDbCommand(strSelect, conn);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                object[] values = new object[reader.FieldCount];
                reader.GetValues(values);
                data.Add(values);
            }

            reader.Close();
            conn.Close();

            return data;
        }

        public static List<T> ReadTable<T>(string dbName, string tblName) where T : new()
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();
            List<object[]> data = ReadTable(dbName, tblName);

            for (int i = 0; i < data.Count; ++i)
            {
                object[] values = data[i];
                T t = new T();
                for (int j = 1; j < values.Length; ++j)
                {
                    PropertyInfo property = props[j - 1];

                    TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
                    object result = converter.ConvertFrom(values[j]);
                    property.SetValue(t, result);
                }

                list.Add(t);
            }

            return list;
        }

        public static void DeleteTable(string dbName, string tblName)
        {
            if (!TableExist(dbName, tblName))
            {
                return;
            }

            OleDbConnection myConn = new OleDbConnection(Provider + @"Data Source=" + dbName + ";");
            myConn.Open();

            string strInsert = "DELETE FROM " + tblName;
            OleDbCommand cmd = new OleDbCommand(strInsert, myConn);
            cmd.ExecuteNonQuery();
            myConn.Close();
        }

        public static void CreateTable(string dbName, string tblName, string[] columns, string pwd = "")
        {
            if (TableExist(dbName, tblName, pwd))
            {
                return;
            }

            ADOX.Catalog cat = new ADOX.Catalog();
            ADODB.Connection cn = new ADODB.Connection();

            cn.Open(Provider + @"Data Source=" + dbName + ";" + Password + "=" + pwd);
            cat.ActiveConnection = cn;

            ADOX.TableClass tbl = new ADOX.TableClass();
            tbl.ParentCatalog = cat;
            tbl.Name = tblName;

            {
                ADOX.ColumnClass col = new ADOX.ColumnClass();

                col.ParentCatalog = cat;
                col.Type = ADOX.DataTypeEnum.adInteger;
                col.Name = "Id"; // master key
                col.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                col.Properties["AutoIncrement"].Value = true;
                tbl.Columns.Append(col, ADOX.DataTypeEnum.adInteger, 0);
            }

            for (int i = 0; i < columns.Length; ++i)
            {
                ADOX.ColumnClass col = new ADOX.ColumnClass();

                col.ParentCatalog = cat;
                col.Name = columns[i];
                col.Properties["Jet OLEDB:Allow Zero Length"].Value = true;
                tbl.Columns.Append(col, ADOX.DataTypeEnum.adVarWChar, 25);
            }

            cat.Tables.Append(tbl);
            cn.Close();

            Marshal.ReleaseComObject(tbl);

            tbl = null;
        }

        public static void Insert(object item, string dbName, string tblName)
        {
            string path = dbName;
            string con = Provider + @"Data Source = " + path;
            OleDbConnection odbc = new OleDbConnection(con);
            Insert(odbc, item, tblName);
        }

        public static void Insert(OleDbConnection odbc, object item, string tblName)
        {
            string cmdText = GetInsertCmdText(item.GetType(), tblName);

            odbc.Open();

            OleDbCommand sqlcmd = new OleDbCommand(cmdText, odbc);

            PropertyInfo[] properties = item.GetType().GetProperties();

            for (int i = 0; i < properties.Length; ++i)
            {
                string paraName = "@" + properties[i].Name.ToUpper();
                sqlcmd.Parameters.Add(paraName, OleDbType.VarChar, 255);
            }

            try
            {
                for (int i = 0; i < properties.Length; ++i)
                {
                    PropertyInfo prop = properties[i];
                    string paraName = "@" + prop.Name.ToUpper();
                    sqlcmd.Parameters[paraName].Value = prop.GetValue(item, null);
                }

                sqlcmd.ExecuteNonQuery();

                odbc.Close();
            }
            catch (OleDbException ex)
            {
                odbc.Close();
                throw ex;
            }
        }

        public static void InsertOpen(OleDbConnection odbc, object item, string tblName)
        {
            string cmdText = GetInsertCmdText(item.GetType(), tblName);

            OleDbCommand sqlcmd = new OleDbCommand(cmdText, odbc);

            PropertyInfo[] properties = item.GetType().GetProperties();

            for (int i = 0; i < properties.Length; ++i)
            {
                string paraName = "@" + properties[i].Name.ToUpper();
                sqlcmd.Parameters.Add(paraName, OleDbType.VarChar, 255);
            }

            try
            {
                for (int i = 0; i < properties.Length; ++i)
                {
                    PropertyInfo prop = properties[i];
                    string paraName = "@" + prop.Name.ToUpper();
                    sqlcmd.Parameters[paraName].Value = prop.GetValue(item, null);
                }

                sqlcmd.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                //    odbc.Close();
            }
        }

        private static string GetInsertCmdText(Type t, string tblName)
        {
            PropertyInfo[] properties = t.GetProperties();

            string s1 = "";
            string s2 = "";
            for (int i = 0; i < properties.Length; ++i)
            {
                PropertyInfo p = properties[i];
                s1 += p.Name;
                s1 += ",";
                s2 += "@" + p.Name.ToUpper();
                s2 += ",";
            }
            s1 = s1.Remove(s1.Length - 1, 1);
            s2 = s2.Remove(s2.Length - 1, 1);

            string cmdText = "INSERT INTO ";
            cmdText += tblName + "(" + s1 + ") ";
            cmdText += "VALUES(" + s2 + ")";

            return cmdText;
        }

        public static int GetRowsCount(string dbName, string tblName)
        {
            string path = dbName;
            string con = Provider + @"Data Source = " + path;
            OleDbConnection odbc = new OleDbConnection(con);

            string cmdText = "SELECT COUNT(*) FROM " + tblName;

            odbc.Open();
            OleDbCommand sqlcmd = new OleDbCommand(cmdText, odbc);
            int rows = (int)sqlcmd.ExecuteScalar();
            odbc.Close();

            return rows;
        }

        public static string[] GetPropertyNames<T>()
        {
            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties();

            string[] pro = new string[properties.Length];
            for (int i = 0; i < pro.Length; ++i)
            {
                pro[i] = properties[i].Name;
            }

            return pro;
        }
    }
}
