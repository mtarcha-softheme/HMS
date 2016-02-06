// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

namespace Client.Model
{
    public class DataReceivedEventArgs<TData> 
    {
        public DataReceivedEventArgs(TData data)
        {
            Data = data;
        }

        public TData Data { get; private set; }
    }
}