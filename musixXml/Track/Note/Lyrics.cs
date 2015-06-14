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
    public enum Syllabic
    {
        begin,
        middle,
        end,
        single
    }
    public class Lyrics : IElement
    {
        #region メンバ変数
        private string lyrics;
        private Syllabic syllabic;
        private int number;
        #endregion

        #region プロパティ
        public Syllabic SyllabicValue
        {
            get
            {
                return this.syllabic;
            }
        }
        #endregion

        #region コンストラクタ
        public Lyrics(string lyrics, Syllabic syllabic, int number)
        {
            this.lyrics = lyrics;
            this.syllabic = syllabic;
            this.number = number;
        }
        #endregion

        #region 関数
        public XElement XmlElement()
        {
            XElement result = new XElement("lyric");
            result.Add(new XAttribute("number", this.number.ToString()));
            result.Add(new XElement("syllabic", this.syllabic));
            result.Add(new XElement("text", this.lyrics));
            return result;
        }
        public void ChangeSyllabic(Syllabic syllabic)
        {
            this.syllabic = syllabic;
        }
        #endregion
    }
}
