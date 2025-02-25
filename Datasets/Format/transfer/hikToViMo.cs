using ScriptAppTools.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataSets.Format.generate.HikVT_Annotation;
using static DataSets.Format.generate.ViMo_Annotation;
namespace Datasets.Format.transfer
{
        public class hikToViMo
        {
                public static void HikVT_XML_TO_ViMo_JSON(hikVT_Seg_RootXMLObject rootXMLObject)
                {
                        string imgPath = rootXMLObject.imgPth;
                        string imgName = FileHandle.DecodeFileInfoInit(imgPath)[FileHandle.DecodeFileInfoInit(imgPath).Length -1];

                        ViMo_Seg_RootJSONObject viMo_segObj = new ViMo_Seg_RootJSONObject();
                        viMo_segObj.Flags = new Dictionary<string, object>();
                        viMo_segObj.ImageData = null;
                        viMo_segObj.ImageHeight = 3648;
                        viMo_segObj.ImagePath = imgName;
                        viMo_segObj.ImageWidth = 5472;//TODO 需要开放
                        viMo_segObj.ImageTags = new List<string>();
                        viMo_segObj.Ng = false;//TODO 后续需考虑空xml文件，海康VT标注为OK的样本，XML文件为空
                        viMo_segObj.Qualified = false;
                       // rootXMLObject.
                        //viMo_segObj.Shapes =
                       // viMo_segObj.Version = "0.0.1";
                }
        }
}
