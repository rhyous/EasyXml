using System.Text;
using System.Xml.Serialization;

namespace Rhyous.EasyXml
{
    public interface ISerializeToXml
    {
        /// <summary>
        /// This function returns the serialized XML as a string
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="t">The instance of the object.</param>
        /// <param name="inOmitXmlDeclaration"></param>
        /// <param name="inNameSpaces"></param>
        /// <param name="inEncoding"></param>
        string ToXml<T>(T t, bool inOmitXmlDeclaration = false, XmlSerializerNamespaces inNameSpaces = null, Encoding inEncoding = null);

        /// <summary>
        /// This function writes the serialized XML to the file name passed in.
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="t">The instance of the object.</param>
        /// <param name="outFilename">The file name. It can be a full path.</param>
        /// <param name="inOmitXmlDeclaration"></param>
        /// <param name="inNameSpaces"></param>
        /// <param name="inEncoding"></param>
        void ToXml<T>(T t, string outFilename, bool inOmitXmlDeclaration = false, XmlSerializerNamespaces inNameSpaces = null, Encoding inEncoding = null);
    }
}
