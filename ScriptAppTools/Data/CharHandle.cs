using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptAppTools.Data
{
    public class CharHandle
    {
                /// <summary>
                /// 序列化路径名，将非法字符替换
                /// </summary>
                /// <param name="srcPath"></param>
                /// <param name="tgtPath"></param>
                /// <returns></returns>
                public static void SerializePath(string srcPath, ref string tgtPath)
                {
                        char[] reFloderPath = srcPath.ToCharArray();
                        foreach (char c in reFloderPath)
                        {
                                if (c == ':')
                                {
                                        tgtPath = tgtPath.Replace(c.ToString(), "-"); // 替换冒号为连接线
                                }
                                if (c == '\\')
                                {
                                        tgtPath = tgtPath.Replace(c.ToString(), "."); // 替换反斜杠为加号
                                }

                        }
                }
        }
}
