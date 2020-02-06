using System.IO;
using System.Text;

namespace Rhyous.EasyXml
{
    public sealed class StreamWriterWithEncoding : StreamWriter
    {
        private readonly Encoding _Encoding;
        public StreamWriterWithEncoding(string file, Encoding encoding)
            : base(file) 
            => _Encoding = encoding;

        public override Encoding Encoding => _Encoding;
    }
}
