using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace musicXml.NoteElements
{
    /// <summary>
    /// 音名を表す列挙体
    /// </summary>
    public enum NoteStep
    {
        [Description("C")]
        C = 0,
        [Description("C#")]
        Cis = 1,
        [Description("D")]
        D = 2,
        [Description("D#")]
        Dis = 3,
        [Description("E")]
        E = 4,
        [Description("F")]
        F = 5,
        [Description("F#")]
        Fis = 6,
        [Description("G")]
        G = 7,
        [Description("G#")]
        Gis = 8,
        [Description("A")]
        A = 9,
        [Description("A#")]
        Ais = 10,
        [Description("B")]
        B = 11
    }
    /// <summary>
    /// 音程を管理するクラス
    /// </summary>
    public class Pitch : IElement
    {
        #region メンバ変数
        private NoteStep notestep;
        private int octave;
        private bool isRest;
        #endregion

        #region プロパティ
        /// <summary>
        /// 休符かどうか
        /// </summary>
        public bool IsRest
        {
            get
            {
                
                return this.isRest;
            }
        }
        /// <summary>
        /// 音名
        /// </summary>
        public NoteStep Notestep
        {
            get
            {
                return this.notestep;
            }
        }
        /// <summary>
        /// オクターブ番号
        /// </summary>
        public int Octave
        {
            get
            {
                return this.octave;
            }
        }
        /// <summary>
        /// midi番号
        /// </summary>
        public int MidiNumber
        {
            get
            {
                return (this.octave + 2) * 12 + (int)this.notestep;
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// 音程を表すクラス
        /// </summary>
        /// <param name="notestep">音名</param>
        /// <param name="octave">オクターブ番号</param>
        public Pitch(NoteStep notestep, int octave)
        {
            this.notestep = notestep;
            this.octave = octave;
            this.isRest = false;
        }
        public Pitch(string noteStep, int octave, int alter)
        {
            this.octave = octave;
            this.isRest = false;
            switch(noteStep)
            {
                case "C":
                    this.notestep = NoteStep.C;
                    break;
                case "D":
                    this.notestep = NoteStep.D;
                    break;
                case "E":
                    this.notestep = NoteStep.E;
                    break;
                case "F":
                    this.notestep = NoteStep.F;
                    break;
                case "G":
                    this.notestep = NoteStep.G;
                    break;
                case "A":
                    this.notestep = NoteStep.A;
                    break;
                case "B":
                    this.notestep = NoteStep.B;
                    break;
            }
            int temp = alter + (int)this.notestep + 12;
            this.notestep = (NoteStep)(temp % 12);
        }

        public Pitch(XElement node)
        {
            string step = node.Element("step").Value;
            int oct = int.Parse(node.Element("octave").Value);
            int alter = 0;
            if(node.Elements("alter").Count() != 0)
            {
                alter = int.Parse(node.Element("alter").Value);
            }
            this.isRest = false;
            switch (step)
            {
                case "C":
                    this.notestep = NoteStep.C;
                    break;
                case "D":
                    this.notestep = NoteStep.D;
                    break;
                case "E":
                    this.notestep = NoteStep.E;
                    break;
                case "F":
                    this.notestep = NoteStep.F;
                    break;
                case "G":
                    this.notestep = NoteStep.G;
                    break;
                case "A":
                    this.notestep = NoteStep.A;
                    break;
                case "B":
                    this.notestep = NoteStep.B;
                    break;
            }
            int temp = alter + (int)this.notestep + 12;
            this.notestep = (NoteStep)(temp % 12);
        }
        /// <summary>
        /// 音程を表すクラス
        /// </summary>
        /// <param name="midinumber">midi番号</param>
        public Pitch(int midinumber)
        {
            this.notestep = (NoteStep)(midinumber % 12);
            this.octave = (midinumber / 12 - 2);
            this.isRest = false;
        }
        /// <summary>
        /// 音程を表すクラス
        /// </summary>
        /// <param name="isRest"></param>
        public Pitch(bool isRest)
        {
            this.isRest = true;
            this.notestep = NoteStep.C;
            this.octave = 0;
        }
        #endregion

        #region 関数
        private int alterNum()
        {
            int result = 0;
            string name = Statics.GetDescription(this.notestep);
            if (name.IndexOf("#") != -1)
            {
                result = 1;
            }
            return result;
        }
        /// <summary>
        /// musicXml用のXmlエレメントを返す
        /// </summary>
        /// <returns></returns>
        public XElement XmlElement()
        {
            if (this.isRest)
            {
                return new XElement("rest");
            }
            else
            {
                XElement result = new XElement("pitch");
                string noteString = Statics.GetDescription(this.notestep)[0].ToString();
                result.Add(new XElement("step", noteString));
                if (this.alterNum() != 0)
                {
                    result.Add(new XElement("alter", this.alterNum().ToString()));
                }
                result.Add(new XElement("octave", this.octave.ToString()));
                return result;
            }
        }
        
        public override string ToString()
        {
            if (this.isRest)
            {
                return "rest";
            }
            else
            {
                return Statics.GetDescription(this.notestep) + this.octave.ToString();
            }
            //return base.ToString();
        }
        #endregion
    }
}
