using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicXml
{
    public abstract class interfaceBase
    {
        #region メンバ変数
        protected PartClass[] partName;
        protected List<Track> track;
        protected Identification identification;
        #endregion

        #region プロパティ
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
        /// このmusicXMLを作成したソフトなどの情報
        /// </summary>
        public Identification Identification
        {
            get
            {
                return this.identification;
            }
        }
        #endregion
    }
}
