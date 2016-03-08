using System.IO;

namespace Rhyous.EasyXml
{
    public sealed class Utf8StreamWriter : StreamWriter
    {
        public Utf8StreamWriter(string file)
            : base(file)
        {
        }

        public override System.Text.Encoding Encoding { get { return System.Text.Encoding.UTF8; } }
    }
}
