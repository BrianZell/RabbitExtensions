using NUnit.Framework;
using System.Text;

namespace IDT.VersionedXmlSerialization.Tests.TestCases
{
    class XmlSerializationConverterTests
    {
        [Test]
        public void EncodeAndDecodeYieldsSameObjectTest()
        {
            var testObject = new TestMessageObjectNoInterface() { Text = "TestMe" };

            var xmlSerializationConverter = new XmlSerializationConverter();

            byte[] encodedMessage = xmlSerializationConverter.Encode(testObject);
            object decodedMessage = xmlSerializationConverter.Decode(encodedMessage, typeof(TestMessageObjectNoInterface));

            var resultTestObject = decodedMessage as TestMessageObjectNoInterface;
            Assert.That(resultTestObject.Text, Is.EqualTo("TestMe"));
        }

        [Test]
        public void EncodeAsOneTypeDecodeAsAnInterfaceTest()
        {
            var testObject = new TestMessageObjectWInterfaces() { Text = "TestMe", Reply = "TestReply"};

            var xmlSerializationConverter = new XmlSerializationConverter();

            byte[] encodedMessage = xmlSerializationConverter.Encode(testObject);
            object decodedMessage = xmlSerializationConverter.Decode(encodedMessage, typeof(ITestMessageObjectWInterfaces1));

            var resultTestObject = decodedMessage as ITestMessageObjectWInterfaces1;
            Assert.That(resultTestObject.Text, Is.EqualTo("TestMe"));
        }

        [Test]
        public void EncodeAsOneTypeDecodeAsAnotherInterfaceTest()
        {
            var testObject = new TestMessageObjectWInterfaces() { Text = "TestMe", Reply = "TestReply"};

            var xmlSerializationConverter = new XmlSerializationConverter();

            byte[] encodedMessage = xmlSerializationConverter.Encode(testObject);
            object decodedMessage = xmlSerializationConverter.Decode(encodedMessage, typeof(ITestMessageObjectWInterfaces2));

            var resultTestObject = decodedMessage as ITestMessageObjectWInterfaces2;
            Assert.That(resultTestObject.Reply, Is.EqualTo("TestReply"));
        }

        [Test]
        public void EncodeAndDecodeString()
        {
            var testObject = "TestMe";

            var xmlSerializationConverter = new XmlSerializationConverter();

            byte[] encodedMessage = xmlSerializationConverter.Encode(testObject);
            object decodedMessage = xmlSerializationConverter.Decode(encodedMessage, typeof(string));

            var resultTestObject = decodedMessage as string;
            Assert.That(resultTestObject, Is.EqualTo("TestMe"));
        }

        [Test]
        public void EncodeAndDecodeInt()
        {
            int testObject = 42;

            var xmlSerializationConverter = new XmlSerializationConverter();

            byte[] encodedMessage = xmlSerializationConverter.Encode(testObject);
            object decodedMessage = xmlSerializationConverter.Decode(encodedMessage, typeof(int));

            var resultTestObject = (int)decodedMessage;
            Assert.That(resultTestObject, Is.EqualTo(42));
        }

        [Test]
        public void EncodeAndDecodeStripsWithInvalidChars()
        {
            StringBuilder invalidCharacterStringBuilder = new StringBuilder();
            invalidCharacterStringBuilder.Append("Test!");
            invalidCharacterStringBuilder.Append("\a");
            invalidCharacterStringBuilder.Append((char)0x1F);
            invalidCharacterStringBuilder.Append("Me");
            var testObject = new TestMessageObjectNoInterface() { Text = invalidCharacterStringBuilder.ToString() };

            var xmlSerializationConverter = new XmlSerializationConverter();

            byte[] encodedMessage = xmlSerializationConverter.Encode(testObject);
            object decodedMessage = xmlSerializationConverter.Decode(encodedMessage, typeof(TestMessageObjectNoInterface));

            var resultTestObject = decodedMessage as TestMessageObjectNoInterface;
            Assert.That(resultTestObject.Text, Is.EqualTo("Test!Me"));
        }

        [Test]
        public void EncodeAndDecodeStripsWithValidEscapedChars()
        {
            StringBuilder invalidCharacterStringBuilder = new StringBuilder();
            invalidCharacterStringBuilder.Append("Test!");
            invalidCharacterStringBuilder.Append("\t");
            invalidCharacterStringBuilder.Append("\n");
            invalidCharacterStringBuilder.Append("Me");
            var testObject = new TestMessageObjectNoInterface() { Text = invalidCharacterStringBuilder.ToString() };

            var xmlSerializationConverter = new XmlSerializationConverter();

            byte[] encodedMessage = xmlSerializationConverter.Encode(testObject);
            object decodedMessage = xmlSerializationConverter.Decode(encodedMessage, typeof(TestMessageObjectNoInterface));

            var resultTestObject = decodedMessage as TestMessageObjectNoInterface;
            Assert.That(resultTestObject.Text, Is.EqualTo("Test!\t\nMe"));
        }
    }
}
