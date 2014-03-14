using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SuperMassive.Helpers
{
    /// <summary>
    /// DataContract and other Xml serialization utilities
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// Builds an object of the specified type from the given XML representation that can be passed to the DataContractSerializer
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ReadObject(string xml, Type type)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException("xml");
            if (type == null)
                throw new ArgumentNullException("type");

            object result = null;
            DataContractSerializer dcs = new DataContractSerializer(type);
            StringReader reader = null;
            try
            {
                reader = new StringReader(xml);
                XmlReader xmlReader = new XmlTextReader(reader);
                result = dcs.ReadObject(xmlReader);
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
            return result;
        }
        /// <summary>
        /// Builds an object from its XML representation that can be passed to the DataContractSerializer.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T ReadObject<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException("xml");

            T result = default(T);

            object objResult = ReadObject(xml, typeof(T));
            if (objResult != null)
            {
                result = (T)objResult;
            }
            return result;
        }
        /// <summary>
        /// Read the XML representation of an object from a file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T ReadObjectFromFile<T>(string fileName)
        {
            T result = default(T);
            object objResult = ReadObjectFromFile(fileName, typeof(T));
            if (objResult != null)
            {
                result = (T)objResult;
            }
            return result;
        }
        /// <summary>
        /// Reads the XML representation of an object from a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <exception cref="FileNotFoundException"></exception>
        /// <returns></returns>
        public static object ReadObjectFromFile(string fileName, Type type)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            if (!File.Exists(fileName))
                throw new FileNotFoundException("fileName");
            string xml = null;
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                xml = reader.ReadToEnd();
                // reader.Close(); CA2202
            }
            if (xml == null)
                return null;
            return ReadObject(xml, type);
        }
        /// <summary>
        /// Gets the XML representation of the given object by using the DataContracSerializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string WriteObject<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            return WriteObject(obj.GetType(), obj);
        }
        /// <summary>
        /// Gets the XML representation of the given object of specified type by using the DataContracSerializer.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string WriteObject(Type type, object value)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (value == null)
                throw new ArgumentNullException("value");

            StringBuilder xmlSerial = new StringBuilder();
            DataContractSerializer dcSerializer = new DataContractSerializer(type);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            using (XmlWriter xWriter = XmlWriter.Create(xmlSerial, settings))
            {
                dcSerializer.WriteObject(xWriter, value);
                xWriter.Flush();
                return xmlSerial.ToString();
            }
        }
        /// <summary>
        /// Gets the XML representation the given <see cref="NameValueCollection"/>
        /// </summary>
        /// <param name="myCollection"></param>
        /// <param name="rootName"></param>
        /// <param name="namespaceUri"></param>
        /// <returns></returns>
        public static string WriteObject(NameValueCollection myCollection, string rootName, string namespaceUri)
        {
            XmlDocument xmlDocument = WriteObjectToXmlDocument(myCollection, rootName, namespaceUri);
            if (xmlDocument == null)
                return null;
            return xmlDocument.InnerXml;
        }
        /// <summary>
        /// Writes the XML representation from the DataContractSerializer
        /// into the specified filename. If a file at filename
        /// already exists then an Exception will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void WriteObjectToFile<T>(string fileName, T obj)
        {
            WriteObjectToFile(fileName, obj.GetType(), obj);
        }
        /// <summary>
        /// Writes the XML representation from the DataContractSerializer
        /// into the specified filename. If a file at filename
        /// already exists then an Exception will be thrown.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public static void WriteObjectToFile(string fileName, Type type, object value)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");
            if (value == null)
                throw new ArgumentNullException("value");

            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                writer.Write(WriteObject(type, value));
            }
        }
        /// <summary>
        /// Writes the XML representation of the <see cref="NameValueCollection"/> into a new <see cref="XmlDocument"/> instance.
        /// </summary>
        /// <param name="myCollection"></param>
        /// <param name="rootName"></param>
        /// <param name="namespaceUri"></param>
        /// <returns></returns>
        public static XmlDocument WriteObjectToXmlDocument(NameValueCollection myCollection, string rootName, string namespaceUri)
        {
            if (myCollection == null)
                return null;
            if (namespaceUri == null)
                namespaceUri = String.Empty;
            if (String.IsNullOrEmpty(rootName))
                rootName = "xmldocument";
            XmlDocument xmldoc = new XmlDocument();
            XmlElement xmlRoot = xmldoc.CreateElement("", XmlConvert.EncodeName(rootName.ToLowerInvariant()), namespaceUri);
            XmlElement xmlElement = null;
            XmlCDataSection xmlCDataSection = null;
            foreach (string s in myCollection)
            {
                xmlElement = xmldoc.CreateElement("", XmlConvert.EncodeName(s.ToLowerInvariant()), namespaceUri);
                xmlCDataSection = xmldoc.CreateCDataSection(myCollection[s]);
                xmlElement.AppendChild(xmlCDataSection);
                xmlRoot.AppendChild(xmlElement);
            }
            xmldoc.AppendChild(xmlRoot);
            return xmldoc;
        }
        /// <summary>
        /// Writes the XML representation of the <see cref="NameValueCollection"/> into a new <see cref="XDocument"/> instance.
        /// </summary>
        /// <param name="myCollection"></param>
        /// <param name="rootName"></param>
        /// <param name="namespaceUri"></param>
        /// <returns></returns>
        public static XDocument WriteObjectToXDocument(NameValueCollection myCollection, string rootName, string namespaceUri)
        {
            if (myCollection == null)
                return null;
            if (namespaceUri == null)
                namespaceUri = String.Empty;
            if (String.IsNullOrEmpty(rootName))
                rootName = "xdocument";
            else
                rootName = XmlConvert.EncodeName(rootName.ToLowerInvariant());
            XNamespace ns = namespaceUri;
            XDocument xdoc = new XDocument(
                /*new XDeclaration("1.0", "utf-8", "yes"),*/
                new XElement(ns + rootName));
            foreach (string s in myCollection)
            {
                xdoc.Element(ns + rootName).Add(
                    new XElement(ns + XmlConvert.EncodeName(s.ToLowerInvariant()),
                        myCollection[s] != null ? new XCData(myCollection[s]) : null));
            }
            return xdoc;
        }
        /// <summary>
        /// Returns the string content of the given <see cref="XDocument"/>.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string ToString(XDocument document, XmlWriterSettings settings)
        {
            if (document == null)
                return null;
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                document.Save(writer);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Returns the string content of the given <see cref="XDocument"/>.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string ToString(XDocument document)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            return ToString(document, settings);
        }
        /// <summary>
        /// Returns the cleaned (no illegal chars) string content of the given <see cref="XDocument"/>.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string ToCleanString(XDocument document)
        {
            if (document == null) return null;

            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true,
                CheckCharacters = false
            };
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                document.Save(writer);
                // writer.Close(); CA2202
            }
            return SanitizeXmlString(sb.ToString());
        }
        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        public static bool IsLegalXmlChar(int character)
        {
            return
            (
                 character == 0x9 /* == '\t' == 9   */        ||
                 character == 0xA /* == '\n' == 10  */        ||
                 character == 0xD /* == '\r' == 13  */        ||
                (character >= 0x20 && character <= 0xD7FF) ||
                (character >= 0xE000 && character <= 0xFFFD) ||
                (character >= 0x10000 && character <= 0x10FFFF)
            );
        }
        /// <summary>
        /// Remove illegal XML characters from a string.
        /// </summary>
        public static string SanitizeXmlString(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return xml;
            }

            var buffer = new StringBuilder(xml.Length);

            foreach (char c in xml)
            {
                if (IsLegalXmlChar(c))
                {
                    buffer.Append(c);
                }
            }

            return buffer.ToString();
        }
    }
}
