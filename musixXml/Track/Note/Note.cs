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
using System.Drawing;

namespace musicXml
{
    public enum NoteType
    {
        /// <summary>
        /// 全音符
        /// </summary>
        [Description("whole")]
        whole,
        /// <summary>
        /// ２分音符
        /// </summary>
        [Description("half")]
        half,
        /// <summary>
        /// ４分音符
        /// </summary>
        [Description("quarter")]
        quarter,
        /// <summary>
        /// ８分音符
        /// </summary>
        [Description("eighth")]
        eighth,
        /// <summary>
        /// 16分音符
        /// </summary>
        [Description("16th")]
        sixteenth
    }
    
    /// <summary>
    /// 1つの音符を表すクラス
    /// </summary>
    public class Note : IElement
    {
        #region メンバ変数
        private Pitch pitch;
        private Notation notation;
        private int voiceNum;
        private int duration;
        private NoteType notetype;
        private Lyrics lyrics;
        private int dotNum;
        private int measure;
        private Point position;
        #region static
        private static int division;
        private static int[] basicNote = new int[] { 1, 2, 3, 4, 6, 8, 12, 16 };
        #endregion
        #endregion

        #region プロパティ
        /// <summary>
        /// 音程
        /// </summary>
        public Pitch Pitch
        {
            get
            {
                return this.pitch;
            }
        }
        /// <summary>
        /// 記号類（現在スラーとタイにのみ対応）
        /// </summary>
        public Notation Notation
        {
            get
            {
                return this.notation;
            }
        }
        /// <summary>
        /// ボーカル番号
        /// </summary>
        public int VoiceNum
        {
            get
            {
                return this.voiceNum;
            }
        }
        /// <summary>
        /// 音符の種類（4分音符など）
        /// </summary>
        public NoteType Notetype
        {
            get
            {
                return this.notetype;
            }
        }
    
