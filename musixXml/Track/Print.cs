using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using musicXml.NoteElements;
using musicXml.AttributeElements;

namespace musicXml
{
    public class Print
    {
        #region メンバ変数
        private bool isTop;
        private double leftMargin;
        private double rightMargin;
        private double topSystemDistance;
        private List<double> staffDistance;
        private List<int> number;
        #endregion

        #region プロパティ
        /// <summary>
        /// 楽譜番号
        /// </summary>
        public List<int> Number
        {
            get
            {
                return this.number;
            }
        }
        /// <summary>
        /// 上の楽譜からの距離（isTopがfalseのとき有効）
        /// </summary>
        public List<double> StaffDistance
        {
            get
            {
                return this.staffDistance;
            }
        }
        /// <summary>
        /// 楽譜の一番上からの距離（isTopがtrueのとき有効）
        /// </summary>
        public double TopSystemDistance
        {
            get
            {
                return this.topSystemDistance;
            }
        }
        /// <summary>
        /// 右側のマージン（isTopがtrueのとき有効）
        /// </summary>
        public double RightMargin
        {
            get
            {
                return this.rightMargin;
            }
        }
        /// <summary>
        /// 左側のマージン（isTopがtrueのとき有効）
        /// </summary>
        public double LeftMargin
        {
            get
            {
                return this.leftMargin;
            }
        }
        /// <summary>
        /// 一番上の譜面かどうか
        /// </summary>
        public bool IsTop
        {
            get
            {
                return this.isTop;
            }
        }
        #endregion

        #region コンストラクタ
        public Print(XElement node)
        {
            //system-layoutタグが1個以上存在するかどうか
            if(node.Elements("system-layout").Count() != 0)
            {
                this.isTop = true;
                this.ReturnSytemLayout(node);
            }
            else
            {
                this.isTop = false;
                this.ReturnStaffLayout(node);
            }
            
        }
        #endregion

        #region 関数
        private void ReturnSytemLayout(XElement node)
        {
            XElement element = node.Element("system-layout");
            //
            XElement margins = element.Element("system-margins");
            this.leftMargin = double.Parse(margins.Element("left-margin").Value);
            this.rightMargin = double.Parse(margins.Element("right-margin").Value);
            //
            this.topSystemDistance = double.Parse(element.Element("top-system-distance").Value);
        }
        private void ReturnStaffLayout(XElement node)
        {
            IEnumerable<XElement> staffs = node.Elements("staff-layout");
            this.number = new List<int>();
            this.staffDistance = new List<double>();
            foreach(XElement staff in staffs)
            {
                number.Add(int.Parse(staff.Attribute("number").Value));
                staffDistance.Add(double.Parse(staff.Element("staff-distance").Value));
            }
        }
        #endregion
    }
}
