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
    /// <summary>
    /// タイ
    /// </summary>
    public enum tied
    {
        /// <summary>
        /// タイのはじまり
        /// </summary>
        start,
        /// <summary>
        /// タイの終わり
        /// </summary>
        stop,
        /// <summary>
        /// タイがかかっている途中
        /// </summary>
        center,
        /// <summary>
        /// タイ無し
        /// </summary>
        none
    }
    public enum slur
    {
        start,
        stop,
        none
    }
    /// <summary>
    /// notationを管理するクラス
    /// </summary>
    public class Notation : IElement
    {
        #region メンバ変数
        private tied tie;
        private slur slu;
        private Technique tech;
        private List<tied> ties;
        #endregion

        #region プロパティ
        /// <summary>
        /// タイ
        /// </summary>
        public List<tied> Ties
        {
            get
            {
                return this.ties;
            }
        }
        /// <summary>
        /// フレット＆弦情報
        /// </summary>
        public Technique Tech
        {
            get
            {
                return this.tech;
            }
        }
        /// <summary>
        /// タイのパラメータ
        /// </summary>
        public tied Tie
        {
            get
            {
                return this.tie;
            }
        }
        /// <summary>
        /// スラーのパラメータ
        /// </summary>
        public slur Slur
        {
            get
            {
                return this.slu;
            }
        }
        #endregion

        #region コンストラクタ
        public Notation(tied tie, slur slu)
        {
            this.tie = tie;
            this.slu = slu;
        }
        public Notation(XElement node)
        {
            if(node.Elements("technical").Count() != 0)
            {
                this.tech = new Technique(node.Element("technical"));
            }
            if(node.Elements("tied").Count() != 0)
            {
                this.ties = new List<tied>();
                IEnumerable<XElement> tt = node.Elements("tied");
                foreach(XElement t in tt)
                {
                    string temp = t.Attribute("type").Value;
                    switch(temp)
                    {
                        case "start":
                            this.ties.Add(tied.start);
                            break;
                        case "stop":
                            this.ties.Add(tied.stop);
                            break;
                        default:
                            break;
                    }
                    //this.ties.Add(t.Attribute("type"))
                }
            }
        }
        #endregion

        #region 関数
        public XElement XmlElement()
        {
            XElement notations = new XElement("notations");
            switch (this.tie)
            {
                case tied.start:
                    XElement tieElement = new XElement("tied");
                    tieElement.Add(new XAttribute("type","start"));
                    notations.Add(tieElement);
                    break;
                case tied.stop:
                    XElement tieStop = new XElement("tied");
                    tieStop.Add(new XAttribute("type", "stop"));
                    notations.Add(tieStop);
                    break;
                case tied.center:
                    XElement tieCenter = new XElement("tied");
                    tieCenter.Add(new XAttribute("type", "stop"));
                    tieCenter.Add(new XAttribute("type", "start"));
                    notations.Add(tieCenter);
                    break;
                default:
                    break;
            }
            switch (this.slu)
            {
                case slur.start:
                    XElement sStart = new XElement("slur");
                    sStart.Add(new XAttribute("type", "start"));
                    notations.Add(sStart);
                    break;
                case slur.stop:
                    XElement sStop = new XElement("slur");
                    sStop.Add(new XAttribute("type", "stop"));
                    notations.Add(sStop);
                    break;
                default:
                    break;
            }
            return notations;
        }
        public XElement Tied()
        {

            switch (this.tie)
            {
                case tied.start:
                    XElement tieElement = new XElement("tied");
                    tieElement.Add(new XAttribute("type", "start"));
                    return tieElement;
                case tied.stop:
                    XElement tieStop = new XElement("tied");
                    tieStop.Add(new XAttribute("type", "stop"));
                    return tieStop;
                case tied.center:
                    XElement tieCenter = new XElement("tied");
                    tieCenter.Add(new XAttribute("type", "stop"));
                    tieCenter.Add(new XAttribute("type", "start"));
                    return tieCenter;
                default:
                    return null;
            }
        }
        public void TieChange(int n)
        {
            switch (n)
            {
                case -1:
                    this.tie = tied.none;
                    break;
                case 0:
                    this.tie = tied.start;
                    break;
                case 1:
                    this.tie = tied.stop;
                    break;
                case 2:
                    this.tie = tied.center;
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
