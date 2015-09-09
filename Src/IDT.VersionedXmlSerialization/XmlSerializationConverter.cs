using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace IDT.VersionedXmlSerialization
{
    /// <summary>
    /// Converts an object into a byte array and back using xml serialization.
    /// </summary>
    public class XmlSerializationConverter
    {
        private const int NumberLeadingChars = 3;  // '&#x'  in the regex below
        private const int NumberTrailingChars = 1; //  '+'  in the regex below
        private static readonly Regex EscapedCharRegEx = new Regex("&#x[0-9A-F]+;");

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSerializationConverter"/> class.
        /// </summary>
        public XmlSerializationConverter()
        {
            _typeBuilder = new ProxyInterfaceTypeBuilder();
        }

        /// <summary>
        /// Decodes the specified message from a byte array to an object.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <returns>The decoded object.</returns>
        /// <exception cref="InvalidOperationException">An error occurred during deserialization.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> or <paramref name="type"/> is <c>null</c>.</exception>
        public object Decode(byte[] message, Type type)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            Type decodedType = type;
            if (type.IsInterface)
            {
                decodedType = _typeBuilder.GetProxyType(type);
            }

            string stringMessage = Encoding.UTF8.GetString(message);
            if (!(decodedType.IsPrimitive || decodedType.FullName == "System.String"))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(stringMessage);
                XmlNode rootNode = xmlDoc.SelectSingleNode("/*");
                stringMessage = String.Format("<{0}>{1}</{0}>", decodedType.Name, rootNode.InnerXml);
            }

            string sanitizedStringMessage = Sanitize(stringMessage);
            var stringReader = new StringReader(sanitizedStringMessage);
            var xmlSerializer = new XmlSerializer(decodedType);
            object deserializedObject = xmlSerializer.Deserialize(stringReader);

            if (deserializedObject == null)
            {
                // This won't happen if the sender used this converter -
                // but the sender could use any converter (or even another library).
                throw new InvalidOperationException("The message decoded to a null object.");
            }

            return deserializedObject;
        }

        /// <summary>
        /// Encodes the specified message from an object to a byte array.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception>
        public byte[] Encode(object message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            var stringWriter = new StringWriter();
            var xmlSerializer = new XmlSerializer(message.GetType());
            xmlSerializer.Serialize(stringWriter, message);
            string stringMessage = stringWriter.ToString();
            string sanitizedStringMessage = Sanitize(stringMessage);
            byte[] encodedMessage = Encoding.UTF8.GetBytes(sanitizedStringMessage);
            return encodedMessage;
        }

        private string ClearIllegalEscapeSequences(Match regexMatch)
        {
            const int BASE16 = 16;

            string value = regexMatch.Value;
            int numberLength = value.Length - (NumberLeadingChars + NumberTrailingChars);
            string hexNumber = value.Substring(NumberLeadingChars, numberLength);
            int hexValue = Convert.ToInt32(hexNumber, BASE16);
            if (!IsLegalXmlChar(hexValue))
            {
                return string.Empty;
            }

            return value;
        }

        /// <summary>
        ///  Whether a given character is allowed by XML 1.0.
        /// </summary>
        private bool IsLegalXmlChar(int character)
        {
            return
                (
                    character == 0x9 /* == '\t' == 9   */||
                    character == 0xA /* == '\n' == 10  */||
                    character == 0xD /* == '\r' == 13  */||
                    (character >= 0x20 && character <= 0xD7FF) ||
                    (character >= 0xE000 && character <= 0xFFFD) ||
                    (character >= 0x10000 && character <= 0x10FFFF)
                );
        }

        /// <summary>
        /// Sanitizes the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns></returns>
        private string Sanitize(string xml)
        {
            if (xml == null)
            {
                return null;
            }

            return EscapedCharRegEx.Replace(xml, ClearIllegalEscapeSequences);
        }


        private readonly ProxyInterfaceTypeBuilder _typeBuilder;
    }
}
