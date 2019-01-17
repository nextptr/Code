using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace WpfLifeGame
{

    public class bitMap
    {
        public int _size;
        public byte[] arry = null;
        public bitMap(IEnumerable<bool> ls)
        {
            List<bool> tmp = ls as List<bool>;
            _size = tmp.Count;
            int count = _size >> 3;
            if ((_size & 7) > 0)
            {
                count += 1;
            }
            arry = new byte[count];
        }

        public void Set(int index,bool val)
        {
            if (index < 0 || index >= _size)
            {
                return;
            }
            int byt = index >> 3;
            int bit = index & 7;
            if (val)
            {
                arry[byt] = (Byte)(arry[byt] | (1 << bit));
            }
            else
            {
                arry[byt] = (Byte)(arry[byt] & ~(1 << bit));
            }
        }

        public bool Get(int index)
        {
            if (index < 0 || index >= _size)
            {
                return false;
            }
            int byt = index >> 3;
            int bit = index & 7;
            if ((arry[byt] & (1 << bit))==(1<<bit))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    public class BitMap
    {
        protected int _size;
        public byte[] arry = null;
        public int Count
        {
            get
            {
                return _size;
            }
            set
            {

            }
        }

        public BitMap()
        {
            _size = 0;
        }
        public BitMap(List<cell> list)
        {
            SetBitMap(list);
        }

        public void SetBitMap(List<bool> list)
        {
            List<bool> localList = new List<bool>();
            _size = list.Count;
            foreach (var tmp in list)
            {
                localList.Add(tmp);
            }
            for (int i = 0; i < (8 - _size % 8); i++)
            {
                localList.Add(false);
            }
            arry = new byte[localList.Count / 8];
            for (int i = 0; i < arry.Length; i++)
            {
                arry[i] = 0;
            }
            for (int i = 0; i < localList.Count; )
            {
                byte tmp = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (list[i + j])
                    {
                        tmp = (byte)(tmp & (1 << j));
                    }
                }
                arry[i / 8] = tmp;
                i += 8;
            }
        }

        public void SetBitMap(List<cell> list)
        {
            List<bool> localList = new List<bool>();
            _size = list.Count;
            foreach (var tmp in list)
            {
                localList.Add(tmp.LiveNow);
            }
            for (int i = 0; i < (8 - _size % 8); i++)
            {
                localList.Add(false);
            }
            arry = new byte[localList.Count / 8];
            for (int i = 0; i < arry.Length; i++)
            {
                arry[i] = 0;
            }
            for (int i = 0; i < localList.Count;)
            {
                byte tmp = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (localList[i + j])
                    {
                        tmp = (byte)(tmp & (1 << j));
                    }
                }
                arry[i / 8] = tmp;
                i += 8;
            }
        }

        public bool this[int index]
        {
            get
            {
                if (index < 0 || index > _size)
                {
                    return false;
                }
                if ((arry[index / 8] & (1 << (index % 8))) == (1 << (index % 8)))
                {
                    return true;
                }
                return false;
            }
        }

        public void GetBitMap(ref List<bool> list)
        {
            if (arry == null)
            {
                list.Clear();
            }
            else
            {
                for (int i = 0; i < arry.Length - 1; i++)
                {
                    byte tmp = arry[i];
                    for (int j = 0; j < 8; j++)
                    {
                        if ((tmp & (1 << j)) == (1 << j))
                        {
                            list.Add(true);
                        }
                        else
                        {
                            list.Add(false);
                        }
                    }
                }
                for (int i = 0; i < (_size - (arry.Length - 1) * 8); i++)
                {
                    if ((arry[arry.Length - 1] & (1 << i)) == (1 << i))
                    {
                        list.Add(true);
                    }
                    else
                    {
                        list.Add(false);
                    }
                }
            }
        }
    }

    public class Pattern
    {
        public string PatternName = "";
        public List<BitMap> bitMaps = null;
        public Pattern()
        {
            bool a = bitMaps[0][1];
        }
        public Pattern(string str, List<List<cell>> cells)
        {
            PatternName = str;
            bitMaps = new List<BitMap>();
            for (int i = 0; i < cells.Count; i++)
            {
                bitMaps.Add(new BitMap(cells[i]));
            }
        }
    }


    public class CellsParameter
    {
        public List<Pattern> patterns = new List<Pattern>();

        protected static CellsParameter _instance = null;
        public static CellsParameter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CellsParameter();
                }
                return _instance;
            }
        }
        public CellsParameter()
        {

        }

        public bool IsExitPattern(string nam)
        {
            foreach (var tmp in patterns)
            {
                if (tmp.PatternName == nam)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddPattern(string nam, List<List<cell>> cells)
        {
            patterns.Add(new Pattern(nam, cells));
        }
        public void DeletePattern(string nam)
        {
            int count = patterns.Count;
            for (int i = 0; i < count; i++)
            {
                if (patterns[i].PatternName == nam)
                {
                    patterns.RemoveAt(i);
                    return;
                }
            }
        }
        public string GetRootPath()
        {
            string currentPath = Directory.GetCurrentDirectory();
            string[] arr = currentPath.Split('\\');
            int count = arr.Length - 2;
            string ret = "";
            for (int i = 0; i < count; i++)
            {
                ret += arr[i];
                if (i != count - 1)
                {
                    ret += '\\';
                }
            }
            return ret;
        }

        public bool ReadParameter()
        {
            return Read("CellPatterns.xml", ref patterns);
        }
        public void WriteParameter()
        {
            Write("CellPatterns.xml", patterns);
        }
        protected bool Read(string name, ref List<Pattern> testValues)
        {
            //string fileName = Directory.GetCurrentDirectory() + "\\" + name;
            string fileName = GetRootPath() + "\\" + name;
            try
            {
                if (File.Exists(fileName))
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    XmlSerializer ser = new XmlSerializer(testValues.GetType());
                    testValues = ser.Deserialize(fs) as List<Pattern>;
                    fs.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("读取文件失败:" + e.Message);
            }
            return false;
        }
        protected bool Write(string name, List<Pattern> testItem)
        {
            try
            {
                //string fileName = Directory.GetCurrentDirectory() + "\\" + name;
                string fileName = GetRootPath() + "\\" + name;
                FileStream fs = new FileStream(fileName, FileMode.Create);
                XmlSerializer ser = new XmlSerializer(testItem.GetType());//以testItem.GetType()类型进行序列化
                ser.Serialize(fs, testItem);//将testItem对象序列化为fs文件
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("保存文件失败:" + e.Message);
                return false;
            }
        }
    }
}