        /// <summary>
        /// 歌詞
        /// </summary>
        public Lyrics Lyrics
        {
            get
            {
                return this.lyrics;
            }
        }
        /// <summary>
        /// 付点の数
        /// </summary>
        public int DotNum
        {
            get
            {
                return this.dotNum;
            }
        }
        /// <summary>
        /// Division（1拍の長さの定義）
        /// </summary>
        public static int Division
        {
            get
            {
                return Note.division;
            }
            set
            {
                Note.Division = value;
            }
        }
        public static int[] BasicNote
        {
            get
            {
                return Note.basicNote;
            }
        }
        /// <summary>
        /// 小節番号
        /// </summary>
        public int Measure
        {
            get
            {
                return this.measure;
            }
        }
        /// <summary>
        /// 音符の長さ
        /// </summary>
        public int Duration
        {
            get
            {
                return this.duration;
            }
        }
        /// <summary>
        /// 音符座標に対応しているかどうか
        /// </summary>
        public bool isPosition
        {
            get
            {
                if(this.position !=null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 音符座標
        /// </summary>
        public Point Position
        {
            get
            {
                return this.position;
            }
        }
        #endregion

        #region コンストラクタ
        public Note(Pitch pitch, Notation notation, int duration, Lyrics lyrics)
        {
            this.pitch = pitch;
            this.notation = notation;
            this.duration = duration;
            this.voiceNum = 1;
            this.lyrics = lyrics;
            this.LengthToType();
            
        }
        public Note(Pitch pitch, Notation notation, int duration, Lyrics lyrics, int measure)
        {
            this.pitch = pitch;
            this.notation = notation;
            this.duration = duration;
            this.measure = measure;
            this.lyrics = lyrics;
            this.voiceNum = 1;
            this.LengthToType();
        }
        public Note(Pitch pitch, Notation notation, int voiceNum,
            int duration, NoteType noteType, Lyrics lyrics, int dotNum, int measure)
        {
            this.pitch = pitch;
            this.notation = notation;
            this.duration = duration;
            this.voiceNum = voiceNum;
            this.measure = measure;
            this.lyrics = lyrics;
            this.LengthToType();
        }
        public Note(XElement node, int measure)
        {
            this.measure = measure;
            //pitch or rest
            if(node.Elements("pitch").Count() != 0)
            {
                this.pitch = new Pitch(node.Element("pitch"));
            }
            if(node.Elements("rest").Count()!=0)
            {
                this.pitch = new Pitch(true);
            }
            //
            this.duration = int.Parse(node.Element("duration").Value);
            //
            this.notetype = this.ReturnType(node.Element("type").Value);
            //
            if(node.Elements("notations").Count() !=0)
            {
                this.notation = new Notation(node.Element("notations"));
            }
            if ((node.Attribute("default-x") != null) && (node.Attribute("default-y")!=null))
            {
                double x = Double.Parse(node.Attribute("default-x").Value);
                double y = Double.Parse(node.Attribute("default-y").Value);
                this.position = new Point((int)x,(int)y);
            }
        }
        #endregion

        #region 関数
        public override string ToString()
        {
            return this.pitch.ToString() + ":" + Statics.GetDescription(this.notetype) + ":" + this.duration;
            //return base.ToString();
        }
        public XElement XmlElement()
        {
            //this.LengthToType();
            XElement result = new XElement("note");
            result.Add(this.pitch.XmlElement());
            if(!this.pitch.IsRest)
            {
                //休符でないなら歌詞を加える
            }
            result.Add(new XElement("duration", this.duration.ToString()));
            if (this.notation.Tied() != null)
            {
                result.Add(this.notation.Tied());
            }
            result.Add(new XElement("type", Statics.GetDescription(this.notetype)));
            result.Add(new XElement("voice", this.voiceNum.ToString()));
            if (this.dotNum != 0)
            {
                for (int i = 0; i < dotNum; i++)
                {
                    result.Add(new XElement("dot"));
                }
            }
            result.Add(this.notation.XmlElement());
            return result;
        }

        public Note[] Divide()
        {
            Note[] result = new Note[this.duration];
            Lyrics temp = this.lyrics;
            for (int i = 0; i < result.Length; i++)
            {
                Notation n = new Notation(tied.center, slur.none);
                result[i] = new Note(this.pitch, n, 1, new Lyrics("-", Syllabic.middle, 1));
            }
            result[0].notation.TieChange(0);
            if ((temp.SyllabicValue == Syllabic.begin) || (temp.SyllabicValue == Syllabic.single))
            {
                //もともとbeginかsingleであれば先頭をbegin（それ以外はmiddleになる）
                result[0].ChangeSyllabic(temp.SyllabicValue);
            }
            result[result.Length - 1].notation.TieChange(1);
            if (temp.SyllabicValue != Syllabic.middle)
            {
                //もともとmiddleでないならendにする（それ以外はmiddleになる）
                result[result.Length - 1].ChangeSyllabic(Syllabic.end);
            }
            if(result.Length == 1)
            {
                result[0].notation.TieChange(-1);
            }
            return result;
        }

        //object ICloneable.IClone()
        //{
        //    return Clone();
        //}
        public Note Clone()
        {
            return new Note(this.pitch, this.notation, this.duration, this.lyrics, this.measure);
        }
        /// <summary>
        /// 長さnで複製する
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Note Clone(int n)
        {
            return new Note(this.pitch, this.notation, n, this.lyrics, this.measure);
        }
        public void ChangeSyllabic(Syllabic syllabic)
        {
            this.lyrics.ChangeSyllabic(syllabic);
        }
        private void LengthToType()
        {
            switch (this.duration)
            {
                case 1:
                    this.notetype = NoteType.sixteenth;
                    this.dotNum = 0;
                    break;
                case 2:
                    this.notetype = NoteType.eighth;
                    this.dotNum = 0;
                    break;
                case 3:
                    this.notetype = NoteType.eighth;
                    this.dotNum = 1;
                    break;
                case 4:
                    this.notetype = NoteType.quarter;
                    this.dotNum = 0;
                    break;
                case 6:
                    this.notetype = NoteType.quarter;
                    this.dotNum = 1;
                    break;
                case 8:
                    this.notetype = NoteType.half;
                    this.dotNum = 0;
                    break;
                case 12:
                    this.notetype = NoteType.half;
                    this.dotNum = 1;
                    break;
                case 16:
                    this.notetype = NoteType.whole;
                    this.dotNum = 0;
                    break;
                default:
                    this.notetype = NoteType.sixteenth;
                    this.dotNum = 0;
                    break;
            }
        }

        private NoteType ReturnType(string str)
        {
            switch(str)
            {
                case "whole":
                    return NoteType.whole;
                case "half":
                    return NoteType.half;
                case "quarter":
                    return NoteType.quarter;
                case "eighth":
                    return NoteType.eighth;
                case "16th":
                    return NoteType.sixteenth;
                default:
                    return NoteType.quarter;
            }
        }
        #endregion
    }
}