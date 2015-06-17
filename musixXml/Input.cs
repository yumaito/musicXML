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
    public class InputMusicXml
    {
        #region メンバ変数
        private PartClass[] partName;
        private List<Track> track;
        private string title;
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
        /// パートネームリスト
        /// </summary>
        public PartClass[] PartName
        {
            get
            {
                return this.partName;
            }
        }
        /// <summary>
        /// 音符リスト
        /// </summary>
        public List<Track> Track
        {
            get
            {
                return this.track;
            }
        }
        /// <summary>
        /// タイトル
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }   
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// musicXMLを読み込む（ファイルパス）
        /// </summary>
        /// <param name="path">ファイルの場所</param>
        public InputMusicXml(string path)
        {
            XDocument xmlDoc = XDocument.Load(path);
            IEnumerable<XElement> titleQuery = xmlDoc.Root.Descendants("work");
            this.ReturnTitle(titleQuery);
            //
            IEnumerable<XElement> idQ = xmlDoc.Root.Descendants("identification");

            //
            IEnumerable<XElement> pathQuery = xmlDoc.Root.Descendants("part-list");
            this.ReturnPart(pathQuery);
            //
            IEnumerable<XElement> partQuery = xmlDoc.Root.Descendants("part");
            this.ReturnTrack(partQuery);
        }
        #endregion

        #region 関数
        private void ReturnID(IEnumerable<XElement> q)
        {
            if (q.Count() != 0)
            {
                //要素が1個以上存在するなら
                //encodingノードを取得
            }
        }
        private void ReturnTitle(IEnumerable<XElement> q)
        {
            if(q.Count() != 0)
            {
                //要素が1個以上存在するなら
                IEnumerable<XElement> node = q.Elements("work-title");
                foreach (XElement n in node)
                {
                    this.title = n.Value;
                }
            }
            else
            {
                //要素が全く存在しないなら＝workノードが存在しない
                this.title = "no title";
            }
        }
        private void ReturnPart(IEnumerable<XElement> q)
        {
            try
            {
                IEnumerable<XElement> node = q.Elements("score-part");
                List<PartClass> temp = new List<PartClass>();
                foreach(XElement n in node)
                {
                    temp.Add(new PartClass(n));
                }
                this.partName = temp.ToArray();
            }
            catch(Exception ex)
            {

            }
        }
        private void ReturnTrack(IEnumerable<XElement> q)
        {
            if(q.Count() != 0)
            {
                track = new List<Track>();
                foreach(XElement node in q)
                {
                    track.Add(new Track(node));
                }
            }
        }
        #endregion
    }
}
