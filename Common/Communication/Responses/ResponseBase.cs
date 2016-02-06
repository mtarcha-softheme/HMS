// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.Xml.Serialization;

namespace Common.Communication.Responses
{
    [XmlInclude(typeof(ConnectionResponse))]
    [XmlInclude(typeof(GetFileNamesResponse))]
    [Serializable]
    public class ResponseBase
    {
        public Guid Id { get; set; }        
    }
}