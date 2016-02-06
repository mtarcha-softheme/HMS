// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System;
using System.Windows;
using Client.View;
using Client.ViewModel;
using Common;
using Common.Communication;

namespace Client
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var window = new MainView(new MainViewModel(new Model.Model(new ConfigurationService())));
            var aplication = new App();
            aplication.Run(window);
        }
    }
}
