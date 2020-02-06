namespace Rhyous.EasyXml
{
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Linq;

    public class Xml
    {
        public Xml(string text)
        {
            Text = text;
        }

        /// <summary>
        /// The original Xml text as is.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// The Xml in a single line (no new lines or carriage returns). The data is trimmed and no more than a single space anywhere.
        /// </summary>
        public string LinearizeXml
        {
            get { return _LinearizeXml ?? (_LinearizeXml = Clean(Document, LinearizedSettings)); }
        } private string _LinearizeXml;

        /// <summary>
        /// And XDocument representation of the Xml. It uses the Linearized Xml not the original text.
        /// </summary>
        public XDocument Document
        {
            get { return _Document ?? (_Document = XDocument.Parse(GetLinearizedXml(Text))); }
        } private XDocument _Document;

        /// <summary>
        /// The Xml with each element properly indented on a separate line. The data is trimmed and no more than a single space anywhere.
        /// </summary>
        public string PrettyXml
        {
            get { return _PrettyXml ?? (_PrettyXml = Clean(Document, PrettySettings)); }
        } private string _PrettyXml;

        /// <summary>
        /// An enum that specifies whether to use UTF-8 or Unicode (UTF-16).
        /// Changing the encoding shouldn't change the original Text but pretty and linearized 
        /// versions of the Xml should change as well as the stream.
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// A method that outputs the Xml as a stream. It outputs using the correct Encoding.
        /// It isn't enough to write encoding="UTF-8" in the Xml declaration if the output file
        /// is still UTF-16. Botht the labeling and the actually bits in the file should match.
        /// </summary>
        /// <returns>A file stream in the configured encoding.</returns>
        public Stream ToStream()
        {
            return new MemoryStream(ToByteArray());
        }

        /// <summary>
        /// This creates a byte array using the correct encoding.
        /// 
        /// Note: Naturally, UTF-8 has half as manay bytes as UTF-16, however,
        /// if UTF-8 is n bytes, UTF-16 will be 2*N+2 bytes. This is because
        /// "UTF-8" is five characters and "UTF-16" is six characters. 
        /// So a 100 byte UTF-8 file would be 202 bytes in UTF-16. 
        /// </summary>
        /// <returns>A byte[] array of the Xml string in the configured encoding.</returns>
        public byte[] ToByteArray()
        {
            return GetEncoding().GetBytes(PrettyXml ?? "");
        }

        /// <summary>
        /// A method to get the current encoding based on the Enum value.
        /// </summary>
        /// <returns>The correct Encoding.</returns>
        private Encoding GetEncoding()
        {
            if (Encoding != null)
                return Encoding;
            string firstLine = null;
            if (Text.StartsWith("<?xml"))
                firstLine = Regex.Match(Text, "<\\?xml[^>]*>")?.Value;
            if (string.IsNullOrWhiteSpace(firstLine))
                return Encoding = Encoding.UTF8;
            var nameValues = firstLine.Split();
            foreach (var nameValue in nameValues)
            {
                if (nameValue.StartsWith("encoding", System.StringComparison.OrdinalIgnoreCase))
                {
                    var pair = nameValue.Split('=');
                    if (pair.Length == 2 && !string.IsNullOrWhiteSpace(pair[1]))
                    {
                        pair[1] = pair[1].Substring(0, pair[1].LastIndexOf('"')).Trim('"');
                        foreach (char c in pair[1])
                        {
                            if (char.IsLetter(c) && char.IsLower(c))
                                return Encoding = Encoding.GetEncoding(pair[1]);
                        }
                        return Encoding = new UpperCaseEncoding(pair[1]);
                    }
                }
            }
            return Encoding.UTF8;
        }

        /// <summary>
        /// XmlWriterSettings for linearized Xml.
        /// </summary>
        private XmlWriterSettings LinearizedSettings
        {
            get
            {
                return new XmlWriterSettings
                {
                    Encoding = GetEncoding(),
                    Indent = false,
                    NewLineOnAttributes = false
                };
            }
        }

        /// <summary>
        /// XmlWriterSettings for Pretty Xml.
        /// </summary>
        private XmlWriterSettings PrettySettings
        {
            get
            {
                return new XmlWriterSettings
                {
                    Encoding = GetEncoding(),
                    Indent = true,
                    IndentChars = string.IsNullOrEmpty(IndentCharacters) ? "  " : IndentCharacters,
                    NewLineOnAttributes = false,
                    NewLineHandling = NewLineHandling.Replace
                };
            }
        }

        /// <summary>
        /// The characters to use for indenting Pretty Xml
        /// </summary>
        public string IndentCharacters { get; set; }

        /// <summary>
        /// The method that uses XDocument to do make clean (pretty or linearized) Xml
        /// </summary>
        /// <param name="doc">The XDcoument version of the Xml.</param>
        /// <param name="settings">The configured XmlWriterSettings.</param>
        /// <returns>A pretty Xml string.</returns>
        private string Clean(XDocument doc, XmlWriterSettings settings)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriterWithEncoding(sb, GetEncoding());
            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                doc.Save(xmlWriter);
                xmlWriter.Flush();
                return sb.ToString();
            }
        }

        /// <summary>
        /// A method that uses Regex to linearize Xml. The regex replaces methods are used.
        /// </summary>
        /// <param name="text">The Xml text</param>
        /// <returns>Linearized Xml string.</returns>
        private string GetLinearizedXml(string text)
        {
            // Replace all white space with a single space
            var halfclean = Regex.Replace(text, @"\s+", " ", RegexOptions.Singleline);

            // Trim after >.
            var clean75 = Regex.Replace(halfclean, @">\s+", ">");

            // Trim before <
            var fullclean = Regex.Replace(clean75, @"\s+<", "<");

            return fullclean;
        }
    }
}