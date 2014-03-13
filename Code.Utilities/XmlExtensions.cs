using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Code.Utilities
{
    public static class XmlExtensions
    {
        /// <summary>
        /// 把 XElement 转换为 XML 格式的字符串
        /// </summary>
        /// <param name="rootElement"></param>
        /// <param name="saveOptions"></param>
        /// <returns></returns>
        public static string ToXmlText(this XElement rootElement, SaveOptions saveOptions)
        {
            XDocument xdoc = new XDocument(rootElement);
            xdoc.Declaration = new XDeclaration("1.0", "gb2312", null);

            MemoryStream htmlStream = new MemoryStream();
            xdoc.Save(htmlStream, saveOptions);

            string htmlText = Encoding.GetEncoding("GB2312").GetString(htmlStream.ToArray());
            htmlStream.Close();

            return htmlText;
        }
    }
}
