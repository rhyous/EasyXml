using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.EasyXml.Tests
{
    [TestClass]
    public class SerializableDictionaryTests
    {
        [TestMethod]
        public void TestSerializeStringString()
        {
            var expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Dictionary><key>1</key><value>one</value><key>2</key><value>two</value></Dictionary>";
            var dict = new SerializableDictionary<string, string>();
            dict.Add("1", "one");
            dict.Add("2", "two");
            var actual = Serializer.SerializeToXml(dict);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeserializeStringString()
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Dictionary><key>1</key><value>one</value><key>2</key><value>two</value></Dictionary>";
            var actual = Serializer.DeserializeFromXml<SerializableDictionary<string, string>>(ref xml);
            Assert.AreEqual("one", actual["1"]);
        }

        [TestMethod]
        public void TestSerializeIntString()
        {
            var expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Dictionary><key>1</key><value>one</value><key>2</key><value>two</value></Dictionary>";
            var dict = new SerializableDictionary<int, string>();
            dict.Add(1, "one");
            dict.Add(2, "two");
            var actual = Serializer.SerializeToXml(dict);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeserializeIntString()
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Dictionary><key>1</key><value>one</value><key>2</key><value>two</value></Dictionary>";
            var actual = Serializer.DeserializeFromXml<SerializableDictionary<int, string>>(ref xml);
            Assert.AreEqual("one", actual[1]);
        }

        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [TestMethod]
        public void TestSerializeIntPerson()
        {
            var expected = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Dictionary><key>1</key><value><FirstName>Joe</FirstName><LastName>Johnson</LastName></value><key>2</key><value><FirstName>Tom</FirstName><LastName>Tomison</LastName></value></Dictionary>";
            var dict = new SerializableDictionary<int, Person>();
            dict.Add(1, new Person { FirstName = "Joe", LastName = "Johnson"});
            dict.Add(2, new Person { FirstName = "Tom", LastName = "Tomison" });
            var actual = Serializer.SerializeToXml(dict);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestDeserializeIntPerson()
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Dictionary><key>1</key><value><FirstName>Joe</FirstName><LastName>Johnson</LastName></value><key>2</key><value><FirstName>Tom</FirstName><LastName>Tomison</LastName></value></Dictionary>";
            var actual = Serializer.DeserializeFromXml<SerializableDictionary<int, Person>>(ref xml);
            Assert.AreEqual("Joe", actual[1].FirstName);
            Assert.AreEqual("Johnson", actual[1].LastName);
            Assert.AreEqual("Tom", actual[2].FirstName);
            Assert.AreEqual("Tomison", actual[2].LastName);
        }

        public class NoEmptyConstructor
        {
            public NoEmptyConstructor(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void TestSerializeIntNoEmptyConstructor()
        {
            var dict = new SerializableDictionary<int, NoEmptyConstructor>();
            dict.Add(1, new NoEmptyConstructor("Joe","Johnson"));
            dict.Add(2, new NoEmptyConstructor("Tom", "Tomison"));
            var actual = Serializer.SerializeToXml(dict);            
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void TestDeserializeIntNoEmptyConstructor()
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Dictionary><key>1</key><value><FirstName>Joe</FirstName><LastName>Johnson</LastName></value><key>2</key><value><FirstName>Tom</FirstName><LastName>Tomison</LastName></value></Dictionary>";
            var actual = Serializer.DeserializeFromXml<SerializableDictionary<int, NoEmptyConstructor>>(ref xml);
        }
    }
}
