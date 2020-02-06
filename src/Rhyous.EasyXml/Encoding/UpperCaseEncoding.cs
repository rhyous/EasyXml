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

        public override bool IsSingleByte => _Encoding.IsSingleByte;
        public override bool IsMailNewsSave => _Encoding.IsMailNewsSave;
        public override bool IsMailNewsDisplay => _Encoding.IsMailNewsDisplay;
        public override bool IsBrowserSave => _Encoding.IsBrowserSave;
        public override bool IsBrowserDisplay => _Encoding.IsBrowserDisplay;
        public override int WindowsCodePage => _Encoding.WindowsCodePage;
        public override string WebName => _Encoding.WebName.ToUpper();
        public override string HeaderName => _Encoding.HeaderName;
        public override string EncodingName => _Encoding.EncodingName;
        public override string BodyName => _Encoding.BodyName;
        public override int CodePage => _Encoding.CodePage;

        public override object Clone() => _Encoding.Clone();
        public override bool Equals(object value) => _Encoding.Equals(value);
        public override int GetByteCount(char[] chars) => _Encoding.GetByteCount(chars);
        public override int GetByteCount(string s) => _Encoding.GetByteCount(s);
        public override unsafe int GetByteCount(char* chars, int count) => _Encoding.GetByteCount(chars, count);
        public override unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount) 
               => _Encoding.GetBytes(chars, charCount, bytes, byteCount);
        public override byte[] GetBytes(char[] chars) => _Encoding.GetBytes(chars);
        public override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
               => _Encoding.GetBytes(s, charIndex, charCount, bytes, byteIndex);
        public override byte[] GetBytes(string s) => _Encoding.GetBytes(s);
        public override byte[] GetBytes(char[] chars, int index, int count) 
               => _Encoding.GetBytes(chars, index, count);
        public override unsafe int GetCharCount(byte* bytes, int count) => _Encoding.GetCharCount(bytes, count);
        public override int GetCharCount(byte[] bytes) => _Encoding.GetCharCount(bytes);
        public override unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount) 
               => _Encoding.GetChars(bytes, byteCount, chars, charCount);
        public override char[] GetChars(byte[] bytes, int index, int count) 
               => _Encoding.GetChars(bytes, index, count);
        public override char[] GetChars(byte[] bytes) => _Encoding.GetChars(bytes);
        public override Decoder GetDecoder() => _Encoding.GetDecoder();
        public override Encoder GetEncoder() => _Encoding.GetEncoder();
        public override int GetHashCode() => _Encoding.GetHashCode();
        public override byte[] GetPreamble() => _Encoding.GetPreamble();
        public override string GetString(byte[] bytes) => _Encoding.GetString(bytes);
        public override string GetString(byte[] bytes, int index, int count) 
               => _Encoding.GetString(bytes, index, count);
        public override bool IsAlwaysNormalized(NormalizationForm form) 
               => _Encoding.IsAlwaysNormalized(form);
    }
}
