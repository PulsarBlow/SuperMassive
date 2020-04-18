namespace SuperMassive
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;

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
                throw new ArgumentNullException(nameof(xml));
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var serializer = new DataContractSerializer(type);
            using var reader = new StringReader(xml);
            XmlReader xmlReader = new XmlTextReader(reader);
            return serializer.ReadObject(xmlReader);
        }

        /// <summary>
        /// Builds an object from its XML representation that can be passed to the DataContractSerializer.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T ReadObject<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentNullException(nameof(xml));

            var objResult = ReadObject(xml, typeof(T));
            var result = (T) objResult;
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
            var objResult = ReadObjectFromFile(fileName, typeof(T));
            var result = (T) objResult;
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
                throw new ArgumentNullException(nameof(fileName));

            if (!File.Exists(fileName))
                throw new FileNotFoundException("File not found", fileName);

            using var reader = new StreamReader(fileName, Encoding.UTF8);
            var xml = reader.ReadToEnd();
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
                throw new ArgumentNullException(nameof(obj));
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
                throw new ArgumentNullException(nameof(type));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var builder = new StringBuilder();
            var serializer = new DataContractSerializer(type);
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true
            };
            using var xWriter = XmlWriter.Create(builder, settings);
            serializer.WriteObject(xWriter, value);
            xWriter.Flush();
            return builder.ToString();
        }

        /// <summary>
        /// Gets the XML representation the given <see cref="NameValueCollection"/>
        /// </summary>
        /// <param name="myCollection"></param>
        /// <param name="rootName"></param>
        /// <param name="namespaceUri"></param>
        /// <returns></returns>
        public static string? WriteObject(NameValueCollection myCollection, string rootName, string namespaceUri)
        {
            var xmlDocument = WriteObjectToXmlDocument(myCollection, rootName, namespaceUri);
            return xmlDocument?.InnerXml;
        }

        /// <summary>
        /// Writes the XML representation from the DataContractSerializer
        /// into the specified filename. If a file at filename
        /// already exists then an Exception will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void WriteObjectToFile<T>(string fileName, [DisallowNull] T obj)
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
                throw new ArgumentNullException(nameof(fileName));

            using var writer = new StreamWriter(fileName, false, Encoding.UTF8);
            writer.Write(WriteObject(type, value));
        }

        /// <summary>
        /// Writes the XML representation of the <see cref="NameValueCollection"/> into a new <see cref="XmlDocument"/> instance.
        /// </summary>
        /// <param name="myCollection"></param>
        /// <param name="rootName"></param>
        /// <param name="namespaceUri"></param>
        /// <returns></returns>
        public static XmlDocument? WriteObjectToXmlDocument(
            NameValueCollection myCollection,
            string rootName,
            string namespaceUri)
        {
            if (string.IsNullOrEmpty(rootName))
                rootName = "xmldocument";
            var xmlDocument = new XmlDocument();
            var xmlRoot =
                xmlDocument.CreateElement(
                    string.Empty,
                    XmlConvert.EncodeName(rootName.ToLowerInvariant()),
                    namespaceUri);

            foreach (string? item in myCollection)
            {
                string s = item ?? string.Empty;
                var xmlElement = xmlDocument.CreateElement(string.Empty, XmlConvert.EncodeName(s.ToLowerInvariant()), namespaceUri);
                var xmlCDataSection = xmlDocument.CreateCDataSection(myCollection[item]);
                xmlElement.AppendChild(xmlCDataSection);
                xmlRoot.AppendChild(xmlElement);
            }

            xmlDocument.AppendChild(xmlRoot);
            return xmlDocument;
        }

        /// <summary>
        /// Writes the XML representation of the <see cref="NameValueCollection"/> into a new <see cref="XDocument"/> instance.
        /// </summary>
        /// <param name="myCollection"></param>
        /// <param name="rootName"></param>
        /// <param name="namespaceUri"></param>
        /// <returns></returns>
        public static XDocument? WriteObjectToXDocument(NameValueCollection myCollection, string rootName,
            string namespaceUri)
        {
            if (myCollection == null)
                return null;
            if (namespaceUri == null)
                namespaceUri = string.Empty;
            rootName = string.IsNullOrEmpty(rootName) ?
                "xdocument" :
                XmlConvert.EncodeName(rootName.ToLowerInvariant());
            XNamespace ns = namespaceUri;
            var xDocument = new XDocument(
                new XElement(ns + rootName));
            foreach (string? item in myCollection)
            {
                string s = item ?? string.Empty;
                xDocument.Element(ns + rootName)
                    ?.Add(
                        new XElement(ns + XmlConvert.EncodeName(s.ToLowerInvariant()),
                        myCollection[item] != null ? new XCData(myCollection[item]) : null));
            }

            return xDocument;
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
                return string.Empty;
            var sb = new StringBuilder();
            using var writer = XmlWriter.Create(sb, settings);
            document.Save(writer);
            return sb.ToString();
        }

        /// <summary>
        /// Returns the string content of the given <see cref="XDocument"/>.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string ToString(XDocument document)
        {
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true
            };
            return ToString(document, settings);
        }

        /// <summary>
        /// Returns the cleaned (no illegal chars) string content of the given <see cref="XDocument"/>.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string? ToCleanString(XDocument document)
        {
            if (document == null) return null;

            var settings = new XmlWriterSettings()
            {
                Encoding = Encoding.UTF8,
                OmitXmlDeclaration = true,
                CheckCharacters = false
            };
            var sb = new StringBuilder();
            using var writer = XmlWriter.Create(sb, settings);
            document.Save(writer);
            return SanitizeXmlString(sb.ToString());
        }

        /// <summary>
        /// Whether a given character is allowed by XML 1.0.
        /// </summary>
        public static bool IsLegalXmlChar(int character)
        {
            return
            (
                character == 0x9 /* == '\t' == 9   */ ||
                character == 0xA /* == '\n' == 10  */ ||
                character == 0xD /* == '\r' == 13  */ ||
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

            foreach (var c in xml.Where(c => IsLegalXmlChar(c)))
            {
                buffer.Append(c);
            }

            return buffer.ToString();
        }
    }
}
