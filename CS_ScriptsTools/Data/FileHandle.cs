using ScriptsTools.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptsTools.Data
{
    public class FileHandle
    {
                /// <summary>
                /// 图像名和路径解析--获取路径、检测结果、产品SN、测试日期、测试时间、测试结果、NG面、NG项
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                public static void DecodeFileInfo( string FilePath, out bool Res, out string DutSN, out string _1stTestDate, out string _1stTestTime, out string NG_Category, out string NG_Item)
                {
                        string fileName = "";
                        string fileRes = "";
                        string fileStation = "";
                        Res = true;
                        DutSN = "";
                        _1stTestDate = "";
                        _1stTestTime = "";
                        NG_Category = "";
                        NG_Item = "";
                        try
                        {
                                string[] fileSplit = FilePath.Split('\\');//按斜杠提取路径名所有的数据

                                //例： E:\\VisionImages\\20241124\\AVI_Front\\NG\\C46H9Q00HCT0000SY7_正面_20241124103618_N1_软板异物._JT.jpg
                                int fileSplitIndex = FilePath.IndexOf('\\');
                                if (fileSplitIndex != -1)
                                {
                                        fileName = fileSplit[fileSplit.Length - 1];//最后一个斜杠后为图像名
                                        fileRes = fileSplit[fileSplit.Length - 2];//倒数第二个斜杠后为该文件夹下的图像为NG图还是OK图
                                        fileStation = fileSplit[fileSplit.Length - 3]; //倒数第三个斜杠后为该文件检测工站
                                }
                                string[] fileNameSplit = fileName.Split('_');//按下划线提取图像文件名所有的信息
                                //例：C46H9Q00HCT0000SY7_正面_20241124103618_N1_软板异物._JT.jpg
                                // 封装后：C46HCM000820000SY7_AfterWrap_20250115201250__JT.jpg
                                // 封装前：C46HCM0008M0000SY7_PutDownDut_20250115202413__JT.jpg
                                DutSN = fileNameSplit[0];
                                NG_Category = fileNameSplit[1];
                                _1stTestDate = fileNameSplit[2].Substring(0, 8);
                                _1stTestTime = fileNameSplit[2].Substring(8, 6);
                                if (fileRes == "OK")
                                {
                                        Res = true;
                                }
                                else if (fileRes == "NG")
                                {
                                        Res = false;
                                        if (fileStation.Contains("AVI_"))
                                        {
                                                var NozzleID = fileNameSplit[3];
                                                NG_Item = fileNameSplit[4];
                                                string[] singleNG = NG_Item.Split('.');
                                                NG_Item = singleNG[0];
                                        }
                                        else if (fileStation.Contains("PutDownDut"))
                                        {
                                                if (NG_Category == "PutDownDut")
                                                {
                                                        NG_Category = "封装前";
                                                }
                                        }
                                        else if (fileStation.Contains("AfterWrap"))
                                        {
                                                if (NG_Category == "AfterWrap")
                                                {
                                                        NG_Category = "封装后";
                                                }
                                        }
                                }
                                //if (NG_Item.Contains(' '))
                                //{
                                //        NG_Item = NG_Item.Replace(' ', '/');
                                //}
                        }
                        catch (Exception ex)
                        {

                        }
                }

        }
}
