using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.ComponentModel;
using musicXml.NoteElements;
using musicXml.AttributeElements;
using musicXml;

namespace musicXml
{
    public class OutputMusicXml : interfaceBase
    {
        #region メンバ変数
        private List<List<Note>> notes;
        //private PartClass[] partName;
        //private List<Track> tracks;
        #endregion

        #region プロパティ
        #endregion

        #region コンストラクタ
        /// <summary>
        /// musicXmlを出力するクラス
        /// 4分の4拍子にのみ対応
        /// </summary>
        /// <param name="path">保存する場所</param>
        /// <param name="tracks">トラック（各ノートはdivisionを4とする）</param>
        /// <param name="partName">パートリスト</param>
        /// <param name="title">タイトル</param>
        /// <param name="generater">生成するソフトの名称</param>
        public OutputMusicXml(string path, List<Track> tracks, PartClass[] partName, string title, string generater)
        {
            if (notes.Count != partName.Length)
            {
                //パートネームの数と音符リストの数が異なる場合例外を返す
                throw new ArgumentException("音符リストの数とパートネームの数が異なります！");
            }
            //.notes = notes;
            this.partName = partName;
            this.identification = new musicXml.Identification(generater, DateTime.Now);
            this.tracks = tracks;
            //1拍の長さを4に設定
            Note.Division = 4;
            //for (int i = 0; i < partName.Length; i++)
            //{
            //    this.tracks.Add(new Track(notes[i], partName[i], ClefType.G2));
            //}
            this.SaveFile(path, title);
        }
        #endregion

        #region 関数
        /// <summary>
        /// musicXMLを書き込む
        /// </summary>
        /// <param name="path">ファイルを保存する場所</param>
        /// <param name="title">曲のタイトル</param>
        /// <param name="generater">このソフトウェアの名前</param>
        private void SaveFile(string path, string title)
        {
            XDocument document = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XDocumentType("score-partwise", "-//Recordare//DTD MusicXML 2.0 Partwise//EN",
                        "http://www.musicxml.org/dtds/partwise.dtd", null)
                );
            document.Add(new XComment(title));
            document.Add(new XElement("score-partwise"));

            #region Noteノード前（ヘッダ部分）

            #region work
            XElement work = new XElement("work");
            work.Add(new XElement("work-title", title));
            document.Root.Add(work);
            #endregion

            #region identification
            //identificationノード
            document.Root.Add(this.identification.XmlElement());
            //XElement identification = new XElement("identification");
            ////encodingノード
            //XElement enc = new XElement("encoding");
            //enc.Add(new XElement("software", generater));
            //DateTime dt = DateTime.Now;
            //enc.Add(new XElement("encoding-date", dt.Year.ToString() + "-" + dt.Month.ToString()
            //    + "-" + dt.Day.ToString()));
            //identification.Add(enc);
            //document.Root.Add(identification);
            #endregion

            #region defaults
            //scalingノード
            XElement defaults = new XElement("defaults");
            XElement scaling = new XElement("scaling");
            scaling.Add(new XElement("millimeters", "7.05556"));
            scaling.Add(new XElement("tenths", "40"));
            defaults.Add(scaling);
            //page-layoutノード
            XElement pageLayout = new XElement("page-layout");
            pageLayout.Add(new XElement("page-height", "1683.73"));
            pageLayout.Add(new XElement("page-width", "1190.55"));
            //
            XElement pageMarginE = new XElement("page-margins");
            pageMarginE.Add(new XAttribute("type", "even"));
            pageMarginE.Add(new XElement("left-margin", "56.6969"));
            pageMarginE.Add(new XElement("right-margin", "56.6969"));
            pageMarginE.Add(new XElement("top-margin", "56.6969"));
            pageMarginE.Add(new XElement("bottom-margin", "113.386"));
            pageLayout.Add(pageMarginE);
            //
            XElement pageMarginO = new XElement("page-marings");
            pageMarginO.Add(new XAttribute("type", "odd"));
            pageMarginO.Add(new XElement("left-margin", "56.6969"));
            pageMarginO.Add(new XElement("right-margin", "56.6969"));
            pageMarginO.Add(new XElement("top-margin", "56.6969"));
            pageMarginO.Add(new XElement("bottom-margin", "113.386"));
            pageLayout.Add(pageMarginO);
            //
            defaults.Add(pageLayout);
            document.Root.Add(defaults);
            #endregion

            #region credit
            XElement credit = new XElement("credit");
            credit.Add(new XAttribute("page", "1"));
            XElement creditWords = new XElement("credit-words", title);
            creditWords.Add(new XAttribute("valign", "top"));
            creditWords.Add(new XAttribute("justify", "center"));
            creditWords.Add(new XAttribute("font-size", "24"));
            creditWords.Add(new XAttribute("default-y", "1627.09"));
            creditWords.Add(new XAttribute("default-x", "595.276"));
            credit.Add(creditWords);
            //
            document.Root.Add(credit);
            #endregion

            #region part-list
            XElement part = new XElement("part-list");
            foreach (PartClass str in this.partName)
            {
                part.Add(str.XmlElement());
            }
            document.Root.Add(part);
            #endregion

            #endregion

            //==========================================//
            for (int i = 0; i < this.tracks.Count; i++)
            {
                if (i == 0)
                {
                    document.Root.Add(tracks[i].XmlElement(true));
                }
                else
                {
                    document.Root.Add(tracks[i].XmlElement(false));
                }
            }
            //foreach (Track track in this.tracks)
            //{
            //    document.Root.Add(track.XmlElement());
            //}
            //==========================================//
            document.Save(path);
            //document.Add
        }
        #endregion
    }
}
