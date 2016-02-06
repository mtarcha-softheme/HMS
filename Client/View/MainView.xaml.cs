// <copyright company="Tarcha Company">
//       Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Myroslava Tarcha</author>

using System.Windows;
using System.Windows.Controls;
using Client.ViewModel;

namespace Client.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private readonly MainViewModel _mainViewModel;
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();

            _mainViewModel = viewModel;
            DataContext = viewModel;
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ConnectionDialogWindow(_mainViewModel);
            dialog.Owner = Application.Current.MainWindow; // We must also set the owner for this to work.
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.ShowDialog();
        }

        private void btnFileNamesByPartialName_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FileNamesByPartialNameDialogWindow(_mainViewModel);
            dialog.Owner = Application.Current.MainWindow; // We must also set the owner for this to work.
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.ShowDialog();
        }

        private void btnDownloadFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new DownloadFileWindow(_mainViewModel);
            dialog.Owner = Application.Current.MainWindow; // We must also set the owner for this to work.
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.ShowDialog();
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            System.Diagnostics.Process.Start((string)button.Content);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            var dialog = new ProcessResultWindow((ProcessingInfo)button.DataContext);
            dialog.Owner = Application.Current.MainWindow; // We must also set the owner for this to work.
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.ShowDialog();
        }
    }
}
