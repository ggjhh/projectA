using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MVC4cjlee.Utility
{
    /// <summary>
    /// Xml의 요약 설명입니다.
    /// </summary>
    public class Xml
    {
        /// <summary>
        /// XML String to Object Mapper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T ToObject<T>(string xml)
        {
            T res;
            using (XmlReader xr = XmlReader.Create(new StringReader(xml)))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                res = (T)xs.Deserialize(xr);
            }
            return res;
        }

        /// <summary>
        /// Object To XML String generator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToXmlString<T>(T obj)
        {
            string res;
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings xset = new XmlWriterSettings();
            xset.OmitXmlDeclaration = true;
            using (XmlWriter xw = XmlWriter.Create(new StringWriter(sb), xset))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                XmlSerializer xs = new XmlSerializer(typeof(T));
                xs.Serialize(xw, obj, ns);
                res = sb.ToString();
            }
            return res;
        }
    }
}