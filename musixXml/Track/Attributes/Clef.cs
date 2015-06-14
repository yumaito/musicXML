using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace musicXml.AttributeElements
{
    public class Clef
    {
        #region メンバ変数
        private ClefType cleftype;
        private int line;
        private int octaveShift;
        private int number;
        #endregion

        #region プロパティ
        /// <summary>
        /// 楽譜番号
        /// </summary>
        public int Number
        {
            get
            {
                return this.number;
            }
        }
        /// <summary>
        /// 記号（ト音記号、ヘ音記号、TABなど）
        /// </summary>
        public ClefType ClefType
        {
            get
            {
                return this.cleftype;
            }
        }
        /// <summary>
        /// ライン情報
        /// </summary>
        public int Line
        {
            get
            {
                return this.line;
            }
        }
        /// <summary>
        /// オクターブシフト
        /// </summary>
        public int OctaveShift
        {
            get
            {
                return this.octaveShift;
            }
        }
        #endregion

        #region コンストラクタ
        public Clef(XElement node)
        {
            if(node.HasAttributes)
            {
                this.number = int.Parse(node.Attribute("number").Value);
            }
            switch(node.Element("sign").Value)
            {
                case "G":
                    this.cleftype = musicXml.ClefType.G2;
                    break;
                case "F":
                    this.cleftype = musicXml.ClefType.F4;
                    break;
                case "TAB":
                    this.cleftype = musicXml.ClefType.TAB;
                    break;
                default:
                    break;
            }
            this.line = int.Parse(node.Element("line").Value);
            if(node.Elements("clef-octave-change").Count() != 0)
            {
                this.octaveShift = int.Parse(node.Element("clef-octave-change").Value);
            }
            
        }
        #endregion

        #region 関数
        #endregion
    }
}
