namespace Rhyous.EasyXml
{
    public interface IDeserializeFromXml
    {
        /// <summary>
        /// This function deserializes the XML file passed in.
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="inFilename">The file or full path to the file.</param>
        /// <returns>The object that was deserialized from xml.</returns>
        T FromXml<T>(string inFilename);

        /// <summary>
        /// This function deserializes the XML string passed in.
        /// </summary>
        /// <typeparam name="T">The object type to serialize.</typeparam>
        /// <param name="inString">The string containing the XML.</param>
        /// <returns>The object that was deserialized from xml.</returns>
        T FromXml<T>(ref string inString);
    }
}
