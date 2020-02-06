using System.Text;

namespace Rhyous.EasyXml
{
    /// <summary>
    /// This allows for the encoding name that appears in the first line of the Xml to be uppercase.
    /// </summary>
    /// <remarks>While specifications suggest case shouldn't matter, it does matter for some implementations.</remarks>
    public class UpperCaseEncoding : Encoding
    {
        private readonly Encoding _Encoding;
        public UpperCaseEncoding(Encoding encoding) => _Encoding = encoding;
        public UpperCaseEncoding(string encoding) => _Encoding = GetEncoding(encoding);

        public override string WebName
        {
            get { return _Encoding.WebName.ToUpper(); }
        }

        public override int GetByteCount(char[] chars, int index, int count) 
               => _Encoding.GetByteCount(chars, index, count);

        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
               => _Encoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);

        public override int GetCharCount(byte[] bytes, int index, int count)
               => _Encoding.GetCharCount(bytes, index, count);

        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
               => _Encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);

        public override int GetMaxByteCount(int charCount)
               => _Encoding.GetMaxByteCount(charCount);

        public override int GetMaxCharCount(int byteCount)
               => _Encoding.GetMaxCharCount(byteCount);
    }
}
