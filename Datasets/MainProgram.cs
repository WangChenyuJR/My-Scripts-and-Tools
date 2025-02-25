using Datasets;
using ScriptAppTools;

namespace Datasets
{
        public class MainProgram
        {
                public static void Run()
                {
                        string a = $@"E:\DL资料\dataset\Avary\ava_gen2front_test\C46H9Q00SB80000SY7_正面_20250104153452_N4_Conn损伤或变形._YT.xml";
                        DataSets.Format.generate.HikVT_Annotation.Gain_SegAnno_HikVT_XML(a, out DataSets.Format.generate.HikVT_Annotation.hikVT_Seg_RootXMLObject rootObject);
                        //string fdPth = "";
                        //List<string> xmlList  = ScriptAppTools.Data.FolderHandle.GetFileNamesInFolder(fdPth, "*.xml");
                        //foreach (string xml in xmlList)
                        //{
                        //        DataSets.Format.generate.HikVT_Annotation.Gain_SegAnno_HikVT_XML(xml, out DataSets.Format.generate.HikVT_Annotation.hikVT_Seg_RootXMLObject rootObject);
                        //}
                }
        }
}
