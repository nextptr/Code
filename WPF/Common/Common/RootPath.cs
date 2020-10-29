using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RootPath
    {
        public static string Root
        {
            get
            {
                return Directory.GetCurrentDirectory().TrimEnd(@"\Debug".ToCharArray());
                //return Directory.GetCurrentDirectory() + @"\..\..";
            }
        }
        public static string ObjDirectory
        {
            get
            {
                string ret = "";
                ret = Directory.GetCurrentDirectory() + @"\..\..\..\..";
                ret = Directory.GetParent(ret).FullName;
                return ret;
            }
        }
    }
}
