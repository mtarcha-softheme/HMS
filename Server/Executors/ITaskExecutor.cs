// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using Server.Console.Queries;

namespace Server.Console.Executors
{
    public interface ITaskExecutor
    {
        Guid Id { get; }
        
        QueryBase Query { get; }
        
        void Execute();
    }
}