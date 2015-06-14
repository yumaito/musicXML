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
    public class BeatInfo
    {
        #region メンバ変数
        private int beats;
        private int beatType;
        #endregion

        #region プロパティ
        /// <summary>
        /// 分子
        /// </summary>
        public int Beats
        {
            get
            {
                return this.beats;
            }
        }
        /// <summary>
        /// 分母
        /// </summary>
        public int BeatType
        {
            get
            {
                return this.beatType;
            }
        }
        #endregion

        #region コンストラクタ
        public BeatInfo(XElement node)
        {
            this.beats = int.Parse(node.Element("beats").Value);
            this.beatType = int.Parse(node.Element("beat-type").Value);
        }
        #endregion

        #region 関数
        #endregion
    }
}
