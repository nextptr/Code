using CsBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CsBase.Class4
{
    class Class4_6 : classBase
    {
        #region codeStart
        public override void RunTest()
        {
            RegexTest("只能输入数字", @"^[0-9]*$", "123xs", "48566");
            RegexTest("只能输入N位数字", @"^\d{3}$", "12", "123", "1234", "123f");
            RegexTest("至少输入N位数字", @"^\d{3,}$", "12", "123","1234", "123f");
            RegexTest("M~N位数字输入", @"^\d{3,4}$", "12", "123", "1234", "12345");
            RegexTest("只能有两位小数的数字", @"^[0-9]+(.[0-9]{2})?$", "12.3", "12.34", "12.345");
            RegexTest("只能有2~3位小数的数字", @"^[0-9]+(.[0-9]{2,3})?$", "12.3", "12.34", "12.345");
            RegexTest("只能输入非零正整数", @"^\+?[1-9][0-9]*$", "012", "123", "123d");
            RegexTest("只能输入非零负整数", @"^\-[1-9][0-9]*$", "12", "-123");
            RegexTest("只能输入长度为3的字符", @"^.{3}$",",xs", "1s2", "fae5");
            RegexTest("只能输入字母字符串", @"^[A-Za-z]+$",",xs", "1s2", "afsdAFF");

        }

        //可变参数列表函数 params关键字
        public void RegexTest(string testName, string reg, params string []test)
        {
            List<string> rets = new List<string>();
            string val = "";
            foreach (string ts in test)
            {
                val= Regex.Match(ts, reg, RegexOptions.RightToLeft).Value;
                if (val == "")
                {
                    val = "Nul";
                }
                rets.Add(val);
            }
            val = null;
            for (int i = 0; i < test.Length; i++)
            {
                val += $"({test[i]}={rets[i]})";
            }
            ddr($"{testName}: ({reg})匹配{val}");
        }
        #endregion codeEnd
    }
}
