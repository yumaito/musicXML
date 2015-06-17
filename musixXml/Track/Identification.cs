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
    public class Identification
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
        #endregion

        #region 関数
        #endregion
    }
}
