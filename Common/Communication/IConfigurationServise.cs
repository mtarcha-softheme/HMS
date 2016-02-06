// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>
namespace Common.Communication
{
    public interface IConfigurationServise
    {
        long GetCurrentIp();

        int GetPort();

        int GetFreePort();
    }
}