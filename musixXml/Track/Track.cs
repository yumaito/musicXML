using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.ComponentModel;
using musicXml.AttributeElements;
using musicXml.NoteElements;

namespace musicXml
{
    public enum ClefType
    {
        G2,F4,TAB
    }
    public class Track : IElement
    {
        #region メンバ変数
        private List<Note> notes;
        private List<List<Note>> notesByMeasure;
        private PartClass partClass;
        private ClefType clefType;
        private int tempo;
        //
        private Print print;
        private Attributes attributes;
        private Identification identification;
        #endregion

        #region プロパティ
        /// <summary>
        /// このmusicXMLを作成したソフトなどの情報
        /// </summary>
        public Identification Identification
        {
            get
            {
                return this.Identification;
            }
        }
        /// <summary>
        /// テンポ
        /// </summary>
        public int Tempo
        {
            get
            {
                return this.tempo;
            }
        }
        /// <summary>
        /// トラックの属性情報
        /// </summary>
        public Attributes Attributes
        {
            get
            {
                return this.attributes;
            }
        }
        /// <summary>
        /// 楽譜のプリント情報
        /// </summary>
        public Print Print
        {
            get
            {
                return this.print;
            }
        }
        /// <summary>
        /// 音符列
        /// </summary>
        public List<Note> Notes
        {
            get
            {
                return this.notes;
            }
        }
        /// <summary>
        /// パート情報
        /// </summary>
        public PartClass Part
        {
            get
            {
                return this.partClass;
            }
        }
        #endregion

        #region コンストラクタ
        public Track(List<Note> notes, PartClass part,ClefType clefType)
        {
            this.notes = notes;
            this.partClass = part;
            this.clefType = clefType;
        }
        public Track(XElement node)
        {
            //プリント情報
            IEnumerable<XElement> printNodes = node.Descendants("print");
            if (printNodes.Count() != 0)
            {
                this.print = new Print(printNodes.ElementAt(0));
            }
            //
            //属性情報
            IEnumerable<XElement> attributeNode = node.Descendants("attributes");
            this.attributes = new Attributes(attributeNode.ElementAt(0));
            //テンポ情報
            if(node.Elements("sound").Count() != 0)
            {
                this.tempo = int.Parse(node.Attribute("tempo").Value);
            }
            //
            //音符情報
            IEnumerable<XElement> noteNode = node.Descendants("note");
            this.notes = new List<Note>();
            foreach(XElement nn in noteNode)
            {
                //小節番号を取得
                int measureNum = int.Parse(nn.Parent.Attribute("number").Value);
                this.notes.Add(new Note(nn, measureNum));
            }
        }
        #endregion

        #region 関数
        /// <summary>
        /// XMLのエレメントを返す関数（多分使わない）
        /// </summary>
        /// <returns></returns>
        public XElement XmlElement()
        {
            XElement result = new XElement("part");
            
            return result;
        }
        public XElement XmlElement(bool isFirst)
        {
            XElement result = new XElement("part");

            //result.Add(this.partClass.PrintLayout(isFirst));
            //小節番号でループ
            for (int i = 0; i < notesByMeasure.Count; i++)
            {
                XElement measure = new XElement("measure");
                measure.Add(new XAttribute("number", (i + 1).ToString()));
                if (i == 0)
                {
                    //最初の小節だけはプリント要素とアトリビュート要素を加える
                    measure.Add(this.partClass.PrintLayout(isFirst));
                    measure.Add(this.Attribute());
                }
                //現在の小節のノートを加える処理
                //======================================
                for (int j = 0; j < this.notesByMeasure[i].Count; j++)
                {
                    measure.Add(this.notesByMeasure[i][j].XmlElement());
                }
                //======================================
                //現在の小節をパートに加える処理
                result.Add(measure);
            }
            return result;
        }
        private XElement Attribute()
        {
            XElement result = new XElement("attributes");
            //
            result.Add(new XElement("divisions",Note.Division));
            //
            XElement key = new XElement("key");
            key.Add(new XElement("fifths", "0"));
            result.Add(key);
            //
            XElement time = new XElement("time");
            time.Add(new XElement("beats", "4"));
            time.Add(new XElement("beat-type", "4"));
            result.Add(time);
            //
            XElement clef = new XElement("clef");
            switch(this.clefType)
            {
                case ClefType.G2:
                    clef.Add(new XElement("sign", "G"));
                    clef.Add(new XElement("line", "2"));
                    break;
                case ClefType.F4:
                    clef.Add(new XElement("sign", "F"));
                    clef.Add(new XElement("line", "4"));
                    break;
                default:
                    break;
            }
            result.Add(clef);
            //
            return result;
        }
        //音符列を小節ごとに再編成し直す処理
        private void ReturnByMeasure(XElement node)
        {
            //IEnumerable<XElement> measure = node.Elements("measure");

        }
        #endregion
    }
}
