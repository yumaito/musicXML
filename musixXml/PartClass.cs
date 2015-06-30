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

namespace musicXml
{
    public class PartClass : IElement
    {
        #region メンバ変数
        private string name;
        private string shortenName;
        private int midiProgram;
        private int channel;
        private string id;
        #endregion

        #region プロパティ
        /// <summary>
        /// 楽器名
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }
        /// <summary>
        /// 楽器の省略名
        /// </summary>
        public string ShortName
        {
            get
            {
                return this.shortenName;
            }
        }
        /// <summary>
        /// MIDIプログラム番号
        /// </summary>
        public int MidiProgram
        {
            get
            {
                return this.midiProgram;
            }
        }
        /// <summary>
        /// チャンネル番号
        /// </summary>
        public int Channel
        {
            get
            {
                return this.channel;
            }
        }
        /// <summary>
        /// Id属性値
        /// </summary>
        public string ID
        {
            get
            {
                return this.id;
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// 1つのパートを表すクラス
        /// </summary>
        /// <param name="id">パートID（P1など）</param>
        /// <param name="name">名前（Pianoなど）</param>
        /// <param name="shortenName">省略名（Pi.など）</param>
        /// <param name="midiProgram"></param>
        /// <param name="channel"></param>
        public PartClass(string id, string name, string shortenName, int midiProgram, int channel)
        {
            this.id = id;
            this.name = name;
            this.shortenName = shortenName;
            this.midiProgram = midiProgram;
            this.channel = channel;
        }
        /// <summary>
        /// 1つのパートを表すクラス
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="midiProgram"></param>
        /// <param name="channel"></param>
        public PartClass(string id, string name, int midiProgram, int channel)
        {
            this.id = id;
            this.name = name;
            this.shortenName = name.Substring(0, 2);
            this.midiProgram = midiProgram;
            this.channel = channel;
        }
        /// <summary>
        /// デフォルト値を設定（ファイル出力時に使える）
        /// </summary>
        /// <param name="id"></param>
        public PartClass(string id)
            : this(id, "Piano", "P.", 1, 1)
        {
        }
        public PartClass(XElement node)
        {
            this.id = node.Attribute("id").Value;
            this.name = node.Element("part-name").Value;
            if (node.Elements("part-abbreviation").Count() != 0)
            {
                this.shortenName = node.Element("part-abbreviation").Value;
            }
            XElement midi = node.Element("midi-instrument");
            this.channel = int.Parse(midi.Element("midi-channel").Value);
            this.midiProgram = int.Parse(midi.Element("midi-program").Value);
        }
        #endregion

        #region 関数
        public XElement XmlElement()
        {
            XElement element = new XElement("score-part");
            element.Add(new XAttribute("id", this.id));
            //
            element.Add("part-name", this.name);
            element.Add("part-abbreviation", this.shortenName);
            //
            string tempName = this.id + "-I1";
            XElement ins = new XElement("score-instrument");
            ins.Add(new XAttribute("id", tempName));
            ins.Add(new XElement("instrument-name", this.name));
            element.Add(ins);
            //
            XElement midiDevice = new XElement("midi-device");
            midiDevice.Add(new XAttribute("id", tempName));
            midiDevice.Add(new XAttribute("port", "1"));
            element.Add(midiDevice);
            //
            XElement midiIns = new XElement("midi-instrument");
            midiIns.Add(new XAttribute("id", tempName));
            midiIns.Add(new XElement("midi-channel", this.channel.ToString()));
            midiIns.Add(new XElement("midi-program", this.midiProgram.ToString()));
            midiIns.Add(new XElement("volume", "57"));
            midiIns.Add(new XElement("pan", "0"));
            element.Add(midiIns);
            //
            return element;
        }
        /// <summary>
        /// 小節のヘッダ部分を返す
        /// </summary>
        /// <param name="isTopIns">楽譜上で最も最初の楽器かどうか</param>
        /// <returns></returns>
        public XElement PrintLayout(bool isTopIns)
        {
            XElement print = new XElement("print");
            XElement layout = new XElement("system-layout");
            if (isTopIns)
            {
                XElement margin = new XElement("system-margins");
                margin.Add(new XElement("left-margin", "112"));
                margin.Add(new XElement("right-margin", "0.00"));
                layout.Add(margin);
                layout.Add(new XElement("top-system-distance", "170.00"));
                print.Add(layout);
            }
            else
            {
                layout.Add(new XAttribute("number", "1"));
                layout.Add(new XElement("staff-distance", "65.00"));
                print.Add(layout);
            }
            return print;
        }
        #endregion
    }
}
