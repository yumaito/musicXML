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
    public class Identification : IElement
    {
        #region メンバ変数
        private string softwareName;
        private DateTime date;
        #endregion

        #region プロパティ
        /// <summary>
        /// このmusicXMLを作成したソフトの名前
        /// </summary>
        public string SoftwareName
        {
            get
            {
                return this.softwareName;
            }
        }
        /// <summary>
        /// このmusicXMLが作成された日付
        /// </summary>
        public DateTime EncodingDate
        {
            get
            {
                return this.date;
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// 入力時のコンストラクタ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="date"></param>
        public Identification(string name, string date)
        {
            this.softwareName = name;
            if (!String.IsNullOrWhiteSpace(date))
            {
                //dateが空白orNullでないなら日付データを取得
                this.date = DateTime.ParseExact(date, "yyyy-MM-dd",
                    System.Globalization.DateTimeFormatInfo.InvariantInfo,
                    System.Globalization.DateTimeStyles.None);
            }
        }
        public Identification(string name, DateTime dt)
        {
            this.softwareName = name;
            this.date = dt;
        }
        #endregion

        #region 関数
        public XElement XmlElement()
        {
            XElement result = new XElement("identification");
            XElement enc = new XElement("encoding");
            enc.Add(new XElement("software", this.softwareName));
            enc.Add(new XElement("encoding-date", date.Year.ToString() + "-" + date.Month.ToString()
                + "-" + date.Day.ToString()));
            result.Add(enc);
            return result;
        }
        #endregion
    }
}
