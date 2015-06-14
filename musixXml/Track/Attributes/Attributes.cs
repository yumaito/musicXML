using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using musicXml.AttributeElements;

namespace musicXml
{
    public class Attributes
    {
        #region メンバ変数
        private int division;
        private Key key;
        private BeatInfo beatInfo;
        private List<Clef> clefInfo;
        private StaffDetails staffDetails;
        #endregion

        #region プロパティ
        /// <summary>
        /// 楽譜詳細（TABのチューニングなど）
        /// </summary>
        public StaffDetails StaffDetails
        {
            get
            {
                return this.staffDetails;
            }
        }
        /// <summary>
        /// 記号情報（ト音記号、ヘ音記号、TABなど）
        /// </summary>
        public List<Clef> ClefInfo
        {
            get
            {
                return this.clefInfo;
            }
        }
        /// <summary>
        /// 拍子情報
        /// </summary>
        private BeatInfo BeatInfo
        {
            get
            {
                return this.beatInfo;
            }
        }
        /// <summary>
        /// 1拍の長さ
        /// </summary>
        public int Division
        {
            get
            {
                return this.division;
            }
        }
        /// <summary>
        /// 調合情報
        /// </summary>
        public Key Key
        {
            get
            {
                return this.key;
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// 楽譜の属性情報
        /// </summary>
        /// <param name="node"></param>
        public Attributes(XElement node)
        {
            //division
            this.division = int.Parse(node.Element("divisions").Value);
            //
            this.key = new Key(node.Element("key"));
            //
            this.beatInfo = new BeatInfo(node.Element("time"));
            //
            this.clefInfo = new List<Clef>();
            IEnumerable<XElement> clefs = node.Elements("clef");
            foreach(XElement c in clefs)
            {
                this.clefInfo.Add(new Clef(c));
            }
            //
            if (node.Elements("staff-details").Count() != 0)
            {
                this.staffDetails = new StaffDetails(node.Element("staff-details"));
            }
        }
        #endregion

        #region 関数
        #endregion 
    }
}
