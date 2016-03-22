using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace Rhyous.EasyXml.Tests
{
    [TestClass]
    public class XmlTests
    {
        public string LinearUtf8Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Person><FirstName>John</FirstName><MiddleName>Al Leon</MiddleName><LastName>Doe</LastName></Person>";
        public string PrettyUtf8Xml =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<Person>
  <FirstName>John</FirstName>
  <MiddleName>Al Leon</MiddleName>
  <LastName>Doe</LastName>
</Person>";
        public string PrettyUtf8XmlWithTabs =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<Person>
	<FirstName>John</FirstName>
	<MiddleName>Al Leon</MiddleName>
	<LastName>Doe</LastName>
</Person>";
        public string UglyUtf8Xml =
@"<?xml version=""1.0""
encoding=""UTF-8""?>
<Person>

<FirstName>
    John
        </FirstName>

<MiddleName>
    Al
    Leon
                </MiddleName>
  <LastName>
    


Doe
        </LastName>


</Person>";

        public string LinearUtf16Xml = "<?xml version=\"1.0\" encoding=\"UTF-16\"?><Person><FirstName>John</FirstName><MiddleName>Al Leon</MiddleName><LastName>Doe</LastName></Person>";
        public string PrettyUtf16Xml =
@"<?xml version=""1.0"" encoding=""UTF-16""?>
<Person>
  <FirstName>John</FirstName>
  <MiddleName>Al Leon</MiddleName>
  <LastName>Doe</LastName>
</Person>";
        public string UglyUtf16Xml =
@"<?xml version=""1.0""
encoding=""UTF-16""?>
<Person>

<FirstName>
    John
        </FirstName>

<MiddleName>
    Al
    Leon
                </MiddleName>
  <LastName>
    


Doe
        </LastName>


</Person>";

        [TestMethod]
        public void TestMethodLinearize()
        {
            // Arrange
            Xml xml = new Xml(PrettyUtf8Xml);

            // Act
            var actual = xml.LinearizeXml;

            // Assert
            LinearUtf8Xml.ShouldEqualWithDiff(actual);
        }

        [TestMethod]
        public void TestMethodPretty()
        {
            // Arrange
            Xml xml = new Xml(LinearUtf8Xml);

            // Act
            var actual = xml.PrettyXml;

            // Assert
            PrettyUtf8Xml.ShouldEqualWithDiff(actual);
        }

        [TestMethod]
        public void TestMethodLinearizeUgly()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf8Xml);

            // Act
            var actual = xml.LinearizeXml;

            // Assert
            LinearUtf8Xml.ShouldEqualWithDiff(actual);
        }

        [TestMethod]
        public void TestMethodMakeUglyPretty()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf8Xml);

            // Act
            var actual = xml.PrettyXml;

            // Assert
            PrettyUtf8Xml.ShouldEqualWithDiff(actual);
        }

        [TestMethod]
        public void TestMethodLinearizeUglyUtf16()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf16Xml)
            {
                Encoding = Xml.XmlEncoding.UTF16
            };

            // Act
            var actual = xml.LinearizeXml;

            // Assert
            LinearUtf16Xml.ShouldEqualWithDiff(actual);
        }

        [TestMethod]
        public void TestMethodMakeUglyPrettyUtf16()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf16Xml)
            {
                Encoding = Xml.XmlEncoding.UTF16
            };

            // Act
            var actual = xml.PrettyXml;

            // Assert
            PrettyUtf16Xml.ShouldEqualWithDiff(actual);
        }

        [TestMethod]
        public void TestMethodStreamIsUtf8()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf8Xml)
            {
                Encoding = Xml.XmlEncoding.UTF8
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
        public void TestMethodStreamIsUtf16()
        {
            // Arrange
            Xml xml = new Xml(UglyUtf16Xml)
            {
                Encoding = Xml.XmlEncoding.UTF16
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
        public void TestMethodPrettyWithTabs()
        {
            // Arrange
            Xml xml = new Xml(LinearUtf8Xml)
            {
                IndentCharacters = "\t"
            };

            // Act
            var actual = xml.PrettyXml;

            // Assert
            PrettyUtf8XmlWithTabs.ShouldEqualWithDiff(actual);
        }

        [TestMethod]
        public void TestMethodStreamUtf8IsDifferentThanStreamUtf16()
        {
            const string text = "Hello, world!";

            var utf8 = Encoding.UTF8.GetBytes(text);
            var utf16 = Encoding.Unicode.GetBytes(text);

            Assert.AreNotEqual(utf8.Length, utf16.Length);
        }
    }
}
