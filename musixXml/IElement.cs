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
    public interface IElement
    {
        XElement XmlElement();
        //bool isOutputEnable();
    }
}
