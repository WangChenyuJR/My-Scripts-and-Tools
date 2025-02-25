using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace DataSets.Format.generate
{
        public class ViMo_Annotation
        {
                /// <summary>
                /// 生成一个ViMo的分割序列化Json标注文件
                /// </summary>
                /// <param name="args"></param>
                public static void Gen_SegAnno_ViMo_JSON(ViMo_Seg_RootJSONObject rootObject)
                {
                        // 序列化为JSON
                        string json = JsonConvert.SerializeObject(rootObject, Newtonsoft.Json.Formatting.Indented);
                        // 写入文件
                        System.IO.File.WriteAllText("output.json", json);
                        Console.WriteLine("JSON文件已生成：output.json");

                }
                /// <summary>
                /// 获取一个ViMo的分割Json标注文件所有信息并反序列化
                /// </summary>
                /// <param name="rootObject"></param>
                public static void Gain_SegAnno_ViMo_JSON(out ViMo_Seg_RootJSONObject rootObject)
                {
                        // 读取JSON文件
                        string jsonFilePath = "output.json"; // JSON文件路径
                        string jsonContent = File.ReadAllText(jsonFilePath);

                        // 反序列化JSON为对象
                        rootObject = JsonConvert.DeserializeObject<ViMo_Seg_RootJSONObject>(jsonContent);
                }
                /// <summary>
                /// ViMoJson的根对象--一张图会有多个根对象
                /// </summary>
                public class ViMo_Seg_RootJSONObject
                {
                        /// <summary>
                        /// 标志--默认空
                        /// </summary>
                        public Dictionary<string, object> Flags { get; set; } = new Dictionary<string, object>();
                        /// <summary>
                        /// 图像数据--默认null
                        /// </summary>
                        public object ImageData { get; set; } = null;
                        /// <summary>
                        /// 图像高度
                        /// </summary>
                        public int ImageHeight { get; set; }
                        /// <summary>
                        /// 图像路径
                        /// </summary>
                        public string ImagePath { get; set; }
                        /// <summary>
                        /// 图像宽度
                        /// </summary>
                        public int ImageWidth { get; set; }
                        /// <summary>
                        /// 图像标签--默认空
                        /// </summary>
                        public List<string> ImageTags { get; set; }
                        /// <summary>
                        /// 图像样本类型--Ng或Ok
                        /// </summary>
                        public bool Ng { get; set; }
                        /// <summary>
                        /// 图像合格参数--默认false
                        /// </summary>
                        public bool Qualified { get; set; } = false;
                        /// <summary>
                        /// 图像标注框形状参数列表
                        /// </summary>
                        public List<Shape> Shapes { get; set; }
                        /// <summary>
                        /// 版本号--默认0.0.1
                        /// </summary>
                        public string Version { get; set; } = "0.0.1";
                }
                /// <summary>
                /// 每个根对象的Shape参数结构体，用于保存标注框的各项参数
                /// </summary>
                public class Shape
                {
                        /// <summary>
                        /// 方向
                        /// </summary>
                        public string ArcDirection { get; set; } = "pass";
                        /// <summary>
                        /// 标志--默认空
                        /// </summary>
                        public Dictionary<string, object> Flags { get; set; }
                        /// <summary>
                        /// 组ID--默认null
                        /// </summary>
                        public object GroupId { get; set; }
                        /// <summary>
                        /// 是否旋转--默认false
                        /// </summary>
                        public bool IsRotate { get; set; }
                        /// <summary>
                        /// 缺陷标签
                        /// </summary>
                        public string Label { get; set; }
                        /// <summary>
                        /// 缺陷标签类型--空
                        /// </summary>
                        public string LabelType { get; set; }
                        /// <summary>
                        /// 标注框线宽
                        /// </summary>
                        public int LineWidth { get; set; }
                        /// <summary>
                        /// 标注框端点坐标列表
                        /// </summary>
                        public List<List<double>> Points { get; set; }
                        /// <summary>
                        /// 标注框类型--一般用多边形
                        /// </summary>
                        public string ShapeType { get; set; } = "polygon";
                }
        }
}