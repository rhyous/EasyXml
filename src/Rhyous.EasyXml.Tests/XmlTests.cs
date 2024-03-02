using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace Rhyous.EasyXml.Tests
{
    [TestClass]
    public class XmlTests
    {
        public string LinearUtf8Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Person><FirstName>John</FirstName><MiddleName>Al Leon</MiddleName><LastName>Doe</LastName></Person>";
        public string PrettyUtf8Xml = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>{Environment.NewLine}<Person>{Environment.NewLine}  <FirstName>John</FirstName>{Environment.NewLine}  <MiddleName>Al Leon</MiddleName>{Environment.NewLine}  <LastName>Doe</LastName>{Environment.NewLine}</Person>";
        public string PrettyUtf8XmlWithTabs = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>{Environment.NewLine}<Person>{Environment.NewLine}\t<FirstName>John</FirstName>{Environment.NewLine}\t<MiddleName>Al Leon</MiddleName>{Environment.NewLine}\t<LastName>Doe</LastName>{Environment.NewLine}</Person>";
        public string UglyUtf8Xml = $"<?xml version=\"1.0\"{Environment.NewLine}encoding=\"UTF-8\"?>{Environment.NewLine}<Person>{Environment.NewLine}{Environment.NewLine}<FirstName>{Environment.NewLine}    John{Environment.NewLine}        </FirstName>{Environment.NewLine}{Environment.NewLine}<MiddleName>{Environment.NewLine}    Al{Environment.NewLine}    Leon{Environment.NewLine}                </MiddleName>{Environment.NewLine}  <LastName>{Environment.NewLine}    {Environment.NewLine}{Environment.NewLine}{Environment.NewLine}Doe{Environment.NewLine}        </LastName>{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}</Person>";
        public string LinearUtf16Xml = $"<?xml version=\"1.0\" encoding=\"UTF-16\"?><Person><FirstName>John</FirstName><MiddleName>Al Leon</MiddleName><LastName>Doe</LastName></Person>";
        public string PrettyUtf16Xml = $"<?xml version=\"1.0\" encoding=\"UTF-16\"?>{Environment.NewLine}<Person>{Environment.NewLine}  <FirstName>John</FirstName>{Environment.NewLine}  <MiddleName>Al Leon</MiddleName>{Environment.NewLine}  <LastName>Doe</LastName>{Environment.NewLine}</Person>";
        public string UglyUtf16Xml = $"<?xml version=\"1.0\"{Environment.NewLine}encoding=\"UTF-16\"?>{Environment.NewLine}<Person>{Environment.NewLine}{Environment.NewLine}<FirstName>{Environment.NewLine}    John{Environment.NewLine}        </FirstName>{Environment.NewLine}{Environment.NewLine}<MiddleName>{Environment.NewLine}    Al{Environment.NewLine}    Leon{Environment.NewLine}                </MiddleName>{Environment.NewLine}  <LastName>{Environment.NewLine}    {Environment.NewLine}{Environment.NewLine}{Environment.NewLine}Doe{Environment.NewLine}        </LastName>{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}</Person>";

        [TestMethod]
        public void Xml_Linearize()
        {
            // Arrange
            Xml xml = new Xml(PrettyUtf8Xml);

            // Act
            var actual = xml.LinearizeXml;

            // Assert
            actual.ShouldEqualWithDiff(LinearUtf8Xml);
        }

        [TestMethod]
        public void Xml_Pretty()
        {
            // Arrange
            Xml xml = new Xml(LinearUtf8Xml);

            // Act
            var actual = xml.PrettyXml;

            // Assert
            actual.ShouldEqualWithDiff(PrettyUtf8Xml);
        }

        [TestMethod]
        public void Xml_Stream()
        {
            // Arrange
            Xml xml = new Xml(LinearUtf8Xml);

            // Act
            var actual = xml.ToStream();

            // Assert
            Assert.AreEqual(xml.PrettyXml, Encoding.UTF8.GetString(((MemoryStream)actual).ToArray()));
        }

        [TestMethod]
        public void Xml_LinearizeUgly()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf8Xml);

            // Act
            var actual = xml.LinearizeXml;

            // Assert
            actual.ShouldEqualWithDiff(LinearUtf8Xml);
        }

        [TestMethod]
        public void Xml_MakeUglyPretty()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf8Xml);

            // Act
            var actual = xml.PrettyXml;

            // Assert
            actual.ShouldEqualWithDiff(PrettyUtf8Xml);
        }

        [TestMethod]
        public void Xml_LinearizeUglyUtf16()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf16Xml)
            {
                Encoding = new UpperCaseEncoding("UTF-16")
            };

            // Act
            var actual = xml.LinearizeXml;

            // Assert
            actual.ShouldEqualWithDiff(LinearUtf16Xml);
        }

        [TestMethod]
        public void Xml_MakeUglyPrettyUtf16()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf16Xml)
            {
                Encoding = new UpperCaseEncoding("UTF-16")
            };

            // Act
            var actual = xml.PrettyXml;

            // Assert
            actual.ShouldEqualWithDiff(PrettyUtf16Xml);
        }

        [TestMethod]
        public void Xml_StreamIsUtf8()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf8Xml)
            {
                Encoding = new UpperCaseEncoding("UTF-8")
            };

            // Act
            var actual = xml.ToStream();
            using (var memoryStream = new MemoryStream())
            {
                actual.CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();
                // Assert
                Assert.AreEqual(154, bytes.Length);
            }
        }

        [TestMethod]
        public void Xml_StreamIsUtf16()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf16Xml)
            {
                Encoding = new UpperCaseEncoding("UTF-16")
            };

            // Act
            var actual = xml.ToStream();
            using (var memoryStream = new MemoryStream())
            {
                actual.CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();

                // Assert
                // 310 is twice the size of 154, 308, but add 2 bytes because
                // UTF-8 is 5 characters but UTF-16 is 6 characters so it is 
                // one character longer.John
                Assert.AreEqual(310, bytes.Length);
            }
        }

        [TestMethod]
        public void Xml_PrettyWithTabs()
        {
            // Arrange
            Xml xml = new Xml(LinearUtf8Xml);
            xml.PrettySettings.IndentChars = "\t";

            // Act
            var actual = xml.PrettyXml;

            // Assert
            actual.ShouldEqualWithDiff(PrettyUtf8XmlWithTabs);
        }

        [TestMethod]
        public void Xml_PrettyWithTabs_SetWithLegacyProperty()
        {
            // Arrange
            Xml xml = new Xml(LinearUtf8Xml);
            xml.IndentCharacters = "\t";

            // Act
            var actual = xml.PrettyXml;

            // Assert
            actual.ShouldEqualWithDiff(PrettyUtf8XmlWithTabs);
        }

        [TestMethod]
        public void Xml_StreamUtf8IsDifferentThanStreamUtf16()
        {
            const string text = "Hello, world!";

            var utf8 = Encoding.UTF8.GetBytes(text);
            var utf16 = Encoding.Unicode.GetBytes(text);

            Assert.AreNotEqual(utf8.Length, utf16.Length);
        }

        [TestMethod]
        public void UseDefaultNamespacesStatic_NoFirstLineXmlTag_Tests()
        {
            // Arrange
            var xmltext = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><A><Id>1</Id><Name>A1</Name><Bs><B><Id>1</Id><Name>B1</Name></B><B><Id>1</Id><Name>B2</Name></B></Bs></A>";
            var expectedXml = $"<A>{Environment.NewLine}  <Id>1</Id>{Environment.NewLine}  <Name>A1</Name>{Environment.NewLine}  <Bs>{Environment.NewLine}    <B>{Environment.NewLine}      <Id>1</Id>{Environment.NewLine}      <Name>B1</Name>{Environment.NewLine}    </B>{Environment.NewLine}    <B>{Environment.NewLine}      <Id>1</Id>{Environment.NewLine}      <Name>B2</Name>{Environment.NewLine}    </B>{Environment.NewLine}  </Bs>{Environment.NewLine}</A>";
            var xml = new Xml(xmltext);
            xml.PrettySettings.OmitXmlDeclaration = true;

            // Act
            var actual = xml.PrettyXml;

            // Assert
            Assert.AreEqual(expectedXml, actual);
        }
    }
}
