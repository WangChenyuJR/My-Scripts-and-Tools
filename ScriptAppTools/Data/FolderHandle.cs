using ScriptAppTools.Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptAppTools.Data
{
    public class FolderHandle
    {
                /// <summary>
                /// 获取指定文件夹下的所有文件名。
                /// </summary>
                /// <param name="folderPath">文件夹路径。</param>
                /// <returns>包含文件名的列表。</returns>
                public static List<string> GetFileNamesInFolder(string folderPath, string fileType)
                {
                        // 创建一个列表来存储文件名
                        List<string> fileNames = new List<string>();

                        // 使用Directory.GetFiles方法获取文件夹中的所有文件
                        List<string> files = Directory.GetFiles(folderPath, fileType, SearchOption.TopDirectoryOnly)
                                .OrderBy(f => f).ToList();

                        // 将每个文件的完整路径添加到列表中
                        foreach (string file in files)
                        {
                                // 只取文件名（不包括路径）
                                string fileName = System.IO.Path.GetFileName(file);
                                fileNames.Add(fileName);
                        }
                        return fileNames;
                }
                /// <summary>
                /// 获取选中文件夹下的所有叶子文件夹
                /// </summary>
                /// <param name="folderPath"></param>
                /// <returns></returns>
                public static List<string> GetLeafFolders(string folderPath)
                {
                        List<string> leafFolders = new List<string>();
                        try
                        {
                                // 获取当前文件夹下的所有子文件夹
                                string[] subFolders = Directory.GetDirectories(folderPath);
                                if (subFolders.Length == 0)
                                {
                                        // 如果没有子文件夹，则是最后一层子文件夹
                                        leafFolders.Add(folderPath);
                                }
                                else
                                {
                                        // 对每个子文件夹递归调用
                                        foreach (string subFolder in subFolders)
                                        {
                                                leafFolders.AddRange(GetLeafFolders( subFolder));
                                        }
                                }
                        }
                        catch (UnauthorizedAccessException)
                        {
                                // 处理权限不足的情况
                        }
                        catch (Exception ex)
                        {
                                // 处理其他可能的异常
                        }
                        return leafFolders;
                }


        }
}
