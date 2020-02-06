namespace Rhyous.EasyXml
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Xml;
    using System.Xml.Serialization;

    public class Serializer : IEasyXmlSerializer
    {
        #region Singleton
        public static IEasyXmlSerializer Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_Instance == null)
                            return (_Instance = new Serializer());
                    }
                }
                return _Instance;
            }
            set { _Instance = value; } // Available for dependency injection
        }

        private static volatile IEasyXmlSerializer _Instance;
        private static object syncRoot = new Object();

        private Serializer()
        {
        }
        #endregion

        #region Functions

        /// <inheritdoc />
        public void ToXml<T>(T t, string outFilename, bool inOmitXmlDeclaration = false, XmlSerializerNamespaces inNameSpaces = null, Encoding inEncoding = null, bool useDefaultNamespaces = false)
        {
            MakeDirectoryPath(outFilename);
            var ns = inNameSpaces;
            if (ns == null && !useDefaultNamespaces)
            {
                ns = new XmlSerializerNamespaces();
                ns.Add("", "");
            }
            var serializer = new XmlSerializer(t.GetType());
            TextWriter textWriter = (inEncoding == null)
                                  ? new StreamWriter(outFilename)
                                  : new StreamWriterWithEncoding(outFilename, inEncoding);
            var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings { OmitXmlDeclaration = inOmitXmlDeclaration });
            serializer.Serialize(xmlWriter, t, ns);
            textWriter.Close();
        }

        public static void SerializeToXml<T>(T t, string outFilename, bool inOmitXmlDeclaration = true, XmlSerializerNamespaces inNameSpaces = null, Encoding inEncoding = null, bool useDefaultNamespaces = false)
        {
            Instance.ToXml(t, outFilename, inOmitXmlDeclaration, inNameSpaces, inEncoding);
        }

        public static void SerializeToXml<T>(T t, string outFilename, bool inOmitXmlDeclaration, bool useDefaultNamespaces, Encoding inEncoding = null)
        {
            Instance.ToXml(t, outFilename, inOmitXmlDeclaration, null, inEncoding, useDefaultNamespaces);
        }

        private static void MakeDirectoryPath(string outFilename)
        {
            var dir = Path.GetDirectoryName(outFilename);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        /// <inheritdoc />
        public string ToXml<T>(T t, bool inOmitXmlDeclaration = false, XmlSerializerNamespaces inNameSpaces = null, Encoding inEncoding = null, bool useDefaultNamespaces = false)
        {
            var ns = inNameSpaces;
            if (ns == null && !useDefaultNamespaces)
            {
                ns = new XmlSerializerNamespaces();
                ns.Add("", "");
            }
            var serializer = new XmlSerializer(t.GetType());
            TextWriter textWriter = inEncoding == null ? new StringWriter() : new StringWriterWithEncoding(inEncoding);
            var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings { OmitXmlDeclaration = inOmitXmlDeclaration});
            serializer.Serialize(xmlWriter, t, ns);
            return textWriter.ToString();
        }

        public static string SerializeToXml<T>(T t, bool inOmitXmlDeclaration = false, XmlSerializerNamespaces inNameSpaces = null, Encoding inEncoding = null, bool useDefaultNamespaces = false)
        {
            return Instance.ToXml(t, inOmitXmlDeclaration, inNameSpaces, inEncoding, useDefaultNamespaces);
        }

        /// <inheritdoc />
        public T FromXml<T>(String inFilename)
        {
            if (string.IsNullOrWhiteSpace(inFilename))
            {
                return default(T);
            }
            // Wait 1 second if file doesn't exist, in case we are waiting on a
            // separate thread and beat it here.
            if (!File.Exists(inFilename))
                Thread.Sleep(1000);

            // File should exist by now.
            if (File.Exists(inFilename))
            {
                var deserializer = new XmlSerializer(typeof(T));
                var textReader = (TextReader)new StreamReader(inFilename);
                var reader = new XmlTextReader(textReader);
                reader.Read();
                var retVal = (T)deserializer.Deserialize(reader);
                textReader.Close();
                return retVal;
            }
            throw new FileNotFoundException(inFilename);
        }

        public static T DeserializeFromXml<T>(string inFilename)
        {
            return Instance.FromXml<T>(inFilename);
        }

        /// <inheritdoc />
        public T FromXml<T>(ref string inString)
        {
            if (string.IsNullOrWhiteSpace(inString))
            {
                return default(T);
            }
            var deserializer = new XmlSerializer(typeof(T));
            var textReader = (TextReader)new StringReader(inString);
            var retVal = (T)deserializer.Deserialize(textReader);
            textReader.Close();
            return retVal;
        }

        public static T DeserializeFromXml<T>(ref string inString)
        {
            return Instance.FromXml<T>(ref inString);
        }

        #endregion
    }
}