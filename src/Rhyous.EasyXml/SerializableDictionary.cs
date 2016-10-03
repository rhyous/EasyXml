using System.Collections.Generic;
using System.Xml.Serialization;

namespace Rhyous.EasyXml
{
    [XmlRoot("Dictionary")]
    public class SerializableDictionary<TKey, TValue>
        : Dictionary<TKey, TValue>, IXmlSerializable
    {
        public string KeyName = "key";
        public string ValueName = "value";

        #region constructors
        public SerializableDictionary()
        {
        }

        public SerializableDictionary(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }
        #endregion
        
        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            var keySerializer = new XmlSerializer(typeof(TKey), null, null, new XmlRootAttribute(KeyName), "");
            var valueSerializer = new XmlSerializer(typeof(TValue), null, null, new XmlRootAttribute(ValueName), "");
            var wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                var key = (TKey)keySerializer.Deserialize(reader);
                var value = (TValue)valueSerializer.Deserialize(reader);
                Add(key, value);
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            var keySerializer = new XmlSerializer(typeof(TKey), new XmlRootAttribute(KeyName));
            var valueSerializer = new XmlSerializer(typeof(TValue), new XmlRootAttribute(ValueName));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            foreach (TKey key in Keys)
            {
                keySerializer.Serialize(writer, key, ns);
                valueSerializer.Serialize(writer, this[key], ns);
            }
        }
        #endregion
    }
}