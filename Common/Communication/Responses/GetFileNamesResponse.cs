﻿// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;

namespace Common.Communication.Responses
{
    [Serializable]
    public class GetFileNamesResponse : ResponseBase
    {
        public string SearchResult { get; set; }

        public string ErrorMessage { get; set; }
    }
}