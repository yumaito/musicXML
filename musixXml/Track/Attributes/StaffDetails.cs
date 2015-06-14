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
    public class StaffDetails
    {
        #region メンバ変数
        private int lines;
        private List<musicXml.NoteElements.Pitch> tuning;
        #endregion

        #region プロパティ
        /// <summary>
        /// 横線の本数
        /// </summary>
        public int Lines
        {
            get
            {
                return this.lines;
            }
        }
        /// <summary>
        /// チューニング（0番=最も手前の弦（ベースなら0番=4弦、1番=3弦…））
        /// </summary>
        public List<musicXml.NoteElements.Pitch> Tuning
        {
            get
            {
                return this.tuning;
            }
        }
        #endregion

        #region コンストラクタ
        public StaffDetails(XElement node)
        {
            this.lines = int.Parse(node.Element("staff-lines").Value);
            IEnumerable<XElement> tune = node.Elements("staff-tuning");
            this.tuning = new List<NoteElements.Pitch>();
            foreach(XElement t in tune)
            {
                string step = t.Element("tuning-step").Value;
                int oct = int.Parse(t.Element("tuning-octave").Value);
                this.tuning.Add(new NoteElements.Pitch(step, oct, 0));
            }
        }
        #endregion

        #region 関数
        #endregion
    }
}
