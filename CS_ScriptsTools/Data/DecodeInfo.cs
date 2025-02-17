using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.IO;

namespace ScriptsTools
{
        public class DecodeInfo
        {
                /// <summary>
                /// 传图
                /// </summary>
                private void DecodeFileName()
                {
                        var rootFolderPath = "";
                        int count_curUploadingIndex = 0;//当前上传量索引
                        try
                        {
                                using (System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog())
                                {
                                        folderBrowserDialog.Description = "请选择复测图像";

                                        //所选路径名
                                        folderBrowserDialog.SelectedPath = "";
                                        if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                        {
                                                rootFolderPath = folderBrowserDialog.SelectedPath;
                                                var exclusionPath = "Pick";
                                                List<string> leafFolders = Data.FolderHandle.GetLeafFolders(rootFolderPath).Where(fileName => !fileName.Contains(exclusionPath)).ToList();

                                                // 打印结果或保存到文件
                                                for (int upIndex = 0; upIndex < leafFolders.Count; upIndex++)
                                                {
                                                        //folderPath: E:\\VisionImages\\20241124\\AVI_Front\\NG\\C46H9Q00HCT0000SY7_正面_20241124103618_N1_软板异物._JT.jpg
                                                        #region 遍历该文件夹
                                                        var exclusionString = "YT"; //排除原图
                                                        List<string> fileList = Data.FolderHandle.GetFileNamesInFolder(leafFolders[upIndex]);
                                                        List<string> filteredFileList = fileList.Where(fileName => !fileName.Contains(exclusionString)).ToList();
                                                        if (filteredFileList.Count == 0)
                                                        {
                                                                continue;
                                                        }
                                                        string svFolderPath = leafFolders[upIndex];
                                                        Data.CharHandle.SerializePath(leafFolders[upIndex], ref svFolderPath);
                                                        #endregion

                                                        #region 开始上传，并记录上传进度
                                                        double testingStage = 0;
                                                        //var para = MachineCalibPara.Instance as MachineCalibPara;
                                                        //if (para.D_AviImgUploadingProcess.ContainsKey(svFolderPath))//本地参数文件读取该文件夹的上传进度
                                                        //{
                                                        //        count_curUploadingIndex = para.D_AviImgUploadingProcess[svFolderPath].UploadedCount;
                                                        //        testingStage = para.D_AviImgUploadingProcess[svFolderPath].UploadedRate;
                                                        //}
                                                        //else
                                                        //{
                                                        //        para.D_AviImgUploadingProcess[svFolderPath] = new UploadingPara();
                                                        //}
                                                        DecodeFileInfo_UploadAviImgs(filteredFileList, leafFolders[upIndex], svFolderPath, count_curUploadingIndex, testingStage);
                                                        #endregion
                                                }
                                        }
                                }

                        }
                        catch (Exception ex)
                        {

                        }
                }
                /// <summary>
                /// 解析文件名并上传AVI图像
                /// </summary>
                /// <param name="guid"></param>
                /// <param name="filteredFileList"></param>
                /// <param name="folderPath"></param>
                /// <param name="count_Uploaded"></param>
                /// <param name="testingStage"></param>
                /// <param name="para"></param>
                public static void DecodeFileInfo_UploadAviImgs(List<string> filteredFileList, string folderPath, string svFolderPath, int count_Uploaded, double testingStage)
                {
                        var startIndex = count_Uploaded;
                        var imgTotalNums = filteredFileList.Count;
                        try
                        {
                                for (int upIndex = startIndex; upIndex < imgTotalNums; upIndex++)
                                {
                                        //判断是否暂停？True为上传状态，False为暂停状态
                                        if (Global.Utility.IsAviUploading)
                                        {
                                                #region 解析文件名 
                                                //首次结果
                                                string fileName = filteredFileList[upIndex].ToString();
                                                string pathFileName = $@"{folderPath}\{fileName}";
                                                Data.FileHandle.DecodeFileInfo( pathFileName, out bool AviRes, out string DutSN, out string _1stTestDate, out string _1stTestTime, out string NG_Category, out string NG_Item);
                                                #endregion
                                                //string NG_Value = "";
                                                //if (string.IsNullOrEmpty(NG_Item))
                                                //{
                                                //        NG_Value = "";
                                                //}
                                                //else
                                                //{
                                                //        var tmpValue = UtilityTools.GetEnumByDescription<EnumCheckResultType>(NG_Item);
                                                //        NG_Value = ((int)tmpValue).ToString();
                                                //}

                                                //var startTime = CmvdCNNSegmentTool.GetTimeStamp();
                                                var errMsg = "";
                                                using (ManualResetEvent upldImgEvent = new ManualResetEvent(false))
                                                {
                                                        Thread upldThread = new Thread(() =>
                                                        {
                                                                //errMsg = MesHelper.Instance.UpLoadAviImg_JPG(DutSN, AviRes, pathFileName, _1stTestDate, _1stTestTime, new I0018_Req_Data_TestResult_FailContent() { FailCategory = NG_Category, FailItem = NG_Item, FailItemVal = NG_Item });
                                                                upldImgEvent.Set();
                                                        });
                                                        upldThread.Start();
                                                        if (!upldImgEvent.WaitOne(500))
                                                        {
                                                                //var uploadTimeOut = CmvdCNNSegmentTool.GetTimeStamp() - startTime;
                                                                return;
                                                        }
                                                }
                                                //var uploadTime = CmvdCNNSegmentTool.GetTimeStamp() - startTime;
                                                count_Uploaded++;
                                                testingStage = ((double)count_Uploaded / imgTotalNums) * 100;//当前测试进度百分比

                                        }
                                        else
                                        {
                                                break;
                                        }
                                }
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {

                        }




                }
        }
}
