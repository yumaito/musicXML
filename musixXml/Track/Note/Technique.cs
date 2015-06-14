using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;


namespace musicXml.NoteElements
{
    public class Technique
    {
        #region メンバ変数
        private int stringNum;
        private int fretNum;
        #endregion

        #region プロパティ
        /// <summary>
        /// 弦番号
        /// </summary>
        public int StringNum
        {
            get
            {
                return this.stringNum;
            }
        }
        /// <summary>
        /// フレット番号
        /// </summary>
        public int FretNum
        {
            get
            {
                return this.fretNum;
            }
        }
        #endregion

        #region コンストラクタ
        public Technique(XElement node)
        {
            this.stringNum = int.Parse(node.Element("string").Value);
            this.fretNum = int.Parse(node.Element("fret").Value);
        }
        #endregion

        #region 関数
        #endregion
    }
}
