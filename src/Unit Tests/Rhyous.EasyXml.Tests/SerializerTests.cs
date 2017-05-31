using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Rhyous.EasyXml.Tests
{
    public class A
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<B> Bs { get; set; }
    }
    public class B
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [TestClass]
    public class SerializerTests
    {
        [TestMethod]
        public void OnlyOneParameterNeededToCreateString()
        {
            // Arrange
            var a = new A { Id = 1, Name = "A1", Bs = new List<B> { new B { Id = 1, Name = "B1" }, new B { Id = 1, Name = "B2" } } };

            // Act
            Serializer.SerializeToXml(a);

            // Assert
            // Just make sure this runs.
        }

        [TestMethod]
        public void TwoParametesToCreateStringWorks()
        {
            // Arrange
            var a = new A { Id = 1, Name = "A1", Bs = new List<B> { new B { Id = 1, Name = "B1" }, new B { Id = 1, Name = "B2" } } };

            // Act
            Serializer.SerializeToXml(a, true);

            // Assert
            // Just make sure this runs.
        }

        [TestMethod]
        public void ThreeParametesToCreateStringWorks()
        {
            // Arrange
            var a = new A { Id = 1, Name = "A1", Bs = new List<B> { new B { Id = 1, Name = "B1" }, new B { Id = 1, Name = "B2" } } };

            // Act
            Serializer.SerializeToXml(a, true, null);

            // Assert
            // Just make sure this runs.
        }

        [TestMethod]
        public void OnlyTwoParameterNeededToCreateFile()
        {
            // Arrange
            var a = new A { Id = 1, Name = "A1", Bs = new List<B> { new B { Id = 1, Name = "B1" }, new B { Id = 1, Name = "B2" } } };

            // Act
            Serializer.SerializeToXml(a, "file.xml");

            // Assert
            // Just make sure this runs.
        }

        [TestMethod]
        public void MethodWithThreeParametersWorks()
        {
            // Arrange
            var a = new A { Id = 1, Name = "A1", Bs = new List<B> { new B { Id = 1, Name = "B1" }, new B { Id = 1, Name = "B2" } } };

            // Act
            Serializer.SerializeToXml(a, "file.xml", true);

            // Assert
            // Just make sure this runs.
        }

        [TestMethod]
        public void MethodWithFourParametersWorks()
        {
            // Arrange
            var a = new A { Id = 1, Name = "A1", Bs = new List<B> { new B { Id = 1, Name = "B1" }, new B { Id = 1, Name = "B2" } } };

            // Act
            Serializer.SerializeToXml(a, true, new XmlSerializerNamespaces(), Encoding.UTF8);

            // Assert
            // Just make sure this runs.
        }

        [TestMethod]
        public void UseDefaultNamespacesTests()
        {
            // Arrange
            var a = new A { Id = 1, Name = "A1", Bs = new List<B> { new B { Id = 1, Name = "B1" }, new B { Id = 1, Name = "B2" } } };
            var expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><A xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Id>1</Id><Name>A1</Name><Bs><B><Id>1</Id><Name>B1</Name></B><B><Id>1</Id><Name>B2</Name></B></Bs></A>";

            // Act
            var xml = Serializer.Instance.ToXml(a, false, null, Encoding.UTF8, true);

            // Assert
            Assert.AreEqual(expectedXml, xml);
        }

        [TestMethod]
        public void UseDefaultNamespacesStaticTests()
        {
            // Arrange
            var a = new A { Id = 1, Name = "A1", Bs = new List<B> { new B { Id = 1, Name = "B1" }, new B { Id = 1, Name = "B2" } } };
            var expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><A xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Id>1</Id><Name>A1</Name><Bs><B><Id>1</Id><Name>B1</Name></B><B><Id>1</Id><Name>B2</Name></B></Bs></A>";

            // Act
            var xml = Serializer.SerializeToXml(a, false, null, Encoding.UTF8, true);

            // Assert
            Assert.AreEqual(expectedXml, xml);
        }
    }
}
