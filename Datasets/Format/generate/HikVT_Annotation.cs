using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DataSets.Format.generate.HikVT_Annotation;
using static DataSets.Format.generate.ViMo_Annotation;

namespace DataSets.Format.generate
{
        public class HikVT_Annotation
        {
                /// <summary>
                /// 获取海康VisionTrain图像分割标注文件XML内容
                /// </summary>
                /// <param name="rootObject"></param>
                public static void Gain_SegAnno_HikVT_XML(string xmlFilePath, out hikVT_Seg_RootXMLObject rootObject)
                {
                        // 加载XML文件
                        XDocument xmlDoc = XDocument.Load(xmlFilePath);
                        // 获取根元素
                        XElement root = xmlDoc.Root;
                        // 获取FlawPolygonRoiParameter元素
                        XElement rootXMLObject = root.Element("_ItemsData").
                                Element("VisionMaster.ModuleMainWindow.ModuleDialogNew.DeepLearning.FlawPolygonRoiParameter");

                        // 读取并输出各个字段的值
                        rootObject = new hikVT_Seg_RootXMLObject
                        {
                                imgPth = (string)rootXMLObject.Element("_ImagePath"),
                                levelNum = (int)rootXMLObject.Element("_LevelNum"),
                                flags = (string)rootXMLObject.Element("flags"),
                                colorValue = (int)rootXMLObject.Element("_ColorValue"),
                                backgroundColor = (string)rootXMLObject.Element("_BackgroundColor"),
                                opacity = (double)rootXMLObject.Element("_Opacity"),
                                isVisibleLabel = (bool)rootXMLObject.Element("_IsVisibleLabel"),
                                sign = (bool)rootXMLObject.Element("_Sign"),
                                tIsVisible = (bool)rootXMLObject.Element("_TIsVisible"),
                                tOpacity = (double)rootXMLObject.Element("_TOpacity"),
                                // 读取PolygonPoints
                                polygonPoints = rootXMLObject.Element("_PolygonPoints").Elements("HikPcUI.ImageView.PolygonPoint"),
                                isOKCalibrated = (bool)root.Element("_IsOKCalibrated"),
                        };
                        foreach (var point in rootObject.polygonPoints)
                        {
                                double x = (double)point.Element("x");
                                double y = (double)point.Element("y");
                                Console.WriteLine($"PolygonPoint: x={x}, y={y}");
                        }
                }
                /// <summary>
                /// HikVT的XML根对象--一张图会有多个根对象
                /// </summary>
                public class hikVT_Seg_RootXMLObject
                {
                        /// <summary>
                        /// 标注文件所对应图片路径
                        /// </summary>
                        public string imgPth { get; set; }
                        /// <summary>
                        /// --TODO
                        /// </summary>
                        public int levelNum { get; set; } = 2;
                        /// <summary>
                        /// 缺陷标签名
                        /// </summary>
                        public string flags { get; set; }
                        /// <summary>
                        /// 缺陷掩膜灰度值
                        /// </summary>
                        public int colorValue { get; set; }
                        /// <summary>
                        /// 背景色
                        /// </summary>
                        public string backgroundColor { get; set; } = "#FF7B151E";
                        /// <summary>
                        /// 透明度
                        /// </summary>
                        public double opacity { get; set; } = 0.2;
                        /// <summary>
                        /// 标签可见性
                        /// </summary>
                        public bool isVisibleLabel { get; set; } = true;
                        /// <summary>
                        /// 信号TODO
                        /// </summary>
                        public bool sign { get; set; } = true;
                        /// <summary>
                        /// TODO
                        /// </summary>
                        public bool tIsVisible { get; set; } = true;
                        /// <summary>
                        /// TODO
                        /// </summary>
                        public double tOpacity { get; set; } = 0;
                        /// <summary>
                        /// 标签多边形框点
                        /// </summary>
                        public IEnumerable<XElement> polygonPoints { get; set; }
                        /// <summary>
                        /// 
                        /// </summary>
                        public bool isOKCalibrated { get; set; } = false;
                }
        }
}
