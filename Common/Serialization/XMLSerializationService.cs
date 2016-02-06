// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System.IO;
using System.Xml.Serialization;

namespace Common.Serialization
{
    public class XMLSerializationService<TSerializationObject> 
    {
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(TSerializationObject));

        public void SerializeToStream(Stream stream, TSerializationObject source)
        {
            _serializer.Serialize(stream, source);
        }

        public TSerializationObject DeserializeFromStream(Stream stream)
        {
            return (TSerializationObject)_serializer.Deserialize(stream);
        }
    }
}