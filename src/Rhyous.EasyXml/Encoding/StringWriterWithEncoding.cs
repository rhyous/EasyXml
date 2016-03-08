using System.IO;
using System.Text;

namespace Rhyous.EasyXml
{
    /// <summary>
    /// This class allows for the Xml to be created with a specific 
    /// </summary>
    public sealed class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding _Encoding;

        public StringWriterWithEncoding(StringBuilder builder, Encoding encoding)
            : base(builder)
        {
            _Encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return _Encoding; }
        }
    }
}