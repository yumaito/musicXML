using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;


namespace musicXml
{
    public class Measure:IElement
    {
        #region メンバ変数
        private int number;
        private double width;
        List<Note> notes;
        #endregion

        #region プロパティ
        /// <summary>
        /// 小節番号
        /// </summary>
        private int Number
        {
            get
            {
                return this.number;
            }
        }
        /// <summary>
        /// 小節の幅
        /// </summary>
        public double Width
        {
            get
            {
                return this.width;
            }
        }
        /// <summary>
        /// 音符リスト
        /// </summary>
        public List<Note> Notes
        {
            get
            {
                return this.notes;
            }
        }
        #endregion

        #region コンストラクタ
        public Measure(XElement node)
        {
            this.number = int.Parse(node.Attribute("number").Value);
            if (node.Attribute("width") != null)
            {
                this.width = Double.Parse(node.Attribute("width").Value);
            }
            this.notes = new List<Note>();
            IEnumerable<XElement> notenodes = node.Elements("note");
            foreach (XElement element in notenodes)
            {
                this.notes.Add(new Note(element, this.number));
            }
        }
        public Measure(List<Note> notes, int number)
        {
            List<Note> tempNotes = notes;
            //this.notes = notes;
            this.number = number;
            this.Optimize(tempNotes);
        }
        #endregion

        #region 関数
        public XElement XmlElement()
        {
            XElement result = new XElement("measure");
            result.Add(new XAttribute("number", this.number));
            if (this.width != 0.0)
            {
                result.Add(new XAttribute("width", this.width));
            }
            foreach(Note note in this.notes)
            {
                result.Add(note.XmlElement());
            }
            return result;
        }
        /// <summary>
        /// 基本音符のみで構成するように最適化
        /// </summary>
        private void Optimize(List<Note> dividedNotes)
        {
            //（タイストップでまとめる）====================================
            List<List<Note>> notesOfNote = new List<List<Note>>();
            List<Note> t = new List<Note>();
            for (int i = 0; i < dividedNotes.Count; i++)
            {
                t.Add(dividedNotes[i]);
                if ((dividedNotes[i].Notation.Tie == NoteElements.tied.stop) ||
                    (i == dividedNotes.Count - 1))
                {
                    //現在参照している音符のタイが「ストップ」or「最後の音符」なら
                    notesOfNote.Add(t);
                    t = new List<Note>();
                }
            }
            //=====================================
            foreach (List<Note> onenote in notesOfNote)
            {

            }
        }

        private List<Note> OneNoteOptimize(List<Note> n)
        {
            List<Note> result = new List<Note>();
            //int l = 0;
            //for (int i = Note.BasicNote.Length - 1; i >= 0; i++)
            //{
            //    if (result.Count - Note.BasicNote[i] > 0)
            //    {
            //        l = i;
            //        result.Add(n[0].Clone(Note.BasicNote[i]));
            //        //残りの音符
            //        int s = Note.BasicNote[i] - 1;
            //        //最後までの数
            //        int c = n.Count - s;
            //        result.AddRange(this.OneNoteOptimize(n.GetRange(s, c)));
            //        break;
            //    }
            //    if (result.Count - Note.BasicNote[i] == 0)
            //    {
            //        l = i;
            //        result.Add(n[0].Clone(Note.BasicNote[i]));
            //        break;
            //    }
            //}
            //
            //result.Add(n[0].Clone(Note.BasicNote[l]));
            return result;
        }
        #endregion
    }
}