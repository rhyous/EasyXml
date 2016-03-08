using System.Text;

namespace Rhyous.EasyXml
{
    /// <summary>
    /// This class makes the UTF-8 text in the Xml declaration show up capitalized.
    /// </summary>
    public sealed class XmlUTF8Encoding : UTF8Encoding
    {
        public override string WebName
        {
            get { return base.WebName.ToUpper(); }
        }

        public override string HeaderName
        {
            get { return base.HeaderName.ToUpper(); }
        }

        public override string BodyName
        {
            get { return base.BodyName.ToUpper(); }
        }

        public static XmlUTF8Encoding Instance
        {
            get { return _XmlUTF8Encoding ?? (_XmlUTF8Encoding = new XmlUTF8Encoding()); }
        }
        private static XmlUTF8Encoding _XmlUTF8Encoding;
    }
}