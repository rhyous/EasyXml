using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Rhyous.EasyXml.Tests
{
    [TestClass]
    public class EncodingTests
    {
        [TestMethod]
        public void Encoding_GetEncoding_Same_as_EncodingDotUTF8()
        {
            Assert.AreEqual(Encoding.GetEncoding("UTF-8"), Encoding.UTF8);
        }

        [TestMethod]
        public void Encoding_GetEncoding_Same_as_EncodingDotUtf8()
        {
            Assert.AreEqual(Encoding.GetEncoding("Utf-8"), Encoding.UTF8);
        }

        [TestMethod]
        public void Encoding_GetEncoding_Same_as_EncodingDotUtf8_Byte()
        {
            var text = "Hello, world.";
            var bytes = Encoding.UTF8.GetBytes(text);
            var reText = Encoding.UTF8.GetString(bytes);
            Assert.AreEqual(text, reText);
        }



        [TestMethod]
        public void Encoding_GetEncoding_Same_as_EncodingDotUtf8_ByteUpperCase()
        {
            var text = "Hello, world.";
            var bytes = new UpperCaseEncoding(Encoding.UTF8).GetBytes(text);
            var reText = Encoding.UTF8.GetString(bytes);
            Assert.AreEqual(text, reText);
        }
    }
}
