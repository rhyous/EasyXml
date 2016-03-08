using System.IO;

namespace Rhyous.EasyXml
{
    public sealed class Utf8StringWriter : StringWriter
    {
        public override System.Text.Encoding Encoding { get { return System.Text.Encoding.UTF8; } }
    }
}
