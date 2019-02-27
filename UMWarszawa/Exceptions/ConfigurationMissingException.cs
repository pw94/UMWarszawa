using System;
using System.Configuration;
using System.Runtime.Serialization;
using System.Xml;

namespace UMWarszawa.Exceptions
{
    public class ConfigurationMissingException : ConfigurationErrorsException
    {
        public ConfigurationMissingException()
        {
        }

        public ConfigurationMissingException(string message) : base(message)
        {
        }

        public ConfigurationMissingException(string message, Exception inner) : base(message, inner)
        {
        }

        public ConfigurationMissingException(string message, XmlNode node) : base(message, node)
        {
        }

        public ConfigurationMissingException(string message, XmlReader reader) : base(message, reader)
        {
        }

        public ConfigurationMissingException(string message, string filename, int line) : base(message, filename, line)
        {
        }

        public ConfigurationMissingException(string message, Exception inner, XmlNode node) : base(message, inner, node)
        {
        }

        public ConfigurationMissingException(string message, Exception inner, XmlReader reader) : base(message, inner, reader)
        {
        }

        public ConfigurationMissingException(string message, Exception inner, string filename, int line) : base(message, inner, filename, line)
        {
        }

        protected ConfigurationMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}