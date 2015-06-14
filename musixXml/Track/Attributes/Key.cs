using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace musicXml.AttributeElements
{
    public enum Mode
    {
        Major,Minor
    }
    public class Key
    {
        #region メンバ変数
        private int fifths;
        private Mode mode;
        #endregion

        #region プロパティ
        /// <summary>
        /// 調合記号（正ならシャープの数、負ならフラットの数）
        /// </summary>
        public int Fifths
        {
            get
            {
                return this.fifths;
            }
        }
        /// <summary>
        /// モード（メジャーorマイナー）
        /// </summary>
        public Mode Mode
        {
            get
            {
                return this.mode;
            }
        }
        #endregion

        #region コンストラクタ
        public Key(XElement node)
        {
            this.fifths = int.Parse(node.Element("fifths").Value);
            //Modeノードがあるかチェック
            if(node.Elements("mode").Count() != 0)
            {
                string temp = node.Element("mode").Value;
                switch(temp)
                {
                    case "major":
                        this.mode = AttributeElements.Mode.Major;
                        break;
                    case "minor":
                        this.mode = AttributeElements.Mode.Minor;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region 関数
        #endregion
    }
}
