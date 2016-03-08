using System.Text;

namespace Rhyous.EasyXml
{
    /// <summary>
    /// This class makes the UTF-16 text in the Xml declaration show up capitalized.
    /// </summary>
    public sealed class XmlUnicode : UnicodeEncoding
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

        public static XmlUnicode Instance
        {
            get { return _XmlUnicode ?? (_XmlUnicode = new XmlUnicode()); }
        }
        private static XmlUnicode _XmlUnicode;
    }
}